using Google.Authenticator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

public class HomeController : Controller
{
    private readonly DatabaseContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HomeController(DatabaseContext dbContext, IHttpContextAccessor httpContextAccessor)
    {
        _dbContext = dbContext;
        _httpContextAccessor = httpContextAccessor;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Rejestracja()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Rejestracja(ApplicationUserModel model)
    {
        if (ModelState.IsValid)
        {
            string salt = GenerateSalt();
            string hashedPassword = HashPassword(model.Haslo, salt);

            model.Haslo = hashedPassword;
            model.Sol = salt;
            model.KodWeryfikacyjny = Generate2FACode();

            _dbContext.ApplicationUsers.Add(model);
            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }
        return View(model);
    }

    public IActionResult ListaUzytkownikow()
    {
        List<ApplicationUserModel> ApplicationUsers = _dbContext.ApplicationUsers.ToList();
        return View(ApplicationUsers);
    }

    public IActionResult Logowanie()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Logowanie(ApplicationUserModel model)
    {
        var user = _dbContext.ApplicationUsers.FirstOrDefault(u => u.Email == model.Email);

        if (user != null)
        {
            var hashedPassword = HashPassword(model.Haslo, user.Sol);
            if (user.Haslo == hashedPassword)
            {
                _httpContextAccessor.HttpContext.Session.SetString("UserId", user.Id.ToString());
                return RedirectToAction("ListaUzytkownikow");
            }
        }

        ModelState.AddModelError("", "Nieprawidłowe dane logowania");
        return View(model);
    }

    public IActionResult Wyloguj()
    {
        _httpContextAccessor.HttpContext.Session.Clear();
        return RedirectToAction("Index");
    }

    private string GenerateSalt()
    {
        byte[] saltBytes = new byte[16];
        using (var rng = new RNGCryptoServiceProvider())
        {
            rng.GetBytes(saltBytes);
        }
        return Convert.ToBase64String(saltBytes);
    }

    private string HashPassword(string password, string salt)
    {
        byte[] saltBytes = Convert.FromBase64String(salt);
        byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

        byte[] combinedBytes = new byte[saltBytes.Length + passwordBytes.Length];
        Array.Copy(saltBytes, 0, combinedBytes, 0, saltBytes.Length);
        Array.Copy(passwordBytes, 0, combinedBytes, saltBytes.Length, passwordBytes.Length);

        using (var sha256 = SHA256.Create())
        {
            byte[] hashedBytes = sha256.ComputeHash(combinedBytes);
            return Convert.ToBase64String(hashedBytes);
        }
    }

    private string Generate2FACode()
    {
        var authenticator = new TwoFactorAuthenticator();
        var setupCode = authenticator.GenerateSetupCode("My App", "ApplicationUser@example.com", "my_secret_key", true, 300);

        return setupCode.ManualEntryKey;
    }
}

public class DatabaseContext : DbContext
{
    public DbSet<ApplicationUserModel> ApplicationUsers { get; set; }
}
