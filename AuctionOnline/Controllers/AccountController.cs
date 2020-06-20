using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuctionOnline.Data;
using AuctionOnline.Models;
using AuctionOnline.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace AuctionOnline.Controllers
{
    public class AccountController : Controller
    {
        private IConfiguration configuration;

        private AuctionDbContext db;
        public AccountController(IConfiguration _configuration,
            AuctionDbContext _db)
        {

            db = _db;
            configuration = _configuration;
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
                    else if ( account.RoleId == 0)
                    {
                        return RedirectToAction("Index", "ListUser", new { area = "Admin" });
                    }
                    else
                    {
                        return RedirectToAction("error", "Home");
                    }
                }
            }
            ViewBag.invalid = "Username hoac Password Khong Dung";
            return View("Login");
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

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("username");
            return RedirectToAction("Index", "Home");
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


        [HttpGet]
        public IActionResult Forgotpassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Forgotpassword(string email)
        {
            var checkemail = db.Accounts.SingleOrDefault(a => a.Email.Equals(email));
            if (checkemail != null)
            {


                string body = "Please reset your password by clicking  https://localhost:44378/account/resetpassword";
                var mailHelper = new MailHelper(configuration);
                if (mailHelper.Send(configuration["Gmail:Username"], email, "From Bookshop", body))
                {
                    HttpContext.Session.SetString("email", email);
                    // send mail
                    int checkid = (from i in db.Accounts
                                   where i.Email == email
                                   select i.Id).FirstOrDefault();
                    HttpContext.Session.SetInt32("checkid", checkid);
                    var checkpassword = (from i in db.Accounts
                                   where i.Email == email
                                   select i.Password).FirstOrDefault();
                    HttpContext.Session.SetInt32("checkid", checkid);
                    HttpContext.Session.SetString("checkpassword", checkpassword);
                    ViewBag.Success = "Your password has been sent in gmail: " + email;
                    return View("Forgotpassword");
                }
                else
                {
                    ViewBag.errorSendMail = "When send mail to you have error, contact with admin.";
                    return View("Forgotpassword");
                }
            }
            else
            {
                ViewBag.Failed = "Sai Gmail";
                return View("Forgotpassword");
            }
        }
        [HttpGet]
        public IActionResult Resetpassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Resetpassword(string password, string confirmpassword , Account account)
        {

            if (HttpContext.Session.GetString("email") != null)
            {
                if (password == confirmpassword)
                {
                    if (HttpContext.Session.GetInt32("checkid") != null)
                    {
                        var hashpassword = BCrypt.Net.BCrypt.HashPassword(password);
                        account = db.Accounts.Find((HttpContext.Session.GetInt32("checkid")));
                        account.Password = hashpassword;
                        db.Entry(account).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Login");
                    }

                }
                else
                {
                    ViewBag.Failed = "Password va Confirm Password khong khop";
                    return View();
                }
            }
            return View();
        }


    }
}