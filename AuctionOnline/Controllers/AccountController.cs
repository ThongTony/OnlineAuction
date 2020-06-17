using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuctionOnline.Data;
using AuctionOnline.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuctionOnline.Controllers
{
    [Route("account")]
    public class AccountController : Controller
    {
        private AuctionDbContext db;

        public AccountController(AuctionDbContext _db)
        {
            db = _db;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View("Login");
        }

      
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var account = db.Accounts.SingleOrDefault(a => a.Username.Equals(username) && a.Status == true);
            if (account != null)
            {
                if (BCrypt.Net.BCrypt.Verify(password, account.Password))
                {
                    if (account.RoleId == 1 && account.IsBlocked == false)
                    {
                        HttpContext.Session.SetString("username", username);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return RedirectToAction("404error", "Home");
                    }
                }
            }
            ViewBag.invalid = "Username hoac Password Khong Dung";
            return View("Login");
        }

        public IActionResult Resetpassword()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View("Register");
        }

      
        [HttpPost]
        public IActionResult Register(string fullname, string username, string email, string password)
        {
            var account = db.Accounts.SingleOrDefault(a => a.Username.Equals(username));
            var emails = db.Accounts.SingleOrDefault(a => a.Email.Equals(email));
            if (account != null)
            {
                ViewBag.failed = "Username đã tồn tại ";
                return View();
            }
            else if (emails != null)
            {
                ViewBag.failed = "Email đã tồn tại ";
                return View();
            }
            else
            {
                ViewBag.success = "Success";
                Account accounts = new Account();
                accounts.Fullname = fullname;
                accounts.Username = username;
                accounts.Email = email;
                accounts.Password = BCrypt.Net.BCrypt.HashPassword(password);
                accounts.Status = true;
                accounts.RoleId = 1;
                accounts.IsBlocked = false;
                accounts.CreatedAt = DateTime.Now;
                db.Accounts.Add(accounts);
                db.SaveChanges();
                return RedirectToAction("Login");
            }
        }

        public IActionResult Adminwelcome()
        {
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("username");
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Forgotpassword()
        {
            return View("Resetpassword");
        }

        public IActionResult DemoIndex()
        {
            ViewBag.Account = db.Accounts.Where(x => x.RoleId == 1).ToList();
            return View("DemoIndex");
        }
        public IActionResult List()
        {
            ViewBag.SellerCount = db.Accounts.Select(x => x.RoleId == 1).Count();
            ViewBag.SellerList = db.Accounts.Where(x => x.RoleId == 1).ToList();
            return View("List");
        }

    }
}