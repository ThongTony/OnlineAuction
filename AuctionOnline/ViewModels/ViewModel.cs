using AuctionOnline.Models;
using System.Collections.Generic;

namespace AuctionOnline.ViewModels
{
    public class ViewModel
    {
        public List<Category> CategoryViewModel { get; set; }
        public List<Item> ItemViewModel { get; set; }
        public List<Account> AccountViewModel { get; set; }

    }
}
