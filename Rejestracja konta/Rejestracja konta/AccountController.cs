using Microsoft.AspNetCore.Mvc;
using Rejestracja_konta.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rejestracja_konta.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;

        public AccountController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IActionResult Register(RegisterViewModel model)
        {
            return Register(model, _userRepository);
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model, IUserRepository userRepository)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // stwórz obiekt ApplicationUser i ustaw wartości
            var user = new ApplicationUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Password = model.Password,
                // dodaj inne właściwości, jeśli istnieją
            };
            userRepository.Add(user);

            return RedirectToAction("Index", "Home");
        }

        public class RegisterViewModel
        {
            public object FirstName { get; internal set; }
            public object LastName { get; internal set; }
            public object Password { get; internal set; }
            public string Email { get; internal set; }
        }

        public interface IUserRepository
        {
            void Add(ApplicationUser user);
            ApplicationUser Get(string email);
        }
    }
}
