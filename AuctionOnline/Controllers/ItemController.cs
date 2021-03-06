﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuctionOnline.Data;
using AuctionOnline.Models;
using AuctionOnline.Utilities;
using AuctionOnline.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;

namespace AuctionOnline.Controllers
{
    public class ItemController : Controller
    {
        private readonly AuctionDbContext db;
        private LayoutViewModel layoutVM;
        private readonly IWebHostEnvironment webHostEnvironment;

        private const string photoPath = "images\\items";
        private const string documentPath = "documents\\items";

        public ItemController(AuctionDbContext _db, IWebHostEnvironment hostEnvironment)
        {
            db = _db;
            webHostEnvironment = hostEnvironment;
            layoutVM = new LayoutViewModel()
            {
                CategoriesVM = RecursiveMenu.GetRecursiveMenu(db)
            };
        }
        public async Task<IActionResult> ListItem()
        {
            if (HttpContext.Session.GetInt32("checkidAdmin") != null)
            {
                ViewBag.item = db.Items.ToList();
                return View("AdminListItem");
            }
            else if (HttpContext.Session.GetInt32("checkiduser") != null)
            {
                var items = db.Items.Include(i => i.Account);

                layoutVM.ItemsVM = ItemUtility.MapModelsToVMs(items.ToList());
                return View(layoutVM);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }


        }

