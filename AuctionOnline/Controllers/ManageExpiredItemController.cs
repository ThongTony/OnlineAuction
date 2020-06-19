using System;
using System.Linq;
using System.Threading.Tasks;
using AuctionOnline.Data;
using AuctionOnline.Notifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace AuctionOnline.Controllers
{
    public class ManageExpiredItemController : Controller
    {
        private AuctionDbContext dbContext;
        private readonly IHubContext<SignalRHub> hubContext;

        public ManageExpiredItemController(AuctionDbContext dbContext, IHubContext<SignalRHub> hubContext)
        {
            this.dbContext = dbContext;
            this.hubContext = hubContext;
        }
        public IActionResult Index()
        {
            // Nhớ filter theo AccountId
            return View(dbContext.ExpiredItems.ToList());
        }

        [HttpGet]
        public IActionResult DeleteExpiredItem(Guid productId)
        {
            var product = dbContext.ExpiredItems.Find(productId);
            dbContext.ExpiredItems.Remove(product);
            dbContext.SaveChanges();
            hubContext.Clients.All.SendAsync("refreshNotifications");
            return RedirectToAction("index");
        }

        [HttpGet]
        public async Task<IActionResult> MarkAllAsRead()
        {
            // Nhớ filter theo AccountId
            var items = dbContext.ExpiredItems;
            foreach (var item in items)
            {
                item.IsSeen = true;
            }

            dbContext.SaveChanges(); 
            await hubContext.Clients.All.SendAsync("refreshNotifications");
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> MarkNotificationAsRead(Guid id)
        {
            var item = dbContext.ExpiredItems.FirstOrDefault(x => x.Id == id);
            item.IsSeen = true;

            dbContext.SaveChanges();
            await hubContext.Clients.All.SendAsync("refreshNotifications");
            return Ok();
        }

    }
}