using System;
using System.Linq;
using System.Threading.Tasks;
using AuctionOnline.Data;
using AuctionOnline.Models;
using AuctionOnline.Utilities;
using AuctionOnline.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AuctionOnline.Controllers
{
    public class AccountController : Controller
    {
        private IConfiguration configuration;

        private AuctionDbContext db;
        private LayoutViewModel layoutVM;
        private readonly ILogger<AccountController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountController(IConfiguration _configuration,
            AuctionDbContext _db, ILogger<AccountController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            db = _db;
            configuration = _configuration;
            layoutVM = new LayoutViewModel()
            {
                CategoriesVM = RecursiveMenu.GetRecursiveMenu(db)
            };
        }

        [HttpGet]
        public IActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var account = db.Accounts.SingleOrDefault(a => a.Username.Equals(username));
            if (account != null)
            {
                if (BCrypt.Net.BCrypt.Verify(password, account.Password))
                {
                    if (account.RoleId == 1 && account.IsBlocked == false && account.Status == true)
                    {
                        HttpContext.Session.SetString("username", username);
                        int checkiduser = (from i in db.Accounts
                                           where i.Username == username
                                           select i.Id).FirstOrDefault();
                        HttpContext.Session.SetInt32("checkiduser", checkiduser);
                        return RedirectToAction("Index", "Home");
                    }
                    else if (account.RoleId == 0)
                    {
                        int checkidadmin = (from i in db.Accounts
                                            where i.Username == username
                                            select i.Id).FirstOrDefault();
                        HttpContext.Session.SetInt32("checkidAdmin", checkidadmin);
                        return RedirectToAction("AdminListUser");
                    }
                    else
                    {
                        HttpContext.Session.SetString("usernameblocked", username);
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

            return View(layoutVM);
        }

        //[HttpPost]
        //public IActionResult Register(string fullname, string username, string email, string password , int phone , string address)
        //{
        //    var account = db.Accounts.SingleOrDefault(a => a.Username.Equals(username));
        //    var emails = db.Accounts.SingleOrDefault(a => a.Email.Equals(email));
        //    if (account != null)
        //    {
        //        ViewBag.failed = "Username đã tồn tại ";
        //        return View();
        //    }
        //    else if (emails != null)
        //    {
        //        ViewBag.failed = "Email đã tồn tại ";
        //        return View();
        //    }
        //    else
        //    {
        //        ViewBag.success = "Success";
        //        Account accounts = new Account();
        //        accounts.Fullname = fullname;
        //        accounts.Username = username;
        //        accounts.Email = email;
        //        accounts.Address = address;
        //        accounts.PhoneNumber = phone;
        //        accounts.Password = BCrypt.Net.BCrypt.HashPassword(password);
        //        accounts.Status = true;
        //        accounts.RoleId = 1;
        //        accounts.IsBlocked = false;
        //        accounts.CreatedAt = DateTime.Now;
        //        db.Accounts.Add(accounts);
        //        db.SaveChanges();
        //        return RedirectToAction("Login");
        //    }
        //}

        [HttpPost]
        public async Task<IActionResult> Register(AccountVM accountVM)
        {
            var account = db.Accounts.SingleOrDefault(a => a.Username.Equals(accountVM.Username));
            var email = db.Accounts.SingleOrDefault(a => a.Email.Equals(accountVM.Email));

            if (account != null)
            {
                ViewBag.failed = "The Username already exists ";
                return View();
            }
            else if (email != null)
            {
                ViewBag.failed = "The Emails already exists";
                return View();
            }
            else
            {
                if (accountVM != null)
                {
                    var acccount = AccountUtility.MapVMToModel(accountVM);
                    //Account accounts = new Account();
                    var i = acccount.Password;
                    var hashpassword = BCrypt.Net.BCrypt.HashPassword(i);
                    acccount.Password = hashpassword;
                    acccount.Status = true;
                    acccount.RoleId = 1;
                    acccount.IsBlocked = false;
                    acccount.CreatedAt = DateTime.Now;
                    db.Accounts.Add(acccount);
                    await db.SaveChangesAsync();
                    ViewBag.success = "Success";
                    return RedirectToAction(nameof(Login));
                }

                return View(accountVM);
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            foreach (var cookie in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(cookie);
            }
            return RedirectToAction("Index", "Home");

        }

        public IActionResult AdminListUser()
        {
            if (HttpContext.Session.GetInt32("checkidAdmin") != null)
            {
                ViewBag.Accounts = db.Accounts.Where(x => x.RoleId == 1).ToList();
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public IActionResult ListUser()
        {


            if (HttpContext.Session.GetInt32("checkiduser") != null)
            {
                var users = db.Accounts.Where(x => x.RoleId == 1 && x.IsBlocked == false).ToList();
                layoutVM.AccountsVM = AccountUtility.MapModelsToVMs(users);
                return View(layoutVM);
            }
            else
            {
                return RedirectToAction("Login");
            }
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
                string host = _httpContextAccessor.HttpContext.Request.Host.Value;
                string body = "Please reset your password by clicking  https://" + host + "/account/resetpassword";
                var mailHelper = new MailHelper(configuration);
                if (mailHelper.Send(configuration["Gmail:Username"], email, "From Bookshop", body))
                {
                    HttpContext.Session.SetString("email", email);
                    //send mail
                    int checkid = (from i in db.Accounts
                                   where i.Email == email
                                   select i.Id).FirstOrDefault();
                    HttpContext.Session.SetInt32("checkid", checkid);
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


            if (HttpContext.Session.GetString("email") != null)
            {
                return View(layoutVM);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }
        [HttpPost]
        public IActionResult Resetpassword(string password, string confirmpassword, Account account)
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
                    ViewBag.Failed = "The password and confirm password do not match";
                    return View();
                }
            }
            return View();
        }

        public IActionResult Delete(AccountVM accountVM)
        {

            if (accountVM.Id != null)
            {
                db.Accounts.Remove(db.Accounts.Find(accountVM.Id));
                db.SaveChanges();
                return RedirectToAction("AdminListUser");
            }
            else
            {
                return RedirectToAction("AdminListUser");
            }
        }

        public IActionResult Blocked(AccountVM accountVM, Account account)
        {
            var checkid = db.Accounts.Find(accountVM.Id);
            if (checkid != null)
            {
                var i = db.Accounts.Where(a => a.IsBlocked == false);
                if (i != null)
                {
                    account = db.Accounts.Find(accountVM.Id);
                    account.Status = false;
                    account.IsBlocked = true;
                    db.Entry(account).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("AdminListUser");
                }
            }
            return View("AdminListUser");

        }

        public IActionResult Unlock(AccountVM accountVM, Account account)
        {
            var checkid = db.Accounts.Find(accountVM.Id);
            if (checkid != null)
            {
                var i = db.Accounts.Where(a => a.IsBlocked == true);
                if (i != null)
                {
                    account = db.Accounts.Find(accountVM.Id);
                    account.Status = true;
                    account.IsBlocked = false;

                    db.Entry(account).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("AdminListUser");
                }
            }
            return View("AdminListUser");

        }

        [HttpGet]
        public IActionResult Profileuser()
        {
            if (HttpContext.Session.GetInt32("checkiduser") != null)
            {
                var id = HttpContext.Session.GetInt32("checkiduser");
                var listuser = db.Accounts.Where(a => a.Id == id).ToList();
                ViewBag.listuser = listuser;
                return View(layoutVM);
            }
            else
            {
                return RedirectToAction("Login");
            }

        }

        [HttpGet]
        public IActionResult Edituser()
        {
            if (HttpContext.Session.GetInt32("checkiduser") != null)
            {
                return View(layoutVM);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        [HttpPost]
        public IActionResult Searchusername(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                var checkkeyword = db.Accounts.Where(a => a.Username.Trim().Contains(keyword.Trim())).ToList();

                if (checkkeyword != null)
                {
                    ViewBag.Accounts = checkkeyword;
                    return View("AdminListUser");

                }
                else
                {
                    return View("AdminListUser");
                }
            }
            else
            {
                return RedirectToAction("AdminListUser");
            }
        }
    }
}