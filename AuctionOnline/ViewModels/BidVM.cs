using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionOnline.ViewModels
{
    public class Bid
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int ItemId { get; set; }
        public decimal CurrentBid { get; set; }
        public DateTime? BidStartDate { get; set; }
        public DateTime? BidEndDate { get; set; }
        public decimal? MinimumBid { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
