using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionOnline.Models
{
    public class NotificationProduct
    {
        [Key]
        public Guid Id { get; set; }
        public bool IsDelete { get; set; }
        [Required]
        public string Name { get; set; }
        public double Price { get; set; }
        public bool IsAvailable { get; set; }
    }
}
