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
        public int AccountId { get; set; }
        public IFormFile Photo { get; set; }
        public IFormFile Document { get; set; }

        public ICollection<CategoryItemVM> CategoryItems { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
