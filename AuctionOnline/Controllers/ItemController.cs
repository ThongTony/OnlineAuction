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
        public IActionResult Index()
        {
            return View();
        }
        [Route("details")]
        public IActionResult Details(int id)
        {
            ViewBag.isBreadCrumb = true;
            ViewBag.Item = db.Items.Where(i => i.Id == id).ToList();
            return View("Details",ViewBag.Item);
        }
        [Route("list")]
        public IActionResult List()
        {
            ViewBag.isBreadCrumb = true;
            ViewBag.Item = db.Items.ToList();
            return View("List", ViewBag.Item);
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