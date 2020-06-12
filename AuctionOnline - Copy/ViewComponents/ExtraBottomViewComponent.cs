using AuctionOnline.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionOnline.ViewComponents
{
    [ViewComponent(Name = "ExtraBottom")]
    public class ExtraBottomViewComponent : ViewComponent
    {
        private AuctionDbContext db;
        public ExtraBottomViewComponent(AuctionDbContext _category)
        {
            db = _category;
        }
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
          
            ViewBag.ExtraBottomItem = db.Items.Find(id);
            return View();
        }

    }
}
