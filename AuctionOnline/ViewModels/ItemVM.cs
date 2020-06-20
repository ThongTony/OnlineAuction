using AuctionOnline.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace AuctionOnline.ViewModels
{
    public class ItemVM
    {
        public int Id { get; set; }       
        public string Title { get; set; }       
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool Status { get; set; }      

        public IFormFile Photo { get; set; }
        public string PhotoName { get; set; }
        public IFormFile Document { get; set; }
        public string DocumentName { get; set; }

        public int AccountId { get; set; }
        public Account Account { get; set; }

        public int BidStatus { get; set; }
        public DateTime? BidStartDate { get; set; }
        public DateTime? BidEndDate { get; set; }
        public decimal? MinimumBid { get; set; }
        public decimal BidIncrement { get; set; }

        public List<SelectListItem> Categories { get; set; }
        public int[] SelectedCategoryIds { get; set; }

        public ICollection<CategoryItem> CategoryItems { get; set; }
        public ICollection<Bid> Bids { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
