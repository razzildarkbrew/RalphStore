using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RalphStore.Models;
using SendGrid;
using SendGrid.Helpers.Mail;

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
               /* //TODO: send an email indicating that the order was placed
                string sendGridApiKey = System.Configuration.ConfigurationManager.AppSettings["SendGrid.Apikey"];

                SendGrid.SendGridClient client = new SendGridClient(sendGridApiKey);
                SendGrid.Helpers.Mail.SendGridMessage message = new SendGrid.Helpers.Mail.SendGridMessage();
                message.Subject = "Receipt for  order #00000";
                message.From = new SendGrid.Helpers.Mail.EmailAddress("admin@unbox.com", "Unbox Store");
                message.AddTo(new sendgrid);*/

                /*message.AddContent();
                await client.SendEmailAsync(message);*/

                using (Entities entities = new Entities())
                {
                    /*string uniqueName = Guid.NewGuid().ToString();
                    OrderHeader newOrder = new OrderHeader();
                    entities.OrderHeaders.Add(newOrder);*/
                    //Order o = null;
                    if (User.Identity.IsAuthenticated)
                    {
                        AspNetUser currentUser = entities.AspNetUsers.Single(x => x.UserName == User.Identity.Name);
                        //o = currentUser;
                    }
                    


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