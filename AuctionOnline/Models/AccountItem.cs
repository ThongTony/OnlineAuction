using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuctionOnline.Models
{
    public class AccountItem
    {
        public int ItemId { get; set; }
        public Item Item { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public bool BidStatus { get; set; }
        public decimal? CurrentBid { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
