using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionOnline.ViewComponents
{
    [ViewComponent(Name = "Breadcrumb")]
    public class BreadcrumbViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {

            return View("Index");
        }
    }
}
