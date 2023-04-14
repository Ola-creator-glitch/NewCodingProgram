using Rejestracja_konta.Data;
using Rejestracja_konta.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Rejestracja_konta.Extensions;
using System.Reflection;
using System.Linq.Expressions;
using System;
using System.Xml.Linq;

namespace Rejestracja_konta.Services
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly ApplicationUserDbContext _dbContext;

        public ApplicationUserService(DbContextOptions<ApplicationUserDbContext> options)
        {
            _dbContext = new ApplicationUserDbContext(options);
        }


        public ApplicationUser Create(ApplicationUserModel ApplicationUserModel)
        {
            if (ApplicationUserModel == null)
            {
                throw new ArgumentNullException(nameof(ApplicationUserModel));
            }

            var user = ApplicationUser.FromApplicationUserModel(ApplicationUserModel);

            _dbContext.ApplicationUsers.Add(user);
            _dbContext.SaveChanges();

            return user;

        }

        public ApplicationUser GetById(int id)
        {
            return _dbContext.ApplicationUsers.FirstOrDefault(x => x.Id == id);
        }

        public IQueryable<ApplicationUser> GetAll()
        {
            return _dbContext.ApplicationUsers;
        }

        public void Update(ApplicationUser ApplicationUserToUpdate)
        {
            var ApplicationUser = GetById(ApplicationUserToUpdate.Id);

            if (ApplicationUser == null)
            {
                throw new Exception($"Nie znaleziono użytkownika o id {ApplicationUserToUpdate.Id}");
            }

            ApplicationUser.Imie = ApplicationUserToUpdate.Imie;
            ApplicationUser.Nazwisko = ApplicationUserToUpdate.Nazwisko;
            ApplicationUser.Pesel = ApplicationUserToUpdate.Pesel;
            ApplicationUser.Email = ApplicationUserToUpdate.Email;
            ApplicationUser.Telefon = ApplicationUserToUpdate.Telefon;
            ApplicationUser.Wiek = ApplicationUserToUpdate.Wiek;
            ApplicationUser.SrednieZuzyciePradu = ApplicationUserToUpdate.SrednieZuzyciePradu;

            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var ApplicationUser = GetById(id);

            if (ApplicationUser == null)
            {
                throw new Exception($"Nie znaleziono użytkownika o id {id}");
            }

            _dbContext.ApplicationUsers.Remove(ApplicationUser);
            _dbContext.SaveChanges();
        }
    }

    public interface IApplicationUserService
    {
        ApplicationUser Create(ApplicationUserModel ApplicationUserModel);
        ApplicationUser GetById(int id);
        IQueryable<ApplicationUser> GetAll();
        void Update(ApplicationUser ApplicationUserToUpdate);
        void Delete(int id);
    }

    public class ApplicationUser
    {
        public int Id { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Pesel { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public int? Wiek { get; set; }
        public decimal? SrednieZuzyciePradu { get; set; }
        public string HasloSalt { get; set; }
        public object FirstName { get; internal set; }
        public object LastName { get; internal set; }
        public object Password { get; internal set; }

        public static ApplicationUser FromApplicationUserModel(ApplicationUserModel ApplicationUserModel)
        {
            var ApplicationUser = new ApplicationUser
            {
                Imie = ApplicationUserModel.Imie,
                Nazwisko = ApplicationUserModel.Nazwisko,
                Pesel = ApplicationUserModel.Pesel,
                Email = ApplicationUserModel.Email,
                Telefon = ApplicationUserModel.Telefon,
                Wiek = ApplicationUserModel.Wiek,
                SrednieZuzyciePradu = ApplicationUserModel.SrednieZuzyciePradu,
                HasloSalt = ApplicationUserModel.CreateSalt()
            };

            ApplicationUser.HasloSalt = ApplicationUserModel.SzyfrowaneHaslo;
            return ApplicationUser;
        }
    }
}
