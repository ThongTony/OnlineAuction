using AuctionOnline.Data;
using AuctionOnline.Utilities;
using AuctionOnline.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AuctionOnline.Controllers
{
    public class HomeController : Controller
    {

        private readonly AuctionDbContext db;
        private LayoutViewModel layoutVM;
        public HomeController(AuctionDbContext _db)
        {
            db = _db;
            layoutVM = new LayoutViewModel()
            {
                CategoriesVM = RecursiveMenu.GetRecursiveMenu(db)
            };
        }
        public IActionResult Index()
        {
            ViewBag.IsHome = true;

            //main items
            //var categories = db.Categories.Where(e => e.ParentId == null).ToList(); 
            var categories = db.Categories.Include(c => c.Children).ToList();
            foreach (var cate in categories)
            {
                var categoryItems = db.CategoryItems.Where(x => x.CategoryId == cate.Id).ToList();

                foreach (var categoryItem in categoryItems)
                {
                    categoryItem.Item = db.Items.FirstOrDefault(x => x.Id == categoryItem.ItemId);
                }
                cate.CategoryItems = categoryItems;
            }
            layoutVM.Categories = categories;
            return View(layoutVM);

        }

        public IActionResult Error()
        {
            return View();
        }
    }
}