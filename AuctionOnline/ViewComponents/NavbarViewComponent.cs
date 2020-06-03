using AuctionOnline.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionOnline.ViewComponents
{
    [ViewComponent(Name = "Navbar")]
    public class NavbarViewComponent : ViewComponent
    {
        private AuctionDbContext db;
        public NavbarViewComponent(AuctionDbContext _category)
        {
            db = _category;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            ViewBag.Categories = db.Categories.ToList();
            //ViewBag.cate = db.Categories.Where(c => c.Id == c.Id).ToList();
            return View("Index", ViewBag.Categories);
        }
    }
}
