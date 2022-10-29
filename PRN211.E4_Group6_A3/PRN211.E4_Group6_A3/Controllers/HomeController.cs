using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;
using PRN211.E4_Group6_A3.Models;
using System.Diagnostics;


namespace PRN211.E4_Group6_A3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(User user)
        {
            MusicStoreContext context = new MusicStoreContext();
            User user1 = context.Users.Where(u => u.UserName == user.UserName && u.Password == user.Password).FirstOrDefault();
            if(user1 == null)
            {
                return View();
            }
            else
            {
                HttpContext.Session.SetString("UserName", user.UserName);
                HttpContext.Session.SetInt32("Role", user1.Role);

                ShoppingCart cart = ShoppingCart.GetCart(HttpContext);
                cart.MigrateCart(HttpContext);
                return RedirectToAction("Index","Home");
            }            
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            ShoppingCart cart = ShoppingCart.GetCart(HttpContext);
            cart.EmptyCart();
            return RedirectToAction("Index","Home");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}