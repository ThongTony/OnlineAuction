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
        public List<SelectListItem> Categories { get; set; }
        public int[] SelectedCategoryIds { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Status { get; set; }
        public string Photo { get; set; }
        public string Document { get; set; }
        public DateTime? BidStartDate { get; set; }
        public DateTime? BidEndDate { get; set; }
        public decimal? MinimumBid { get; set; }
        public ICollection<CategoryItem> CategoryItems { get; set; }
        //public int? BidIncrementDefinitionId { get; set; }
        //public BidIncrementDefinition BidIncrementDefinition { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
