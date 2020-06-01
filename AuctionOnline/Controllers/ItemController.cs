using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AuctionOnline.Controllers
{
    [Route("item")]
    public class ItemController : Controller
    {
        [Route("index")]
        public IActionResult Index()
        {
            return View();
        }
        [Route("details")]
        public IActionResult Details()
        {
            ViewBag.isBreadCrumb = true;
            return View();
        }
        [Route("list")]
        public IActionResult List()
        {
            ViewBag.isBreadCrumb = true;
            return View();
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