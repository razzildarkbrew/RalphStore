using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RalphStore.Models;
namespace RalphStore.Controllers
{
    public class CheckoutController : Controller
    {
        // string connectionString = System.ConfigurationManager,ConnectionStrings["RalphStore].ConnectionString;
        // GET: Checkout
        public ActionResult Index()
        {
            CheckoutModel model = new CheckoutModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(CheckoutModel model)
        {
            if (ModelState.IsValid)
            {
                using (Entities entities = new Entities())
                {
                    string uniqueName = Guid.NewGuid().ToString();
                    OrderHeader newOrder = new OrderHeader();
                    entities.OrderHeaders.Add(newOrder);

                }
                //validate
                //TODO: Persist this order to the database, redirect to a receipt page
                return RedirectToAction("Index", "Receipt");
            }
            
            return View(model);
        }
        [HttpPost]
        public ActionResult States(string country)
        {
            var c = StateData.Countries.FirstOrDefault(x => x.Value == country);
            if (c != null)
                if (c != null)
            {
                return Json(c.States);
            }
            return Json(new StateModel[0]);
        }
        [HttpPost]
        public ActionResult Countries()
        {
            return Json(StateData.Countries);
        }

    }
}