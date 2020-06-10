using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using AuctionOnline.Data;
using AuctionOnline.Models;
using Microsoft.AspNetCore.Mvc;

namespace AuctionOnline.Controllers
{
    [Route("item")]
    public class ItemController : Controller
    {
        private AuctionDbContext db;
        public ItemController(AuctionDbContext _db)
        {
            db = _db;
        }
        public IActionResult AddCategoryItem()
        {
            var add = db.CategoryItems.ToList();
            return View();
        }
        [Route("index")]
        public IActionResult DemoIndex()
        {
            ViewBag.Item = db.Items.ToList();
            return View();
        }
        [Route("details/{id}")]
        public IActionResult Details(int id)
        {
            ViewBag.isBreadCrumb = true;
            ViewBag.DetailItem = db.Items.Find(id);
            return View("Details");
        }
        [Route("listedbycategory")]
        public IActionResult ListedByCategory(int id)
        {
            ViewBag.isBreadCrumb = true;
            ViewBag.CategoryItem = db.Categories.Where(i => i.Id == id).ToList();
            return View();
        }
        [Route("listinshop")]
        public IActionResult ListInShop(int id)
        {
            ViewBag.isBreadCrumb = true;
            ViewBag.AccountItem = db.Accounts.Where(i => i.Id == id);
            return View();
        }
        [HttpGet]
        [Route("add/{id}")]
        public IActionResult Add(int id)
        {
            ViewBag.Account = db.Accounts.Find(id);
            ViewBag.Category = db.Categories.ToList();
            return View("DemoAdd", new Item());
        }
        [HttpPost]
        [Route("add")]
        public IActionResult Add(Item items, int[] id)
        {
            ViewBag.Item = "Failed";
            items.CreatedAt = DateTime.Now;
            CategoryItem ci = new CategoryItem();
            for (int i = 0; i < id.Count(); i++)
            {
                ci.CategoryId = id[i];
            }
            if (items != null)
            {
                db.CategoryItems.Add(ci);
                db.Categories.Find(id);
                db.Items.Add(items);                
                db.SaveChanges();
                ViewBag.Item = "Success";
            }
            return RedirectToAction("Add");
        }
        [Route("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var items = db.Items.Find(id);
            db.Items.Remove(items);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("edit/{id}")]
        public IActionResult Edit(int id)
        {
            var item = db.Items.Find(id);
            return View("List", item);
        }
        [HttpPost]
        [Route("edit")]
        public IActionResult Edit(Item item)
        {
            db.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Edit", "item");
        }

        [Route("odertracking")]
        public IActionResult Odertracking()
        {
            ViewBag.isBreadCrumb = true;
            return View();
        }
        [Route("featured")]
        public IActionResult Featured()
        {
            ViewBag.isBreadCrumb = true;
            return View();
        }
    }
}