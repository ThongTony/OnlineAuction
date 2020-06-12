using AuctionOnline.Data;
using AuctionOnline.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AuctionOnline.Controllers
{
    [Route("admincategory")]
    public class AdminCategoryController : Controller
    {
        private AuctionDbContext db;
        public AdminCategoryController(AuctionDbContext _category)
        {
            db = _category;
        }
        [Route("index")]
        public IActionResult Index(int id)
        {
            ViewBag.categories = db.Categories.ToList();
            return View("Index", ViewBag.categories);
        }
        [HttpGet]
        [Route("add")]
        public IActionResult Add()
        {
            return View("Add", new Category());
        }
        [HttpPost]
        [Route("add")]
        public IActionResult Add(Category category)
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
            var categories = db.Categories.Find(id);
            db.Categories.Remove(categories);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}