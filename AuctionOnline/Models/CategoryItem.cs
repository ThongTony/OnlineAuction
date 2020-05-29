using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionOnline.Models
{
    public class CategoryItem
    {
        public int ItemId { get; set; }
        public Item Item{ get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
