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
            //if (id == 1)
            //{
            //    model.ProductName = "Backpack";
            //    model.ProductPrice = 19.99m;
            //    model.ProductDescription = "blah blah blah";
            //    model.ID = 1;
                

            //    return View(model);
            //}
            //else if (id == 2)
            //{
            //    model.ProductName = "Travel Adapter";
            //    model.ProductPrice = 29.99m;
            //    model.ProductDescription = "lalalala";
            //    model.ID = 2;
                
            //    return View(model);
            //}

            //return HttpNotFound(string.Format("ID {0} Not Found", id));

            using (Entities entities = new Entities())
            {
                var product = entities.Products.Find(id);
                if (product != null)
                {
                    ProductModel model = new ProductModel();
                    model.ID = product.ID;
                    model.ProductDescription = product.Description;
                    model.ProductName = product.Name;
                    model.ProductPrice = product.Price;
                    model.Images = product.ProductImages.Select(x => x.Path).ToArray();

                    return View(model);

                }
            }
            return HttpNotFound(string.Format("ID {0} Not Found", id));
        }
        //POST: Product
        [HttpPost]
        public ActionResult Index(ProductModel model)
        {
            using (Entities entities = new Entities())
            {
                if (User.Identity.IsAuthenticated)
                {
                    AspNetUser currentUser = entities.AspNetUsers.Single(x => x.UserName == User.Identity.Name);
                    //OrderHeader o = currentUser.Orders.FirstOrDefault(x => x.Completed == null);
                }
            }
            return HttpNotFound();
        }
    }
}