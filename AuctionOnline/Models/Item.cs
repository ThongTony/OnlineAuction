using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuctionOnline.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [Column(TypeName = "decimal(18,1)")]
        public decimal Price { get; set; }
        public int Status { get; set; }
        public string Photo { get; set; }
        public string Document { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }

        public ICollection<CategoryItem> CategoryItems { get; set; }
        public ICollection<Bid> Bids { get; set; }
        public DateTime CreatedAt { get; set; }

        public bool BidStatus { get; set; }
        public DateTime? BidStartDate { get; set; }
        public DateTime? BidEndDate { get; set; }

        [Column(TypeName = "decimal(18,1)")]
        public decimal? MinimumBid { get; set; }

        [Column(TypeName = "decimal(18,1)")]
        public decimal BidIncrement { get; set; }

    }
}
