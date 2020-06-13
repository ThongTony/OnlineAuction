using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using AuctionOnline.Data;
using AuctionOnline.Models;
using AuctionOnline.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            ViewBag.Item = db.Items.ToList();
            return View();
        }
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
                string fileName = await UploadAsync(itemVM.Photo, photoPath);
                string documentName = await UploadAsync(itemVM.Document, documentPath);
                var item = new Item()
                {
                    Title = itemVM.Title,
                    Price = itemVM.Price,
                    AccountId = itemVM.AccountId,
                    Photo = fileName,
                    Document = documentName,
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
                //item.AccountId = iduser;

                db.Items.Add(item);
                db.SaveChanges();
                ViewBag.Item = "Success";
            }
            return RedirectToAction("Add");
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

        public async Task<IActionResult> Delete(int id)
        {
            var item = db.Items.Find(id);
            
            var categoryitems = db.CategoryItems.Where(c => c.ItemId == id);
            foreach (var categoryitem in categoryitems)
            {
                item.CategoryItems.Remove(categoryitem);
            }
            db.Items.Remove(item);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

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