        [HttpGet]
        public IActionResult Create()
        {
            if (HttpContext.Session.GetInt32("checkiduser") != null)
            {
                layoutVM.CategoryVM.Categories = db.Categories.Select(a =>
                                      new SelectListItem
                                      {
                                          Value = a.Id.ToString(),
                                          Text = a.Name
                                      }).ToList();
                return View(layoutVM);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ItemVM itemVM)
        {
            ViewBag.ItemAdd = "Failed";
            itemVM.CreatedAt = DateTime.Now;
            itemVM.Status = false;
            if (itemVM != null)
            {
                string fileName = await UploadFile.UploadAsync(webHostEnvironment, itemVM.Photo, photoPath);
                string documentName = await UploadFile.UploadAsync(webHostEnvironment, itemVM.Document, documentPath);
                var item = ItemUtility.MapVMToModel(itemVM);

                item.AccountId = (int)HttpContext.Session.GetInt32("checkiduser");
                item.MinimumBid = itemVM.MinimumBid;
                item.Photo = fileName;
                item.Document = documentName;
                item.CategoryItems = new List<CategoryItem>();

                foreach (var id in itemVM.SelectedCategoryIds)
                {
                    item.CategoryItems.Add(new CategoryItem
                    {
                        CategoryId = id,
                        ItemId = item.Id
                    });
                }
                db.Items.Add(item);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountId"] = new SelectList(db.Accounts, "Id", "Id", itemVM.AccountId);
            return View(itemVM);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await db.Items.FindAsync(id);

            layoutVM.ItemVM = ItemUtility.MapModelToVM(item);

            int[] selectedCategoryIds = db.CategoryItems.Where(x => x.ItemId == id).Select(i => i.CategoryId).ToArray();
            layoutVM.ItemVM.Categories = db.Categories.Select(a =>
                                  new SelectListItem
                                  {
                                      Value = a.Id.ToString(),
                                      Text = a.Name
                                  }).ToList();
            layoutVM.ItemVM.BidStatuses = new List<SelectListItem>()
            {
                new SelectListItem{
                                      Value = "0",
                                      Text = "Not Start"
                                  },
                new SelectListItem{
                                      Value = "1",
                                      Text = "Start bid"
                                  }
        };

            layoutVM.ItemVM.SelectedCategoryIds = selectedCategoryIds;
            if (item == null)
            {
                return NotFound();
            }
            return View(layoutVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ItemVM itemVM)
        {
            var item = await db.Items.FindAsync(itemVM.Id);
            if (itemVM.Photo != null)
            {
                string fileName = await UploadFile.UploadAsync(webHostEnvironment, itemVM.Photo, photoPath);
                item.Photo = fileName;
            }

            if (itemVM.Document != null)
            {
                string documentName = await UploadFile.UploadAsync(webHostEnvironment, itemVM.Document, documentPath);
                item.Document = documentName;
            }

            item.Title = itemVM.Title;
            item.Description = itemVM.Description;
            item.MinimumBid = itemVM.MinimumBid;
            item.CreatedAt = itemVM.CreatedAt;
            item.BidStatus = itemVM.BidStatus;
            item.BidEndDate = itemVM.BidEndDate;
            item.BidStartDate = itemVM.BidStartDate;
            if (itemVM.SelectedCategoryIds != null)
            {
                var categoryitems = db.CategoryItems.Where(c => c.ItemId == itemVM.Id).ToList();
                if (categoryitems.Count > 0)
                {
                    foreach (var categoryitem in categoryitems)
                    {
                        item.CategoryItems.Remove(categoryitem);
                    }
                }
                foreach (var cateId in itemVM.SelectedCategoryIds)
                {
                    item.CategoryItems.Add(new CategoryItem
                    {
                        CategoryId = cateId,
                        ItemId = item.Id,
                        CreatedAt = DateTime.Now
                    });
                }
                db.Items.Add(item);

                try
                {
                    db.Update(item);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                    }
                }

                return RedirectToAction("Detail", "Item", new { id = itemVM.Id });
            }

            layoutVM.ItemVM = itemVM;
            layoutVM.ItemVM.Categories = db.Categories.Select(a =>
                                 new SelectListItem
                                 {
                                     Value = a.Id.ToString(),
                                     Text = a.Name
                                 }).ToList();

            return View(layoutVM);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await db.Items
                .Include(i => i.Account)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            layoutVM.ItemVM = ItemUtility.MapModelToVM(item);

            return View(layoutVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(ItemVM itemVM)
        {
            if (HttpContext.Session.GetInt32("checkidAdmin") != null)
            {
                var item = await db.Items.FindAsync(itemVM.Id);

                var categoryitems = db.CategoryItems.Where(c => c.ItemId == itemVM.Id);

                foreach (var categoryitem in categoryitems)
                {
                    item.CategoryItems.Remove(categoryitem);
                }
                db.Items.Remove(item);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction("AdminListItem");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            if (HttpContext.Session.GetInt32("checkiduser") != null)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var item = await db.Items
                    .Include(i => i.Account)
                    .FirstOrDefaultAsync(m => m.Id == id);

                if (db.Bids.Any())
                {
                    item.Bids = db.Bids.Where(x => x.ItemId == id).OrderByDescending(x => x.CurrentBid).ToList();
                }

                if (item == null)
                {
                    return NotFound();
                }

                layoutVM.ItemVM = ItemUtility.MapModelToVM(item);
                //layoutVM.BidHistoriesVM = GetBidHistory(id);
                return View(layoutVM);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public async Task<IActionResult> ListedByCategory(int id)
        {
            var category = db.Categories.Find(id);
            //var c = category.Children;
            //var items = db.Items.FromSqlRaw(
            //    $"Select i.* from Categories c, CategoryItems ci, Items i where c.Id = ci.CategoryId and i.Id = ci.ItemId and c.Id = " + id);

            var categoryItems = db.CategoryItems.Where(x => x.CategoryId == category.Id).ToList();

            foreach (var categoryItem in categoryItems)
            {
                categoryItem.Item = db.Items.FirstOrDefault(x => x.Id == categoryItem.ItemId);
            }
            category.CategoryItems = categoryItems;
            layoutVM.CategoryVM = CategoryUtility.MapModeltoVM(category);
            return View(layoutVM);
        }
        public async Task<IActionResult> ListInShop()
        {
            if (HttpContext.Session.GetInt32("checkiduser") != null)
            {
                var username = HttpContext.Session.GetString("username");
                var account = db.Accounts.FirstOrDefault(a => a.Username.Equals(username));
                var items = db.Items.Where(i => i.AccountId == account.Id).ToList();

                layoutVM.ItemsVM = ItemUtility.MapModelsToVMs(items);

                return View(layoutVM);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public async Task<IActionResult> Pagination(int page = 1, int pageSize = 5)
        {
            PagedList<Item> item = new PagedList<Item>(db.Items, page, pageSize);
            return View(item);
        }

        private bool ItemExists(int id)
        {
            return db.Items.Any(e => e.Id == id);
        }

        [Produces("application/json")]
        [HttpGet]
        public async Task<IActionResult> Searchkeyword()
        {
            try
            {
                string term = HttpContext.Request.Query["term"].ToString();
                var postTitle = db.Items.Where(i => i.Title.Contains(term)).Select(i => i.Title).ToList();
                return Ok(postTitle);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult Searchkeyword(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                var searchkeyword = db.Items.Where(i => i.Title.Trim().Contains(keyword.Trim())).ToString();
                HttpContext.Session.SetString("searchkeyword", searchkeyword);
                var checkkeyword = db.Items.Where(i => i.Title.Trim().Contains(keyword.Trim())).ToList();

                if (checkkeyword != null)
                {
                    ViewBag.Success = checkkeyword;
                    ViewBag.keyword = keyword;
                    return View("ListBySearch", layoutVM);

                }
                else
                {
                    return View("ListBySearch", layoutVM);
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult ListBySearch()
        {
            return View(layoutVM);
        }

        public IActionResult Approveitem(ItemVM itemVM, Item item)
        {
            var checkid = db.Items.Find(itemVM.Id);
            if (checkid != null)
            {
                var i = db.Items.Where(i => i.Status == false);
                if (i != null)
                {
                    item = db.Items.Find(itemVM.Id);
                    item.Status = true;
                    db.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("AdminListItem");
                }
            }
            return View("AdminListItem");
        }

        public IActionResult Banitem(ItemVM itemVM, Item item)
        {
            var checkid = db.Items.Find(itemVM.Id);
            if (checkid != null)
            {
                var i = db.Items.Where(i => i.Status == true);
                if (i != null)
                {
                    item = db.Items.Find(itemVM.Id);
                    item.Status = false;
                    db.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("AdminListItem");
                }
            }
            return View("AdminListItem");
        }

        [HttpPost]
        public IActionResult SearchTitle(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                var checkkeyword = db.Items.Where(a => a.Title.Trim().Contains(keyword.Trim())).ToList();

                if (checkkeyword != null)
                {
                    ViewBag.item = checkkeyword;

                    return View("AdminListItem");

                }
                else
                {
                    return View("AdminListItem");
                }
            }
            else
            {
                return RedirectToAction("AdminListItem");
            }
        }

    }
}