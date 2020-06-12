using AuctionOnline.Data;
using AuctionOnline.Models;
using Microsoft.AspNetCore.Mvc;
using System;
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
            ViewBag.CategoryAdd = "Failed";
            category.CreatedAt = DateTime.Now;
            if (category != null)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                ViewBag.CategoryAdd = "Success";
            }
            return RedirectToAction("AdminAdd");
        }

        [HttpGet]
        [Route("addchild/{id}")]
        public IActionResult AdminAddChild(int id)
        {
            ViewBag.CategoryAddChild = db.Categories.Find(id);
            return View("AdminAddChild");
        }

        [HttpPost]
        [Route("addchild")]
        public IActionResult AdminAddChild(Category category)
        {
            ViewBag.CategoryAddChild = "Failed";
            category.CreatedAt = DateTime.Now;

            if (category != null)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                ViewBag.CategoryAddChild = "Success";
            }
            return RedirectToAction("AdminAdd");
        }

        [HttpGet]
        [Route("edit/{id}")]
        public IActionResult AdminEdit(int id)
        {
            ViewBag.Edit = db.Categories.Find(id);
            return View("AdminEdit", ViewBag.Edit);
        }

        [HttpPost]
        [Route("edit")]
        public IActionResult AdminEdit(Category category)
        {
            db.Entry(category).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("AdminIndex", "category");
        }

        [Route("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var category = db.Categories.Find(id);
            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("AdminIndex");
        }
    }
}