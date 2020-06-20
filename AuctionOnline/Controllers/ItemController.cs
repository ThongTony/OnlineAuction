 using System;
using System.Collections.Generic;
using System.IO;
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
        private readonly IWebHostEnvironment webHostEnvironment;

        private const string photoPath = "images\\items";
        private const string documentPath = "documents\\items";

        public ItemController(AuctionDbContext _db, IWebHostEnvironment hostEnvironment)
        {
            db = _db;
            webHostEnvironment = hostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            var items = db.Items.Include(i => i.Account);
            var viewVM = new LayoutViewModel();
            viewVM.ItemsVM = ItemUtility.MapModelsToVMs(items.ToList());
            return View(viewVM);
        }

        [HttpGet]
        public IActionResult Create()
        {
            //ViewData["AccountId"] = new SelectList(db.Accounts, "Id", "Id");
            var viewmodel = new LayoutViewModel();
            viewmodel.CategoryVM.Categories = db.Categories.Select(a =>
                                  new SelectListItem
                                  {
                                      Value = a.Id.ToString(),
                                      Text = a.Name
                                  }).ToList();
            return View(viewmodel);
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
                string fileName = await UploadAsync(itemVM.Photo, photoPath);
                string documentName = await UploadAsync(itemVM.Document, documentPath);
                var item = ItemUtility.MapVMToModel(itemVM);

                item.AccountId = 1; /*itemVM.AccountId*/
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
            if (item == null)
            {
                return NotFound();
            }
            var layoutVM = new LayoutViewModel();
            layoutVM.ItemVM = ItemUtility.MapModelToVM(item);
            layoutVM.ItemVM.Categories = db.Categories.Select(a =>
                                  new SelectListItem
                                  {
                                      Value = a.Id.ToString(),
                                      Text = a.Name
                                  }).ToList();

            return View(layoutVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ItemVM itemVM)
        {
            if (ModelState.IsValid)
            {
                if (id != itemVM.Id)
                {
                    return NotFound();
                }
                string fileName = await UploadAsync(itemVM.Photo, photoPath);
                string documentName = await UploadAsync(itemVM.Document, documentPath);

                var item = await db.Items.FindAsync(id);
                //map vm to model
                //item = ItemUtility.MapVMToModel(itemVM);
                item.Title = itemVM.Title;
                item.Description = itemVM.Description;
                item.Price = itemVM.Price;
                item.AccountId = itemVM.AccountId;
                item.Photo = fileName;
                item.Document = documentName;
                item.CreatedAt = itemVM.CreatedAt;


                //update selected Categories' Ids
                if (itemVM.SelectedCategoryIds != null)
                {
                    //remove related categories
                    var categoryitems = db.CategoryItems.Where(c => c.ItemId == id);
                    foreach (var categoryitem in categoryitems)
                    {
                        item.CategoryItems.Remove(categoryitem);
                    }
                    //add selected categories
                    foreach (var cateId in itemVM.SelectedCategoryIds)
                    {
                        item.CategoryItems.Add(new CategoryItem
                        {
                            CategoryId = cateId,
                            ItemId = item.Id
                        });
                    }
                    db.Items.Add(item);
                }
                else
                {
                    item.CategoryItems = itemVM.CategoryItems;
                }

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
                return RedirectToAction(nameof(Index));
            }
            //ViewData["AccountId"] = new SelectList(db.Accounts, "Id", "Id", itemVM.AccountId);
            return View(itemVM);
        }

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

            var viewlayout = new LayoutViewModel()
            {
                ItemVM = ItemUtility.MapModelToVM(item)
            };
            return View(viewlayout);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await db.Items.FindAsync(id);

            var categoryitems = db.CategoryItems.Where(c => c.ItemId == id);
            foreach (var categoryitem in categoryitems)
            {
                item.CategoryItems.Remove(categoryitem);
            }
            db.Items.Remove(item);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Detail(int id)
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

            var viewmodel = new LayoutViewModel()
            {
                ItemVM = ItemUtility.MapModelToVM(item)
            };

            return View(viewmodel);
        }

        public IActionResult Model()
        {
            var items = db.Items.ToList();
            return View(items);
        }
        public async Task<IActionResult> ListedByCategory(int id)
        {
            ViewBag.Category = db.Categories.Find(id);
            ViewBag.Items = db.Items.FromSqlRaw(
                $"Select i.* from Categories c, CategoryItems ci, Items i where c.Id = ci.CategoryId and i.Id = ci.ItemId and c.Id = " + id);
            return View();
        }
        public async Task<IActionResult> ListInShop()
        {
            var username = HttpContext.Session.GetString("username");
            var account = db.Accounts.FirstOrDefault(a => a.Username.Equals(username));
            ViewBag.Items = db.Items.Where(i => i.AccountId == account.Id).ToList();
            return View();
        }

        private async Task<string> UploadAsync(IFormFile fileType, string path)
        {

            string uniqueFileName = null;

            if (fileType != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, path);
                uniqueFileName = Guid.NewGuid().ToString() + "_" + fileType.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                Directory.CreateDirectory(uploadsFolder);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await fileType.CopyToAsync(fileStream);
                }
            }
            return uniqueFileName;
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
    }
}