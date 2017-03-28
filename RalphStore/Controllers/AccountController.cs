using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RalphStore.Models;

namespace RalphStore.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View(new RegisterModel());
        }

        [HttpPost]
        public ActionResult ValidateAddress(string street1, string street2, string city, string state,
            string zip)
        {
            var authId = ConfigurationManager.AppSettings["SmartyStreets.AuthId"];
            var authtoken = ConfigurationManager.AppSettings["SmartyStreets.AuthToken"];
            SmartyStreets.USStreetApi.ClientBuilder builder = new SmartyStreets.USStreetApi.ClientBuilder(authId, authtoken);
            SmartyStreets.USStreetApi.Client client = builder.Build();
            SmartyStreets.USStreetApi.Lookup lookup = new SmartyStreets.USStreetApi.Lookup();
            lookup.City = city;
            lookup.State = state;
            lookup.ZipCode = zip;
            lookup.Street = street1;
            lookup.Street2 = street2;

            client.Send(lookup);
            return Json(lookup.Result);


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                using (IdentityModels entities = new IdentityModels())
                {
                    var userStore = new UserStore<User>(entities);

                    var manager = new UserManager<User>(userStore);

                    var user = new User()
                    {
                        UserName = model.EmailAddress,
                        //EmailConfirmed = true,
                        Email = model.EmailAddress
                    };
                    IdentityResult result = manager.Create(user, model.Password);

                    User u = manager.FindByName(model.EmailAddress);
                    String confirmationToken = manager.GenerateEmailConfirmationToken(u.Id);

                    if (result.Succeeded)
                    {
                        FormsAuthentication.SetAuthCookie(model.EmailAddress, true);
                    }
                }
                return RedirectToAction("Index", "Home");
            }
            return View(model);  
        }

        public ActionResult Login()
        {
            return View(new LoginModel());
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}