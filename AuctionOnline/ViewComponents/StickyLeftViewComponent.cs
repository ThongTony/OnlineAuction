using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionOnline.ViewComponents
{
    [ViewComponent(Name = "StickyLeft")]
    public class StickyLeftViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {

            return View("Index");
        }
    }
}
