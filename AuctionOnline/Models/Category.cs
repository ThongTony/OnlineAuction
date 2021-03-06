﻿using System;
using System.Collections.Generic;

namespace AuctionOnline.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Level { get; set; }
        public int? ParentId { get; set; }
        public virtual Category Parent { get; set; }
        public virtual List<Category> Children { get; set; }
        public ICollection<CategoryItem> CategoryItems { get; set; }
    }
}
