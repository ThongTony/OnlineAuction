using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AuctionOnline.Controllers
{
    [Route("account")]
    public class AccountController : Controller
    {
        [Route("login2")]
        public IActionResult Login2()
        {
            return View();
        }
        [Route("resetpassword")]
        public IActionResult Resetpassword()
        {
            return View();
        }
    }
}