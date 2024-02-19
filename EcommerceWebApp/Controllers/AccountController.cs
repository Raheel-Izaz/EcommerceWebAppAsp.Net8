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

            if (adminCred != null)
            {
                bool isValid = (adminCred.Email == admin.Email && adminCred.Password == admin.Password);
                if (isValid)
                {
                   var ident
                }
                else
                {
                    TempData["LoginError"] = "Invalid User Name or Email";
                }
            }
            TempData["LoginError"] = "Invalid User Name or Email";
            return View(admin);
        }
    }
}
