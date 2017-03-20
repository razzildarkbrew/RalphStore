using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RalphStore.Models;

namespace RalphStore.Controllers
{
    public class ProductController : Controller
    {
        ProductModel model = new ProductModel();
        // GET: Product
        
        public ActionResult Index(int? id)
        {
            if (id == 1)
            {
                model.ProductName = "Backpack";
                model.ProductPrice = 19.99m;
                model.ProductDescription = "blah blah blah";
                model.ID = 1;
                

                return View(model);
            }
            else if (id == 2)
            {
                model.ProductName = "Travel Adapter";
                model.ProductPrice = 29.99m;
                model.ProductDescription = "lalalala";
                model.ID = 2;
                
                return View(model);
            }

            return HttpNotFound(string.Format("ID {0} Not Found", id));
        }
        //POST: Product
        [HttpPost]
        public ActionResult Index(ProductModel model)
        {
            //TODO: Collect information about the selected product
            //persist it in some sort of "Cart/Basket/ShoppingBag" in a database
            List<ProductModel> cart = this.Session["Cart"] as List<ProductModel>;
            if (cart == null)
            {
                cart = new List<ProductModel>();
            }

            cart.Add(model);

            this.Session.Add("Cart", cart);

            TempData.Add("AddedToCart", true);

            return RedirectToAction("Index", "Cart");
        }
    }
}