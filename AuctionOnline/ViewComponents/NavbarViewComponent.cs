using AuctionOnline.Data;
using AuctionOnline.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionOnline.ViewComponents
{
    [ViewComponent(Name = "Navbar")]
    public class NavbarViewComponent : ViewComponent
    {
        private AuctionDbContext db;
        public NavbarViewComponent(AuctionDbContext _category)
        {
            db = _category;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Category> category = new List<Category>();
            List<Category> categories = db.Categories.ToList();
            category = categories
                .Where(c => c.ParentId == null)
                .Select(c => new Category()
                {
                    Id = c.Id,
                    Name = c.Name,
                    ParentId = c.ParentId,
                    Children = GetChildren(categories, c.Id)
                }).ToList();

            return View(category);
        }

        public static List<Category> GetChildren(List<Category> categories, int parentId)
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

        //private IList<Category> GetChildrenCate(IList<Category> cateList, int parentId)
        //{
        //    cateList = db.Categories.Where(x => x.ParentId == parentId).ToList();
        //    return cateList;
        //}

        //private Category GetCateItem(IList<Category> cateList, int id)
        //{
        //    cateList = (IList<Category>)db.Categories.FirstOrDefault(x => x.Id == id);
        //    return (Category)cateList;
        //}

        //private IList<CategoryVM> GetCate(IList<Category> menuList, int parentId)
        //{
        //    var children = GetChildrenCate(menuList, parentId);

        //    if (!children.Any())
        //    {
        //        return new List<CategoryVM>();
        //    }

        //    var vmList = new List<CategoryVM>();
        //    foreach (var item in children)
        //    {
        //        var menu = GetCateItem(menuList, item.Id);

        //        var vm = new CategoryVM();

        //        vm.Id = menu.Id;
        //        vm.Name = menu.Name;
        //        vm.Children = (HashSet<Category>)GetCate(menuList, menu.Id);

        //        vmList.Add(vm);
        //    }

        //    return vmList;
        //}
    }
}
