using System;
using System.ComponentModel.DataAnnotations;

namespace AuctionOnline.Models
{
    public class ExpiredItem
    {
        [Key]
        public Guid Id { get; set; }
        public int ItemId { get; set; }
        public Item Item { get; set; }
        public DateTime CurrentDate { get; set; }
        public bool IsExpired { get; set; }
        public bool IsSeen { get; set; }
    }
}
