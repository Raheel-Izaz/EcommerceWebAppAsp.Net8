using EcommerceWebApp.Data;
using EcommerceWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _db;
        public AccountController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Admin admin)
        {
            var adminCred = _db.Admin.Where(u => u.Email == admin.Email).SingleOrDefault();

            
            TempData["LoginError"] = "Invalid User Name or Email";
            return View(admin);
        }
    }
}
