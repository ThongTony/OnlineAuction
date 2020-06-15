﻿using System;
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
        [Route("login")]
        public IActionResult Login()
        {
            return View("Login");
        }

      
        [HttpPost]
        [Route("login")]
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

        [Route("resetpassword")]
        public IActionResult Resetpassword()
        {
            return View();
        }

        [HttpGet]
        [Route("register")]
        public IActionResult Register()
        {
            return View("Register");
        }

      
        [HttpPost]
        [Route("register")]
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

        [Route("adminwelcome")]
        public IActionResult Adminwelcome()
        {
            return View();
        }

        [Route("logoout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("username");
            return RedirectToAction("Index", "Home");
        }

        [Route("forgotpassword")]
        public IActionResult Forgotpassword()
        {
            return View("Resetpassword");
        }
        [Route("index")]
        public IActionResult DemoIndex()
        {
            ViewBag.Account = db.Accounts.Where(x => x.RoleId == 1).ToList();
            return View("DemoIndex");
        }
        [Route("list")]
        public IActionResult List()
        {
            ViewBag.SellerCount = db.Accounts.Select(x => x.RoleId == 1).Count();
            ViewBag.SellerList = db.Accounts.Where(x => x.RoleId == 1).ToList();
            return View("List");
        }

    }
}