using EcommerceWebApp.Data;
using EcommerceWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EcommerceWebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _env;
        public ProductController(ApplicationDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public IActionResult Index()
        {
            var products = _db.Products.Include(u => u.Category).ToList();
            return View(products);
        }

        public IActionResult Create()
        {
            var categoryList = _db.Categories.ToList();
            ViewBag.CategoryList = new SelectList(categoryList, "Id", "CategoryName");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product obj, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    string webRootPath = _env.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(webRootPath, @"images\product");

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    obj.Image = @"images\product\" + fileName;
                }
                _db.Products.Add(obj);
                TempData["ProductAddMsg"] = "Product Added Successfuly";
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            var categoryList = _db.Categories.ToList();
            ViewBag.CategoryList = new SelectList(categoryList, "Id", "CategoryName");
            return View(obj);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var products = _db.Products.FirstOrDefault(p => p.Id == id);
            return View(products);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var products = _db.Products.FirstOrDefault(p => p.Id == id);


            var imagePath = Path.Combine(_env.WebRootPath, products.Image.TrimStart('\\'));
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }

            _db.Products.Remove(products);
            TempData["ProductDeleteMsg"] = "Deleted Successfully!";
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var categoryList = _db.Categories.ToList();
            ViewBag.CategoryList = new SelectList(categoryList, "Id", "CategoryName");
            var product = _db.Products.FirstOrDefault(p => p.Id == id);
            return View(product);
        }

        [HttpPost]
        public IActionResult Update(Product obj, IFormFile newImage)
        {
            if (ModelState.IsValid)
            {

                var products = _db.Products.FirstOrDefault(p => p.Id == obj.Id);

                if (!string.IsNullOrEmpty(products.Image))
                {
                    var oldImage = Path.Combine(_env.WebRootPath, products.Image.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImage))
                    {
                        System.IO.File.Delete(oldImage);
                    }
                }

                if (newImage != null && newImage.Length > 0)
                {
                    string webRootpath = _env.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(newImage.FileName);
                    string productPath = Path.Combine(webRootpath, @"images\product");

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        newImage.CopyTo(fileStream);
                    }
                    products.Image = @"images\product\" + fileName; 
                }

                // Update product details
                products.Name = obj.Name;
                products.Description = obj.Description;
                products.Price = obj.Price;
                products.CategoryId = obj.CategoryId;

                _db.Products.Update(products);
                TempData["ProductUpdateMsg"] = "Updated Successfully!";
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            var categoryList = _db.Categories.ToList();
            ViewBag.CategoryList = new SelectList(categoryList, "Id", "CategoryName");
            return View(obj);
        }
    }
}
