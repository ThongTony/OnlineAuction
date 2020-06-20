using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuctionOnline.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AuctionOnline.Areas.Admin.Controllers
{
    [Area("admin")]
    public class LoginController : Controller
    {
        private AuctionDbContext db;
        public LoginController(AuctionDbContext _db)
        {

            db = _db;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username , string password)
        {
            var account = db.Accounts.SingleOrDefault(a => a.Username.Equals(username) && a.Status == true);
            if (account != null)
            {
                if (BCrypt.Net.BCrypt.Verify(password, account.Password))
                {
                    if (account.RoleId == 0 && account.IsBlocked == false)
                    {
                        return RedirectToAction("Index", "ListUser");
                    }
                    else
                    {
                        ViewBag.invalid = "Username hoac Password Khong Dung";
                        return RedirectToAction("Login");
                    }
                }
            }
            ViewBag.invalid = "Username hoac Password Khong Dung";
            return View("Login");
        }
        public IActionResult Logout()
        {
            return RedirectToAction("index");
        }
    }
}
