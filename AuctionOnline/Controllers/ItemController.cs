using System;
using System.Collections.Generic;
using System.Linq;
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
        [Route("index")]
        public IActionResult Index(int id)
        {
            ViewBag.Item = db.Items.Where(i => i.Id == id).ToList();
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
            ViewBag.CategoryItem = db.Categories.Where(i => i.Id == id);
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
        [Route("add")]
        public IActionResult Add()
        {
            return View("Add", new Item());
        }
        [HttpPost]
        [Route("add")]
        public IActionResult Add(Item items)
        {
            ViewBag.result = "Failed";
            if (items != null)
            {
                db.Items.Add(items);
                db.SaveChanges();
                ViewBag.result = "Success";
            }
            return View();
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
        [Route("edit/{id}")]
        public IActionResult Edit(int id, Item item)
        {
            db.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("list", "item");
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