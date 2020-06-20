using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuctionOnline.Data;
using AuctionOnline.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuctionOnline.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/listuser")]
    public class ListUserController : Controller
    {
        private AuctionDbContext db;
        public ListUserController(AuctionDbContext _db)
        {
            db = _db;
        }
        //[Route("")]
        public IActionResult Index()
        {
            ViewBag.Accounts = db.Accounts.Where(a => a.RoleId == 1).ToList();
            return View();
        }

        [Route("delete/{id}")]
        public IActionResult Delete(int id)
        {
            db.Accounts.Remove(db.Accounts.Find(id));
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [Route("blocked/{id}")]
        public IActionResult Blocked(int id , Account account)
        {
            var checkid = db.Accounts.Find(id);
            if(checkid != null)
            {
                var i = db.Accounts.Where(a => a.IsBlocked == false);
                if (i != null)
                {
                    account = db.Accounts.Find(id);
                    account.IsBlocked = true;
                    db.Entry(account).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(nameof(Index));

        }


        [Route("unblock/{id}")]
        public IActionResult Unlock(int id, Account account)
        {
            var checkid = db.Accounts.Find(id);
            if (checkid != null)
            {
                var i = db.Accounts.Where(a => a.IsBlocked == true);
                if (i != null)
                {
                    account = db.Accounts.Find(id);
                    account.IsBlocked = false;

                    db.Entry(account).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(nameof(Index));

        }



    }
}