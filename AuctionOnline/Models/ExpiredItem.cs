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
        public DateTime ExpiredDate { get; set; }
        public int SessionId { get; set; }
        public bool IsSeen { get; set; }
    }
}
