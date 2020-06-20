using AuctionOnline.Models;
using System.Collections.Generic;

namespace AuctionOnline.ViewModels
{
    public class LayoutViewModel
    {
        public LayoutViewModel()
        {
            CategoryVM = new CategoryVM();
            CategoriesVM = new List<CategoryVM>();

            ItemVM = new ItemVM();
            ItemsVM = new List<ItemVM>();

            AccountVM = new AccountVM();

            Categories = new List<Category>();

            Bid = new Bid();
        }

        public CategoryVM CategoryVM { get; set; }
        public List<CategoryVM> CategoriesVM { get; set; }

        public ItemVM ItemVM { get; set; }
        public List<ItemVM> ItemsVM { get; set; }

        public AccountVM AccountVM { get; set; }

        public List<Category> Categories { get; set; }
        public Bid Bid { get; set; }

    }
}
