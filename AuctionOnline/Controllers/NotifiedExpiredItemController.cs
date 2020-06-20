using System.Linq;
using AuctionOnline.Data;
using Microsoft.AspNetCore.Mvc;

namespace AuctionOnline.Controllers
{
    public class NotifiedExpiredItemController : Controller
    {
        private AuctionDbContext db;
        public NotifiedExpiredItemController(AuctionDbContext _db)
        {
            db = _db;
        }

        [HttpGet]
        public IActionResult GetExpiredItems()
        {
            return Ok(db.ExpiredItems.OrderByDescending(x => x.Id).ToList());
        }
        public IActionResult Index()
        {
            return View();
        }

    }
}