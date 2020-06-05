using AuctionOnline.Data;
using AuctionOnline.Models;
using AuctionOnline.Models.Business;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AuctionOnline.Controllers
{
    [Route("category")]
    public class CategoryController : Controller
    {
        private AuctionDbContext db;
        public CategoryController(AuctionDbContext _category)
        {
            db = _category;
        }
        [Route("index")]
        public IActionResult Index()
        {
            ViewBag.Category = new CategoryBus(db).GetAll().ToList();            
            return View();
        }
        [Route("adminindex")]
        public IActionResult AdminIndex()
        {
            ViewBag.Categories = db.Categories.ToList();
            return View();
        }
        [HttpGet]
        [Route("add")]
        public IActionResult AdminAdd()       
        {
            ViewBag.CategoryAdd = db.Categories.ToList();
            return View("AdminAdd");
        }
        [HttpPost]
        [Route("add")]
        public IActionResult AdminAdd(Category category)
        {
            ViewBag.CategoryAdd = "Failed";
            if (category != null)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                ViewBag.result = "Success";
            }
            return RedirectToAction("AdminAdd");
        }
        [Route("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var category = db.Categories.Find(id);
            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("adminindex");
        }
        [HttpGet]
        [Route("edit/{id}")]
        public IActionResult Edit(int id)
        {
            var category = db.Categories.Find(id);
            return View("Adminindex", category);
        }
        [HttpPost]
        [Route("edit/{id}")]
        public IActionResult Edit(int id, Category category)
        {
            db.Entry(category).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("adminindex", "category");
        }
    }
}