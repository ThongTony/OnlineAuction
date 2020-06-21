using AuctionOnline.Data;
using AuctionOnline.Models;
using AuctionOnline.Utilities;
using AuctionOnline.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace AuctionOnline.Controllers
{
    public class HomeController : Controller
    {

        private readonly AuctionDbContext db;
        public HomeController(AuctionDbContext _db)
        {
            db = _db;
        }
        public IActionResult Index()
        {
            ViewBag.IsHome = true;

            //var categories = db.Categories.Where(e => e.ParentId == null).ToList();
            //foreach (var cate in categories)
            //{
            //    var categoryItems = db.CategoryItems.Where(x => x.CategoryId == cate.Id).ToList();

            //    foreach (var categoryItem in categoryItems)
            //    {
            //        categoryItem.Item = db.Items.FirstOrDefault(x => x.Id == categoryItem.ItemId);
            //    }
            //    cate.CategoryItems = categoryItems;
            //}
            //var layoutVM = new LayoutViewModel()
            //{
            //    CategoriesVM = CategoryUtility.MapModelsToVMs(categories)
            //};

            //return View(layoutVM);

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
            var layoutVM = new LayoutViewModel()
            {
                CategoriesVM = CategoryUtility.MapModelsToVMs(category)
            };
            return View(layoutVM);

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

        public IActionResult Logout()
        {
            return View("Index");
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}