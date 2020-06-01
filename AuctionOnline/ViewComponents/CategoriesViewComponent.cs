using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionOnline.ViewComponents
{
    [ViewComponent(Name = "Categories")]
    public class CategoriesViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {

            return View("Navbar");
        }

    }
}
