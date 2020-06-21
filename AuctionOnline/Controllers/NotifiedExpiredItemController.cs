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
            // Nhớ filter theo AccountId

            var expiredItems = db.ExpiredItems.OrderByDescending(x => x.Id).Take(7).ToList();
            
            return Json(new { expiredItems = expiredItems, notSeen = expiredItems.Where(x => !x.IsSeen).Count() });
        }
    }
}