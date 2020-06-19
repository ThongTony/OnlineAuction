using AuctionOnline.Models;
using System.Collections.Generic;

namespace AuctionOnline.ViewModels
{
    public class LayoutViewModel
    {
        public LayoutViewModel()
        {
            CategoryVM = new CategoryVM();
            ItemVM = new ItemVM();
            AccountVM = new AccountVM();
            CategoriesVM = new List<CategoryVM>();
            Categories = new List<Category>();
        }

        public CategoryVM CategoryVM { get; set; }
        public ItemVM ItemVM { get; set; }
        public AccountVM AccountVM { get; set; }
        public List<CategoryVM> CategoriesVM { get; set; }
        public List<Category> Categories { get; set; }

    }
}
