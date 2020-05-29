using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AuctionOnline.Controllers
{
    [Route("details")]
    public class DetailsController : Controller
    {
        [Route("index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}