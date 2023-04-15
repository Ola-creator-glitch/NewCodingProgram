using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rejestracja_konta.Data;
using Rejestracja_konta.Models;
using Rejestracja_konta.Services;
using System.Linq;

namespace Rejestracja_konta.Controllers
{
    public class UserController : Controller
    {
        private readonly AppDbContext _dbContext;

        public UserController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var users = _dbContext.PublicUsers.ToList();
            return View(users);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(PublicUser user)
        {
            if (ModelState.IsValid)
            {
                _dbContext.PublicUsers.Add(user);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        public IActionResult Edit(int id)
        {
            var user = _dbContext.PublicUsers.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost]
        public IActionResult Edit(PublicUser user)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Entry(user).State = EntityState.Modified;
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        public IActionResult Delete(int id)
        {
            var user = _dbContext.PublicUsers.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var user = _dbContext.PublicUsers.Find(id);
            _dbContext.PublicUsers.Remove(user);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
