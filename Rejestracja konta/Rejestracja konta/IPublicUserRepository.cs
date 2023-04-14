using Rejestracja_konta.Controllers;
using Rejestracja_konta.Models;
using System.Collections.Generic;
using static Rejestracja_konta.Controllers.AccountController;

namespace Rejestracja_konta.Services
{
    public interface IPublicUserRepository : IUserRepository
    {
        new void Add(User user);
        new void Delete(User user);
        new void Update(User user);
    }

    public class User
    {
    }
}
