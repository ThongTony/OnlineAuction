using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuctionOnline.Data;
using AuctionOnline.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace AuctionOnline.Controllers
{
    public class NotificationProductController : Controller
    {
        private AuctionDbContext dbContext;
        private readonly IHubContext<SignalServer> hubContext;

        public NotificationProductController(AuctionDbContext dbContext, IHubContext<SignalServer> hubContext)
        {
            this.dbContext = dbContext;
            this.hubContext = hubContext;
        }
        public IActionResult Index()
        {
            return View(dbContext.NotificationProducts.ToList());
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddProduct(NotificationProduct model)
        {
            if (!ModelState.IsValid) return View(model);

            dbContext.NotificationProducts.Add(model);
            dbContext.SaveChanges();
            hubContext.Clients.All.SendAsync("refreshProducts");
            return RedirectToAction("index");
        }

        [HttpGet]
        public IActionResult EditProduct(Guid id)
        {
            return View(dbContext.NotificationProducts.Find(id));
        }
        [HttpPost]
        public IActionResult EditProduct(NotificationProduct model)
        {
            if (!ModelState.IsValid) return View(model);

            var product = dbContext.NotificationProducts.Find(model.Id);
            product.Name = model.Name;
            product.Price = model.Price;
            product.IsAvailable = model.IsAvailable;
            dbContext.NotificationProducts.Update(product);
            dbContext.SaveChanges();

            hubContext.Clients.All.SendAsync("refreshProducts");
            return RedirectToAction("index");
        }


        [HttpGet]
        public IActionResult DeleteProduct(Guid productId)
        {
            var product = dbContext.NotificationProducts.Find(productId);
            dbContext.NotificationProducts.Remove(product);
            dbContext.SaveChanges();
            hubContext.Clients.All.SendAsync("refreshProducts");
            return RedirectToAction("index");
        }
    }
}