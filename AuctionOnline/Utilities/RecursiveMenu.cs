using AuctionOnline.ViewModels;
using AuctionOnline.Models;
using System.Collections.Generic;
using System.Linq;
using AuctionOnline.Data;

namespace AuctionOnline.Utilities
{
    public static class RecursiveMenu
    {
        public static List<CategoryVM> GetRecursiveMenu(AuctionDbContext db)
        {
            //Recursive
            List<Category> category = new List<Category>();
            List<Category> categories = db.Categories.OrderByDescending(c => c.Children.Any()).ToList();
            category = categories
                .Where(c => c.ParentId == null)
                .Select(c => new Category()
                {
                    Id = c.Id,
                    Name = c.Name,
                    ParentId = c.ParentId,
                    Children = GetChildren(categories, c.Id)
                }).ToList();

            return CategoryUtility.MapModelsToVMs(category);
        }

        private static List<Category> GetChildren(List<Category> categories, int parentId)
        {
            return categories
                .Where(c => c.ParentId == parentId)
                .Select(c => new Category
                {
                    Id = c.Id,
                    Name = c.Name,
                    ParentId = c.ParentId,
                    Parent = c,
                    Children = GetChildren(categories, c.Id)
                }).ToList();
        }
    }
}
