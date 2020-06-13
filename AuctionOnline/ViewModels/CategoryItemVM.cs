using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionOnline.Models
{
    public class CategoryItemVM
    {
        public int ItemId { get; set; }
        public Item Item{ get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
