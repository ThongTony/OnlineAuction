using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AuctionOnline.Data;
using AuctionOnline.Models;
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
            var getAllItem = db.Items.Include(i => i.Account);

            return View(await getAllItem.ToListAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewData["AccountId"] = new SelectList(db.Accounts, "Id", "Id");
            var model = new ItemVM();
            model.Categories = db.Categories.Select(a =>
                                  new SelectListItem
                                  {
                                      Value = a.Id.ToString(),
                                      Text = a.Name
                                  }).ToList();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ItemVM itemVM)
        {
            ViewBag.Item = "Failed";
            itemVM.CreatedAt = DateTime.Now;

            if (itemVM != null)
            {
                string fileName = await UploadAsync(itemVM.Photo, photoPath);
                string documentName = await UploadAsync(itemVM.Document, documentPath);
                var item = new Item()
                {
                    Title = itemVM.Title,
                    Price = itemVM.Price,
                    AccountId = 1 /*itemVM.AccountId*/,
                    Photo = fileName,
                    Document = documentName,
                    CreatedAt = itemVM.CreatedAt,
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
                db.Items.Add(item);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(ListInShop));
            }
            ViewData["AccountId"] = new SelectList(db.Accounts, "Id", "Id", itemVM.AccountId);
            return View(itemVM);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await db.Items.FindAsync(id);
            //var itemVM = new ItemVM()
            //{
            //    Title = item.Title,
            //    Price = item.Price,
            //    AccountId = 1 /*itemVM.AccountId*/,
            //    Photo = item.Photo,
            //    Document = documentName,
            //    CreatedAt = item.CreatedAt,
            //    CategoryItems = new List<CategoryItem>()
            //};
            if (item == null)
            {
                return NotFound();
            }
            ViewData["AccountId"] = new SelectList(db.Accounts, "Id", "Id", item.AccountId);
            return View(item);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, /*[Bind("Id,Title,Description,Price,Status,Photo,Document,AccountId,CreatedAt")]*/ Item item)
        {
            if (id != item.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
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
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountId"] = new SelectList(db.Accounts, "Id", "Id", item.AccountId);
            return View(item);
        }

        public async Task<IActionResult> Delete(int? id)
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

            return View(item);
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

        public async Task<IActionResult> Details(int? id)
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

            return View(item);
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



        [HttpGet]
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
                    return View("ListBySearch");

                }
                else
                {
                    return View("ListBySearch");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult ListBySearch()
        {
            return View();
        }
    }
}