using System.Collections.Generic;
using System.Linq;
using Rejestracja_konta.Models;
using Rejestracja_konta.Data;

namespace Rejestracja_konta.Services
{
    public class PublicUserRepository : IPublicUserRepository
    {
        private readonly AppDbContext _dbContext;

        public PublicUserRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(User user)
        {
            throw new NotImplementedException();
        }

        public void Add(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public void AddPublicUser(PublicUser publicUser)
        {
            _dbContext.PublicUsers.Add(publicUser);
            _dbContext.SaveChanges();
        }

        public void Delete(User user)
        {
            throw new NotImplementedException();
        }

        public ApplicationUser Get(string email)
        {
            throw new NotImplementedException();
        }

        public List<PublicUser> GetAllPublicUsers()
        {
            if (_dbContext == null)
            {
                throw new NullReferenceException("AppDbContext is null in PublicUserRepository.GetAllPublicUsers");
            }

            return System.Linq.Enumerable.ToList<PublicUser>(_dbContext.PublicUsers);
        }



        public void Update(User user)
        {
            throw new NotImplementedException();
        }
    }

    public class PublicUser
    {
    }
}
