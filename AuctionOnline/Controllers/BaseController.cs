using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuctionOnline.Controllers
{
    public class BaseController : Controller
    {
        [HttpGet]
        public async Task<string> GetSession()
        {
            //HttpContext.Session.SetString("username", "Khoa session");
            var sessionValue = HttpContext.Session.GetString("username");

            return sessionValue;
        }
    }
}