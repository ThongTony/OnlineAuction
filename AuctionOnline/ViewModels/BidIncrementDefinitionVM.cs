using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionOnline.ViewModels
{
    public class BidIncrementDefinition
    {
        public int Id { get; set; }
        public decimal CurrentPrice { get; set; }
        public decimal BidIncrement { get; set; }
    }
}
