using AuctionOnline.Controllers;
using AuctionOnline.Data;
using AuctionOnline.Utilities;
using AuctionOnline.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionOnline.ViewComponents
{
    [ViewComponent(Name = "Search")]
    public class SearchViewComponent : ViewComponent
    {
        private AuctionDbContext db;
        public SearchViewComponent(AuctionDbContext _category)
        {
            db = _category;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var allCate = db.Categories.ToList();
            //var cate =  new CategoryController(db).GetallMenu();
            //var layoutVM = new LayoutViewModel()
            //{
            //    CategoriesVM = CategoryUtility.MapModelsToVMs(getCate)
            //};
            //return View(await cate(allCate, null));
            return View();
        }
    }
}
