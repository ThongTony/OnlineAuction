using AuctionOnline.ViewModels;
using AuctionOnline.Models;
using System.Collections.Generic;
using AuctionOnline.Data;

namespace AuctionOnline.Utilities
{
    public static class CategoryUtility
    {
        public static Category MapVMtoModel(CategoryVM categoryVM)
        {
            var model = new Category()
            {
                //Id = categoryVM.Id,
                Name = categoryVM.Name,
                ParentId = categoryVM.ParentId,
                Parent = categoryVM.Parent,
                CreatedAt = categoryVM.CreatedAt,
                Level = categoryVM.Level
            };

            return model;
        }

        public static CategoryVM MapModeltoVM(Category category)
        {
            var viewModel = new CategoryVM()
            {
                Id = category.Id,
                Name = category.Name,
                ParentId = category.ParentId,
                CreatedAt = category.CreatedAt,
                Parent = category.Parent,
                Level = category.Level,
                CategoryItems = category.CategoryItems 
            };

            return viewModel;
        }

        public static List<Category> MapVMsToModels(List<CategoryVM> categoriesVM)
        {
            var categories = new List<Category>();
            foreach (var categoryVM in categoriesVM)
            {

                var category = new Category()
                {
                    Id = categoryVM.Id,
                    Name = categoryVM.Name,
                    ParentId = categoryVM.ParentId,
                    CreatedAt = categoryVM.CreatedAt,
                    Parent = categoryVM.Parent,
                    Level = categoryVM.Level
                };

                categories.Add(category);

            }
            return categories;
        }

        public static List<CategoryVM> MapModelsToVMs(List<Category> categories)
        {
            var categoriesVM = new List<CategoryVM>();
            foreach (var category in categories)
            {
                var categoryVM = new CategoryVM()
                {
                    Id = category.Id,
                    Name = category.Name,
                    ParentId = category.ParentId,
                    CreatedAt = category.CreatedAt,
                    Parent = category.Parent,
                    Children = category.Children,
                    Level = category.Level
                };
                categoriesVM.Add(categoryVM);
            }
            return categoriesVM;
        }
    }
}
