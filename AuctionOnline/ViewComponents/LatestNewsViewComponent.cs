using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AuctionOnline.ViewComponents
{
    [ViewComponent(Name = "LatestNews")]
    public class LatestNewsViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {

            return View("Index");
        }
    }
}
