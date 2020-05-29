using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionOnline.Models
{
    public class AccountItem
    {
        public int ItemId { get; set; }
        public Item Item { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }
    }
}
