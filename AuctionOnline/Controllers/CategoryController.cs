using AuctionOnline.Data;
using AuctionOnline.Utilities;
using AuctionOnline.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionOnline.Controllers
{
    public class CategoryController : Controller
    {
        private AuctionDbContext db;
        private LayoutViewModel layoutVM;
        public CategoryController(AuctionDbContext _category)
        {
            db = _category;
            layoutVM = new LayoutViewModel();
        }

        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetInt32("checkidAdmin") != null)
            {
                //var categories = db.Categories.Include(c => c.Parent);
                var categories = db.Categories.Include(c => c.Parent);
                layoutVM.CategoriesVM = CategoryUtility.MapModelsToVMs(categories.ToList());
                return View(layoutVM);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public async Task<IActionResult> Detail(int id)
        {
            if (HttpContext.Session.GetInt32("checkidAdmin") != null)
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
                layoutVM.CategoryVM = CategoryUtility.MapModeltoVM(category);

                return View(layoutVM);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpGet]
        public IActionResult Create(int? id)
        {
            if (HttpContext.Session.GetInt32("checkidAdmin") != null)
            {
                layoutVM.CategoryVM.ParentId = id;
                return View(layoutVM);
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

                //stop admin add 3rd category
                if (category.ParentId == null)
                {
                    category.Level = 1;
                }
                else if (CategoryExists(category.ParentId) == true)
                {
                    category.Level = 2;
                }

                db.Add(category);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(categoryVM);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (HttpContext.Session.GetInt32("checkidAdmin") != null)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var category = await db.Categories.FindAsync(id);

                layoutVM.CategoryVM = CategoryUtility.MapModeltoVM(category);
                layoutVM.CategoryVM.Categories = db.Categories.Select(a =>
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
                return View(layoutVM);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
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
            if (HttpContext.Session.GetInt32("checkidAdmin") != null)
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
            else
            {
                return RedirectToAction("Login", "Account");
            }
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

        private bool CategoryExists(int? id)
        {
            return db.Categories.Any(e => e.Id == id);
        }

        [HttpPost]
        public IActionResult SearchCategory(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                var checkkeyword = db.Categories.Where(a => a.Name.Trim().Contains(keyword.Trim())).ToList();

                if (checkkeyword != null)
                {
                    var viewmodel = new LayoutViewModel()
                    {
                        CategoriesVM = CategoryUtility.MapModelsToVMs(checkkeyword.ToList())
                    };

                    return View("Index", viewmodel);

                }
                else
                {
                    return View("Index");
                }
            }
            else
            {
                return RedirectToAction("Index");
            }
        }


    }
}