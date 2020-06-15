using AuctionOnline.Data;
using AuctionOnline.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        //[HttpGet]
        //[Route("edit/{id}")]
        //public IActionResult AdminEdit(int id)
        //{
        //    ViewBag.Edit = db.Categories.Find(id);
        //    return View("AdminEdit", ViewBag.Edit);
        //}

        [HttpPost, ActionName("Edit")]
        [Route("edit")]
        public async Task<IActionResult> EditAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var categoryDb = await db.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (await TryUpdateModelAsync<Category>(categoryDb, "", s => s.Name, s => s.ParentId, s => s.CreatedAt))
            {
                try
                {
                    await db.SaveChangesAsync();
                    return RedirectToAction(nameof(AdminIndex));
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            //db.Entry(categoryDb).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            //category.CreatedAt = db.Categories.FirstOrDefault(c => c.CreatedAt);
            //db.SaveChanges();
            return View(categoryDb);
        }

        [Route("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var category = db.Categories.Find(id);
            var categoryitems = db.CategoryItems.Where(c => c.CategoryId == id);
            foreach (var categoryitem in categoryitems)
            {
                category.CategoryItems.Remove(categoryitem);
            }
            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("AdminIndex");
        }

        [Route("model")]
        public IActionResult Model()
        {
            var categories = db.Categories.ToList();
            return View("Index", categories);
        }

        public IActionResult GetallMenu()
        {
            List<Category> category = new List<Category>();
            List<Category> categories = db.Categories.ToList();
            category = categories
                .Where(c => c.ParentId == null)
                .Select(c => new Category()
                {
                    Id = c.Id,
                    Name = c.Name,
                    ParentId = c.ParentId,
                    Children = GetChildren(categories, c.Id)
                }).ToList();

            return View(category);
        }

        public static List<Category> GetChildren(List<Category> categories, int parentId)
        {
            return categories
                .Where(c => c.ParentId == parentId)
                .Select(c => new Category
                {
                    Id = c.Id,
                    Name = c.Name,
                    ParentId = c.ParentId,
                    Parent = c,
                    Children = GetChildren(categories, c.Id)
                }).ToList();
        }
    }
}