using System.ComponentModel.DataAnnotations.Schema;

namespace AuctionOnline.Models
{
    public class BidIncrementDefinition
    {
        public int Id { get; set; }
        [Column(TypeName= "decimal(18,1)")]
        public decimal PriceRange { get; set; }
        [Column(TypeName = "decimal(18,1)")]
        public decimal BidIncrement { get; set; }

    }
}
