using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AuctionOnline.Controllers
{
    [Route("blog")]
    public class BlogController : Controller
    {
        [Route("index")]
        public IActionResult Index()
        {
            ViewBag.isBread = true;
            return View();
        }
    }
}