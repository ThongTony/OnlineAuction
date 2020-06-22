using AuctionOnline.ViewModels;
using AuctionOnline.Models;
using System.Collections.Generic;

namespace AuctionOnline.Utilities
{
    public static class ItemUtility
    {
        public static Item MapVMToModel(ItemVM itemVM)
        {
            var model = new Item()
            {
                Id = itemVM.Id,
                Title = itemVM.Title,
                Description = itemVM.Description,
                Status= itemVM.Status,
                Account = itemVM.Account,
                Photo = itemVM.PhotoName,
                Document = itemVM.DocumentName,

                BidStatus = itemVM.BidStatus,
                BidStartDate= itemVM.BidStartDate,
                BidEndDate= itemVM.BidEndDate,
                MinimumBid= itemVM.MinimumBid,
                BidIncrement= itemVM.BidIncrement,
                CreatedAt = itemVM.CreatedAt,               
            };

            return model;
        }

        public static ItemVM MapModelToVM(Item item)
        {
            var viewModel = new ItemVM()
            {
                Id = item.Id,
                Title = item.Title,
                Description = item.Description,
                Status = item.Status,
                Account = item.Account,

                PhotoName = item.Photo,
                DocumentName = item.Document,

                BidStatus = item.BidStatus,
                BidStartDate = item.BidStartDate,
                BidEndDate = item.BidEndDate,
                MinimumBid = item.MinimumBid,
                BidIncrement = item.BidIncrement,
                CreatedAt = item.CreatedAt,
                Bids = item.Bids
            };

            return viewModel;
        }

        public static List<Item> MapVMsToModels(List<ItemVM> itemsVM)
        {
            var items = new List<Item>();
            foreach (var itemVM in itemsVM)
            {
                var item = new Item()
                {
                    Id = itemVM.Id,
                    Title = itemVM.Title,
                    Description = itemVM.Description,
                    Status = itemVM.Status,
                    Account = itemVM.Account,

                    Photo = itemVM.PhotoName,
                    Document = itemVM.DocumentName,

                    BidStatus = itemVM.BidStatus,
                    BidStartDate = itemVM.BidStartDate,
                    BidEndDate = itemVM.BidEndDate,
                    MinimumBid = itemVM.MinimumBid,
                    BidIncrement = itemVM.BidIncrement,
                    CreatedAt = itemVM.CreatedAt,
                };

                items.Add(item);
            }
            return items;
        }

        public static List<ItemVM> MapModelsToVMs(List<Item> items)
        {
            var itemsVM = new List<ItemVM>();
            foreach (var item in items)
            {
                var itemVM = new ItemVM()
                {
                    Id = item.Id,
                    Title = item.Title,
                    Description = item.Description,
                    Status = item.Status,
                    Account = item.Account,

                    PhotoName = item.Photo,
                    DocumentName = item.Document,

                    BidStatus = item.BidStatus,
                    BidStartDate = item.BidStartDate,
                    BidEndDate = item.BidEndDate,
                    MinimumBid = item.MinimumBid,
                    BidIncrement = item.BidIncrement,
                    CreatedAt = item.CreatedAt,
                };
                itemsVM.Add(itemVM);
            }
            return itemsVM;
        }
    }
}
