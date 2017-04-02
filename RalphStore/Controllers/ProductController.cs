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
        //ProductModel model = new ProductModel();
        // GET: Product
        [OutputCache(Duration = 300)]
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
                    model.Description = product.Description;
                    model.Name = product.Name;
                    model.Price = product.Price;
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
                    Order o = currentUser.Orders.FirstOrDefault(x => x.Completed == null);
                    if (o == null)
                    {
                        o = new Order();
                        o.OrderNumber = Guid.NewGuid();
                        currentUser.Orders.Add(o);
                    }
                    var product = o.OrderProducts.FirstOrDefault(x => x.ProductID == model.ID);
                    if (product == null)
                    {
                        product = new OrderProduct();
                        product.ProductID = model.ID ?? 0;
                        product.Quantity = 0;
                        o.OrderProducts.Add(product);
                    }
                    product.Quantity += 1;
                }
                else
                {
                    Order o = null;
                    if (Request.Cookies.AllKeys.Contains("orderNumber"))
                    {
                        Guid orderNumber = Guid.Parse(Request.Cookies["orderNumber"].Value);
                        o = entities.Orders.FirstOrDefault(x => x.Completed == null && x.OrderNumber == orderNumber);

                    }
                    if (o == null)
                    {
                        o = new Order();
                        o.OrderNumber = Guid.NewGuid();
                        entities.Orders.Add(o);
                        Response.Cookies.Add(new HttpCookie("orderNumber", o.OrderNumber.ToString()));
                    }
                    var product = o.OrderProducts.FirstOrDefault(x => x.ProductID == model.ID);
                    if (product == null)
                    {
                        product = new OrderProduct();
                        product.ProductID = model.ID ?? 0;
                        product.Quantity = 0;
                        o.OrderProducts.Add(product);
                    }
                    product.Quantity += 1;
                }

                entities.SaveChanges();
                TempData.Add("AddedToCart", true);
            }
            return RedirectToAction("Index", "Cart");
        }
    }
}