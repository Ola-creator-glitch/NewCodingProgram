using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Rejestracja_konta.Data;
using Rejestracja_konta.Models;

namespace Rejestracja_konta.Services
{
    public class ApplicationUserRepository : IApplicationUserRepository
    {
        private readonly ApplicationUserDbContext _context;

        public ApplicationUserRepository(ApplicationUserDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Initialize()
        {
            _context.Database.EnsureCreated();
        }

        public ApplicationUser GetApplicationUserById(int id)
        {
            return _context.ApplicationUsers.FirstOrDefault(u => u.Id == id);
        }

        public ApplicationUser GetByEmail(string email)
        {
            return _context.ApplicationUsers.FirstOrDefault(u => u.Email == email);
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            return (IEnumerable<ApplicationUser>)_context.ApplicationUsers.ToList();
        }

        public void Add(ApplicationUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            _context.ApplicationUsers.Add(user);
            _context.SaveChanges();
        }


        public void Delete(ApplicationUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            _context.ApplicationUsers.Remove(user);
            _context.SaveChanges();
        }

        public void Update(ApplicationUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            _context.ApplicationUsers.Update(user);
            _context.SaveChanges();
        }
    }

    public interface IApplicationUserRepository
    {
        ApplicationUser GetApplicationUserById(int id);
        ApplicationUser GetByEmail(string email);
        IEnumerable<ApplicationUser> GetAll();
        void Add(ApplicationUser user);
        void Update(ApplicationUser user);
        void Delete(ApplicationUser user);
    }
}
