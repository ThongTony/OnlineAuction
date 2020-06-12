using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AuctionOnline.Controllers
{
    [Route("user")]
    public class UserController : Controller
    {
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
        [Route("itemlist")]
        public IActionResult ItemList()
        {
            ViewBag.isBreadCrumb = true;
            return View();
        }
        [Route("cart")]
        public IActionResult Cart()
        {
            ViewBag.isBreadCrumb = true;
            return View();
        }
    }
}