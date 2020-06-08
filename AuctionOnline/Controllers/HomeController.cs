using Microsoft.AspNetCore.Mvc;

namespace AuctionOnline.Controllers
{

    [Route("home")]
    public class HomeController : Controller
    {
        [Route("~/")]
        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            return View();
        }
        [Route("about")]
        public IActionResult About()
        {
            return View();
        }
        [Route("contact")]
        public IActionResult Contact()
        {
            return View();
        }
    }
}