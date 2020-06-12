using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuctionOnline.Data;
using AuctionOnline.Models;
using AuctionOnline.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AuctionOnline.Controllers
{
    public class ItemController : Controller
    {
        private AuctionDbContext db;
        public ItemController(AuctionDbContext _db)
        {
            db = _db;
        }
        //public async Task<IActionResult> DemoIndex()
        //{
        //    ViewBag.Item = db.Items.ToList();
        //    return View();
        //}
        //public async Task<IActionResult> Details(int id)
        //{
        //    ViewBag.isBreadCrumb = true;
        //    ViewBag.DetailItem = db.Items.Find(id);
        //    return View("Details");
        //}
        //public async Task<IActionResult> ListedByCategory(int id)
        //{
        //    ViewBag.isBreadCrumb = true;
        //    ViewBag.CategoryItem = db.Categories.Where(i => i.Id == id).ToList();
        //    return View();
        //}
        //public async Task<IActionResult> ListInShop(int id)
        //{
        //    ViewBag.isBreadCrumb = true;
        //    ViewBag.AccountItem = db.Accounts.Where(i => i.Id == id);
        //    return View();
        //}

        [HttpGet]
        public IActionResult Add(int id)
        {
            var model = new ItemVM();
            ViewBag.Account = db.Accounts.Find(id);
            //ViewBag.Category = db.Categories.ToList();
            model.Categories = db.Categories.Select(a =>
                                  new SelectListItem
                                  {
                                      Value = a.Id.ToString(),
                                      Text = a.Name
                                  }).ToList();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ItemVM itemVM)
        {
            ViewBag.Item = "Failed";
            itemVM.CreatedAt = DateTime.Now;
            
            if (itemVM != null)
            {
                var item = new Item()
                {
                    Title = itemVM.Title,
                    Price = itemVM.Price,
                    CategoryItems = new List<CategoryItem>()
                };
                
                foreach (var id in itemVM.SelectedCategoryIds)
                {
                    item.CategoryItems.Add(new CategoryItem
                    {
                        CategoryId = id,
                        ItemId = item.Id

                    });
                }
                //var iduser = 
                //item.AccountId = 1;

                db.Items.Add(item);
                db.SaveChanges();
                ViewBag.Item = "Success";
            }
            return RedirectToAction("Add");
        }

        //public async Task<IActionResult> Delete(int id)
        //{
        //    var items = db.Items.Find(id);
        //    db.Items.Remove(items);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //[HttpGet]
        //public async Task<IActionResult> Edit(int id)
        //{
        //    var item = db.Items.Find(id);
        //    return View("List", item);
        //}
        //[HttpPost]
        //public async Task<IActionResult> Edit(Item item)
        //{
        //    db.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        //    db.SaveChanges();
        //    return RedirectToAction("Edit", "item");
        //}
        //public IActionResult Odertracking()
        //{
        //    ViewBag.isBreadCrumb = true;
        //    return View();
        //}
        //public IActionResult Featured()
        //{
        //    ViewBag.isBreadCrumb = true;
        //    return View();
        //}
    }
}