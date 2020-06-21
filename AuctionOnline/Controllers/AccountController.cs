using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuctionOnline.Data;
using AuctionOnline.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AuctionOnline.Controllers
{
    [Route("account")]
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

        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("username");
            return RedirectToAction("Index", "Home");
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


        [HttpGet]
        [Route("resetpassword")]
        public IActionResult Resetpassword()
        {
            return View();
        }

        [HttpPost]
        [Route("resetpassword")]
        public IActionResult Resetpassword(string email, string username )
        {
            var checkemail = db.Accounts.SingleOrDefault(a => a.Email.Equals(email));
            var checkusername = db.Accounts.SingleOrDefault(a => a.Username.Equals(username));
            if (checkusername != null && checkemail != null)
            {
                // send mail
                var checkpassword = (from i in db.Accounts
                               where i.Username == username
                                     select i.Password).FirstOrDefault();
                var password = BCrypt.Net.BCrypt.HashString(checkpassword);
                var body = "<b>Your Password Is: " + password; 
                var mailHelper = new MailHelper(configuration);
                if (mailHelper.Send(configuration["Gmail:Username"], email, "From Bookshop", body))
                {
                    
                    ViewBag.Success = "Your password has been sent in gmail: " + email;
                    return View("Resetpassword");
                }
                else
                {
                    ViewBag.errorSendMail = "When send mail to you have error, contact with admin.";
                    return View("Resetpassword");
                }
            }
            else
            {
                ViewBag.Failed = "Invalid";
                return View("resetpassword");
            }
        }
        /*
         * [HttpGet]
        [Route("forgotpw")]
        public IActionResult Forgotpw()
        {
            return View(new Account());
        }
        [HttpPost]
        [Route("forgotpw")]
        public async Task<IActionResult> Forgotpw(Account account)
        {
            if (account.Username !=null || account.Email !=null)
            {
                if (accountRepository.GetAll().SingleOrDefault(a => a.Username.Equals(account.Username) && a.Email.Equals(account.Email))!=null)
                {
                    var accountDB = accountRepository.GetAll().SingleOrDefault(a => a.Username.Equals(account.Username) && a.Email.Equals(account.Email));
                    account = await accountRepository.GetById(accountDB.Id);
                    var newpw = accountRepository.RandomNumber(10000000, 99999999);
                    account.Password = BCrypt.Net.BCrypt.HashString(newpw.ToString());
                    // send mail
                    var body = "Your new password: " + newpw;
                    var mailHelper = new MailHelper(configuration);
                    if (mailHelper.sendMail(configuration["Gmail:Username"], account.Email, "From Bookshop", body))
                    {
                        await accountRepository.Update(account.Id, account);
                        return RedirectToAction("index", "login");
                    }
                    else
                    {
                        ViewBag.errorSendMail = "When send mail to you have error, contact with admin.";
                        return View(account);
                    }
                }
                else
                {
                    ViewBag.errorInput = "No matching information found, please check again.";
                    return View(account);
                }
            }
            else
            {
                return View(account);
            }

            return View(account);
        }
         * */
    }
}