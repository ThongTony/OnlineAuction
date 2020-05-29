using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionOnline.Models
{
    public class BidIncrementDefinition
    {
        public int Id { get; set; }
        [Column(TypeName= "decimal(18,1)")]
        public decimal CurrentPrice { get; set; }
        [Column(TypeName = "decimal(18,1)")]
        public decimal BidIncrement { get; set; }
    }
}
