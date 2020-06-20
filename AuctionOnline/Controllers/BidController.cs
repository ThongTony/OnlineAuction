using AuctionOnline.Data;
using AuctionOnline.Models;
using AuctionOnline.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionOnline.Controllers
{
    public class BidController : Controller
    {
        private readonly AuctionDbContext db;

        public BidController(AuctionDbContext _db)
        {
            db = _db;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ItemVM itemVM)
        {
            if (ModelState.IsValid)
            {
                var bidSession = 1;

                var latestSessionBid = db.Bids.Where(x => x.ItemId == itemVM.Id).OrderByDescending(x => x.BidSession).FirstOrDefault();

                if (latestSessionBid != null)
                {
                    var latestCurrentPriceBid = db.Bids.Where(x => x.BidSession == latestSessionBid.BidSession).OrderByDescending(x => x.CurrentBid).FirstOrDefault();

                    if (latestCurrentPriceBid != null)
                    {
                        if (!latestCurrentPriceBid.IsWinned)
                        {
                            bidSession = latestCurrentPriceBid.BidSession;
                        }
                        else
                        {
                            bidSession = latestCurrentPriceBid.BidSession + 1;
                        }
                    }
                }

                var availableBid = new Bid
                {
                    AccountId = 1,
                    ItemId = itemVM.Id,
                    CurrentBid = itemVM.BidPrice,
                    BidSession = bidSession,
                    BidStartDate = itemVM.BidStartDate.Value,
                    BidEndDate = itemVM.BidEndDate.Value,
                    CreatedAt = DateTime.Now
                };

                db.Bids.Add(availableBid);
                await db.SaveChangesAsync();
            }

            return RedirectToAction("Detail", "Item", new { id = itemVM.Id });
        }

        [HttpGet]
        public async Task<Boolean> EndBid(int itemId)
        {
            var latestSessionBid = db.Bids.Where(x => x.ItemId == itemId).OrderByDescending(x => x.BidSession).FirstOrDefault();

            var latestCurrentPriceBid = db.Bids.Where(x => x.BidSession == latestSessionBid.BidSession).OrderByDescending(x => x.CurrentBid).FirstOrDefault();

            if (latestCurrentPriceBid != null)
            {
                latestCurrentPriceBid.IsWinned = true;
                latestCurrentPriceBid.IsWinnedDateTime = DateTime.Now;

                db.Bids.Update(latestCurrentPriceBid);

                await db.SaveChangesAsync();

                return latestCurrentPriceBid.IsWinned;
            }

            return false;
        }

    }
}