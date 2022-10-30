using Microsoft.AspNetCore.Mvc;
using PRN211.E4_Group6_A3.Models;

namespace PRN211.E4_Group6_A3.Controllers
{
    public class ShoppingController : Controller
    {
        MusicStoreContext context = new MusicStoreContext();
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Cart()
        {
            

            return View();
        }
    }
}
