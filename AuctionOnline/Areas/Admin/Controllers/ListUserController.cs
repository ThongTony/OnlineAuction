using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuctionOnline.Data;
using AuctionOnline.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuctionOnline.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/listuser")]
    public class ListUserController : Controller
    {
        private AuctionDbContext db;
        public ListUserController(AuctionDbContext _db)
        {
            db = _db;
        }
        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            ViewBag.Accounts = db.Accounts.ToList();
            return View();
        }

        [HttpGet]
        [Route("edit/{id}")]
        public IActionResult Edit(int id)
        {
            var account = db.Accounts.Find(id);
            return View("edit", account);
        }


        [HttpPost]
        public IActionResult Edit(Account account)
        {
            db.Entry(account).State = EntityState.Modified;
            db.SaveChanges();
            return View("Index");
        }



    }
}
