using EcommerceWebApp.Data;
using EcommerceWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace EcommerceWebApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _db;
        public AdminController(ApplicationDbContext db)
        {
            _db= db;
        }
        public IActionResult Index()
        {         
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Admin admin)
        {
            var adminCred = _db.Admin.FirstOrDefault(a=>a.Email == admin.Email && a.Password==admin.Password);
            return RedirectToAction("Index" , "Admin");
        }
    }
}
