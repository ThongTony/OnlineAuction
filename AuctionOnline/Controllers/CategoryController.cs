using AuctionOnline.Data;
using AuctionOnline.Models;
using AuctionOnline.Utilities;
using AuctionOnline.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionOnline.Controllers
{
    public class CategoryController : Controller
    {

        private AuctionDbContext db;
        public CategoryController(AuctionDbContext _category)
        {
            db = _category;
        }

        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetInt32("checkidAdmin") != null)
            {
                var categories = db.Categories.Include(c => c.Parent);
                var viewmodel = new LayoutViewModel();
                viewmodel.CategoriesVM = CategoryUtility.MapModelsToVMs(categories.ToList());
                return View(viewmodel);
            }
            else
            {
                return RedirectToAction("Login","Account");
            }

        }

        public async Task<IActionResult> Detail(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await db.Categories
                .Include(c => c.Parent)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            var viewmodel = new LayoutViewModel()
            {
                CategoryVM = CategoryUtility.MapModeltoVM(category)
            };


            return View(viewmodel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (HttpContext.Session.GetInt32("checkidAdmin") != null)
            {
                ViewData["ParentId"] = new SelectList(db.Categories, "Id", "Id");
                var viewmodel = new LayoutViewModel();
                //var cateVM = new CategoryVM();
                viewmodel.CategoryVM.Categories = db.Categories.Select(a =>
                                       new SelectListItem
                                       {
                                           Value = a.Id.ToString(),
                                           Text = a.Name
                                       }).ToList();
                return View(viewmodel);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryVM categoryVM)
        {

            ViewBag.CategoryAdd = "Failed";
            categoryVM.CreatedAt = DateTime.Now;
            if (categoryVM != null)
            {
                var category = CategoryUtility.MapVMtoModel(categoryVM);
                db.Add(category);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParentId"] = new SelectList(db.Categories, "Id", "Id", categoryVM.ParentId);
            return View(categoryVM);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await db.Categories.FindAsync(id);
            var layoutViewModel = new LayoutViewModel();
            layoutViewModel.CategoryVM = CategoryUtility.MapModeltoVM(category);
            layoutViewModel.CategoryVM.Categories = db.Categories.Select(a =>
                                  new SelectListItem
                                  {
                                      Value = a.Id.ToString(),
                                      Text = a.Name
                                  }).ToList();
            if (category == null)
            {
                return NotFound();
            }
            ViewData["ParentId"] = new SelectList(db.Categories, "Id", "Id", category.ParentId);
            return View(layoutViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CategoryVM categoryVM)
        {
            if (ModelState.IsValid)
            {
                //Map model to viewmodels
                var category = await db.Categories.FindAsync(categoryVM.Id);
                category.Name = categoryVM.Name;
                category.ParentId = categoryVM.ParentId;

                try
                {
                    db.Update(category);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParentId"] = new SelectList(db.Categories, "Id", "Id", categoryVM.ParentId);
            return View(categoryVM);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var category = await db.Categories
                .Include(c => c.Parent)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            var viewlayout = new LayoutViewModel()
            {
                CategoryVM = CategoryUtility.MapModeltoVM(category)
            };

            return View(viewlayout);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(CategoryVM categoryVM)
        {
            var category = await db.Categories.FindAsync(categoryVM.Id);

            //delete related category - item table
            var categoryitems = db.CategoryItems.Where(c => c.CategoryId == categoryVM.Id);
            foreach (var categoryitem in categoryitems)
            {
                category.CategoryItems.Remove(categoryitem);
            }

            db.Categories.Remove(category);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Route("model")]
        public IActionResult Model()
        {
            var categories = db.Categories.ToList();
            return View("Index", categories);
        }
                
        private bool CategoryExists(int id)
        {
            return db.Categories.Any(e => e.Id == id);
        }


    }
}