using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AuctionOnline.Controllers
{
    public class AccountsController : Controller
    {
        public IActionResult Index()
        {
            // fix comment
            return View();
        }
    }
}