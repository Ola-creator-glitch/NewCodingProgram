using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Rejestracja_konta.Extensions
{
    public static class Extensions
    {
        public static IQueryable<TSource> Where<TSource>(this IQueryable<TSource> source, IDictionary<string, object> propertyValues)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (propertyValues == null) throw new ArgumentNullException(nameof(propertyValues));

            var type = typeof(TSource);
            var parameter = Expression.Parameter(type, "x");
            var predicates = new List<Expression>();

            foreach (var propertyValue in propertyValues)
            {
                var property = type.GetProperty(propertyValue.Key);
                if (property == null) throw new ArgumentException($"Property {propertyValue.Key} does not exist on type {type.Name}");

                var value = propertyValue.Value;
                var propertyType = property.PropertyType;
                var constant = Expression.Constant(value, propertyType);
                var propertyAccess = Expression.Property(parameter, property);

                var predicate = Expression.Equal(propertyAccess, constant);
                predicates.Add(predicate);
            }

            var body = predicates.Aggregate(Expression.AndAlso);

            var lambda = Expression.Lambda<Func<TSource, bool>>(body, parameter);
            return source.Where(lambda);
        }
    }
}
