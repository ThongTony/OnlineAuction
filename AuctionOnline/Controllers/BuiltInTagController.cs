using AuctionOnline.Data;
using AuctionOnline.Models;
using AuctionOnline.Models.Business;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AuctionOnline.Controllers
{
    public class BuiltInTagController : Controller
    {
        public IActionResult Index() => View();
        public IActionResult AnchorTagHelper(int id)
        {
            var category = new Category
            {
                ParentId = id
            };

            return View(category);
        }
    }
}