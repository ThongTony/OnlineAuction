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
            //ViewBag.NavCategories = db.Categories.FirstOrDefault(a => a.ParentId == id);
            ViewBag.NavCategories = db.Categories.ToList();
            return View();//GetMenuItem(ViewBag.NavCategories, null)
        }
        private IList<Category> GetChildrenMenu(IList<Category> menuList, int parentId)
        {
            menuList = db.Categories.Where(x => x.ParentId == parentId).ToList();
            return menuList;
        }
        private Category GetMenuItem(IList<Category> menu, int id)
        {
            menu = (IList<Category>)db.Categories.FirstOrDefault(x => x.Id == id);
            return (Category)menu;
        }
    }
}
