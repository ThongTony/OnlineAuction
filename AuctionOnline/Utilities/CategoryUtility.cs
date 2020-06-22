using AuctionOnline.ViewModels;
using AuctionOnline.Models;
using System.Collections.Generic;

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
                    Parent = categoryVM.Parent,//MapVMtoModel(categoryVM)
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
                    Children = MapModelsToVMs(category.Children)
                };
                categoriesVM.Add(categoryVM);
            }
            return categoriesVM;
        }
    }
}
