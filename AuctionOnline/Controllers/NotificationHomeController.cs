using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AuctionOnline.Data;
using Microsoft.AspNetCore.Mvc;

namespace AuctionOnline.Controllers
{
    public class NotificationHomeController : Controller
    {
        private AuctionDbContext db;
        public NotificationHomeController(AuctionDbContext _db)
        {
            db = _db;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            return Ok(db.NotificationProducts.ToList());
        }
        public IActionResult Index()
        {
            return View();
        }

    }
}