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
        public int BidStatus { get; set; }
        public string Photo { get; set; }
        public string Document { get; set; }
        public DateTime BidStartDate { get; set; }
        public DateTime BidEndDate { get; set; }
        public int BidIncrementId { get; set; }
        [Column(TypeName="decimal(18,1)")]
        public decimal MinimumBid { get; set; }
        public int BidIncrementDefinitionId { get; set; }
        public BidIncrementDefinition BidIncrementDefinition { get; set; }
        public ICollection<CategoryItem> CategoryItems { get; set; }
        public ICollection<AccountItem> AccountItems { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
