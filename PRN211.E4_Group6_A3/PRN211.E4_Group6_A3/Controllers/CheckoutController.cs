using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using PRN211.E4_Group6_A3.Models;
using SE1634_MVC.Models;

namespace PRN211.E4_Group6_A3.Controllers
{
    public class CheckoutController : Controller

    {
        private readonly MusicStoreContext _context;
        
        private readonly ShoppingCart cart;
        public CheckoutController(MusicStoreContext context, ShoppingCart cart)
        {
            _context = context;
            
        }

        // GET: CheckoutController
  
        public IActionResult Checkout()
        {
            return View();
        }

        // POST: CheckoutController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Checkout(IFormCollection collection)
        {
            Order order = new Order();
            order.OrderDate = DateTime.Now;
            order.UserName = User.Identity.Name;

            _context.Orders.Add(order);
            _context.SaveChanges();
            var cart = ShoppingCart.GetCart(this.HttpContext);
            cart.CreateOrder(order);
            return View(order);
            
        }

    }
}
