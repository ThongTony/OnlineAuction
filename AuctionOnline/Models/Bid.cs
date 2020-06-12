using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuctionOnline.Models
{
    public class Bid
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public Item Item { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }      

        public bool BidStatus { get; set; }
        public DateTime? BidStartDate { get; set; }
        public DateTime? BidEndDate { get; set; }

        [Column(TypeName = "decimal(18,1)")]
        public decimal? MinimumBid { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal CurrentBid { get; set; }

        public int BidIncrementDefinitionId { get; set; }
        public BidIncrementDefinition BidIncrementDefinition { get; set; }
     
        public DateTime CreatedAt { get; set; }
    }
}
