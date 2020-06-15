using AuctionOnline.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AuctionOnline.Controllers
{
    [Route("home")]
    public class HomeController : Controller
    {

        private readonly AuctionDbContext db;
        public HomeController(AuctionDbContext _db)
        {
            db = _db;
        }        
        [Route("~/")]
        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            ViewBag.IsHome = true;
            ViewBag.Categories = db.Categories.Where(c => c.ParentId == null).ToList();
            ViewBag.Items = db.Items.ToList();
            return View();
        }
        [Route("logout")]
        public IActionResult Logout()
        {
            return View("index");
        }
    }
}