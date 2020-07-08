using System;

namespace AuctionOnline.ViewModels
{
    public class BidVM
    {
        public decimal CurrentBid { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
