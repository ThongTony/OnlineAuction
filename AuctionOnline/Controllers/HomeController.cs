using AuctionOnline.Data;
using AuctionOnline.Models;
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

            var categories = db.Categories.Where(e => e.ParentId == null).ToList();
            foreach (var cate in categories)
            {
                var categoryItems = db.CategoryItems.Where(x => x.CategoryId == cate.Id).ToList();

                foreach (var categoryItem in categoryItems)
                {
                    categoryItem.Item = db.Items.FirstOrDefault(x => x.Id == categoryItem.ItemId);
                }
                cate.CategoryItems = categoryItems;
            }
            var layoutVM = new LayoutViewModel()
            {
                Categories = categories
            };

            return View(layoutVM);
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