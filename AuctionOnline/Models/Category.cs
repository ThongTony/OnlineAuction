using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionOnline.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt
        {
            get
            {
                return this.createdAt.HasValue 
                    ? this.createdAt.Value 
                    : DateTime.Now;
            }
            set { this.createdAt = value; }
        }
        private DateTime? createdAt = null;
        public int? ParentId { get; set; }
        public virtual Category Parent { get; set; }
        public virtual List<Category> Children { get; set; }
        public ICollection<CategoryItem> CategoryItems { get; set; }
    }
}
