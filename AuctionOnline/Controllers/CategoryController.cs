using AuctionOnline.Data;
using AuctionOnline.Models;
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
        public IActionResult AdminIndex()
        {
            ViewBag.Categories = db.Categories.ToList();
            return View();
        }
        [HttpGet]
        [Route("add")]
        public IActionResult AdminAdd()
        {
            return View("AdminAdd", new Category());
        }
        [HttpPost]
        [Route("add")]
        public IActionResult AdminAdd(Category category)
        {
            ViewBag.result = "Failed";
            if (category != null)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                ViewBag.result = "Success";
            }
            return View();
        }
        [Route("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var category = db.Categories.Find(id);
            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}