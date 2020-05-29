using Microsoft.AspNetCore.Http;
using System;

namespace AuctionOnline.ViewModels
{
    public class Item
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int BidStatus { get; set; }
        public IFormFile Photo { get; set; }
        public IFormFile Document { get; set; }
        public DateTime BidStartDate { get; set; }
        public DateTime BidEndDate { get; set; }
        public int BidIncrementId { get; set; }
        public decimal MinimumBid { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
