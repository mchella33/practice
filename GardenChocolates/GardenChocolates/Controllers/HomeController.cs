using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GardenChocolates.Controllers
{
    [RoutePrefix("")]
    public class HomeController : Controller
    {
       
        private ChocolatesEntities db = new ChocolatesEntities();
        public ActionResult Index()
        {
            var products = db.Products.ToList();
            return View(products);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact Us";

            return View();
        }
        [HttpPost]
        [Route("AddToCart")]
        public ActionResult AddToCart(int productId)
        {
            var customerId = 1;
            var existingCart = db.Carts.FirstOrDefault(x => x.CustomerId == customerId && x.ProductId == productId);
            if (existingCart != null)
            {
                existingCart.Quantity++;
                db.SaveChanges();
            }
            else
            {
                var cart = new Cart();
                var product = db.Products.FirstOrDefault(p => p.ProductId == productId);
                cart.CustomerId = 2;
                cart.PriceEach = product.PriceEach;
                cart.PriceTotal = product.PriceEach;
                cart.Quantity = 1;
                db.Carts.Add(cart);
                db.SaveChanges();
            }
            return null;


        }

    }
}