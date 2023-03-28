using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NextwoIdentity2023.Data;
using NextwoIdentity2023.Models;

namespace NextwoIdentity2023.Controllers
{
    public class ProductsController : Controller
    {
        private NextwoDbContext db;
        public ProductsController(NextwoDbContext _db)
        {
            db = _db;
        }
        public IActionResult Index()
        {
            return View(db.Products.Include(x=>x.Category));
        }
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            ViewBag.CatList = new SelectList(db.Categories, "CategoryId", "CategoryName");
            return View();
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public IActionResult Create(Product pro)
        {
           if (ModelState.IsValid)
            {
                db.Products.Add(pro);   
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pro);
        }
        [Authorize(Roles = "Administrator")]
        public IActionResult CreateCategory()
        {
            return View();
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public IActionResult CreateCategory(Category cat)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(cat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cat);
        }
    }
}
