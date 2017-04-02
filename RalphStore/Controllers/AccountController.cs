using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Display(Name = "Validate Address")]
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
        public  async Task<ActionResult> Register(RegisterModel model)
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



                    if (result.Succeeded)
                    {
                        User u = manager.FindByName(model.EmailAddress);


                        //create a customer record in braintree

                        string merchantId = ConfigurationManager.AppSettings["Braintree.MerchantID"];
                        string publicKey = ConfigurationManager.AppSettings["Braintree.PublicKey"];
                        string privateKey = ConfigurationManager.AppSettings["Braintree.PrivateKey"];
                        string environment = ConfigurationManager.AppSettings["Braintree.Environment"];
                        Braintree.BraintreeGateway braintree = new Braintree.BraintreeGateway(environment, merchantId,
                            publicKey, privateKey);
                        Braintree.CustomerRequest customer = new Braintree.CustomerRequest();
                        customer.CustomerId = u.Id;
                        customer.Email = u.Email;

                        var r = await braintree.Customer.CreateAsync(customer);


                        string confirmationToken = manager.GenerateEmailConfirmationToken(u.Id);

                        string sendGridApiKey = System.Configuration.ConfigurationManager.AppSettings["SendGrid.ApiKey"];

                        //SendGrid.SendGridClient 
                        var client = new SendGrid.SendGridClient(sendGridApiKey);
                        //SendGrid.Helpers.Mail.SendGridMessage 
                        var message = new SendGrid.Helpers.Mail.SendGridMessage();
                        message.Subject = string.Format("Please confirm your account");
                        message.From = new SendGrid.Helpers.Mail.EmailAddress("admin@boardgames.codingtemple.com",
                            "Coding Temple Board Games Administrator");
                        message.AddTo(new SendGrid.Helpers.Mail.EmailAddress(model.EmailAddress));
                        SendGrid.Helpers.Mail.Content contents = new SendGrid.Helpers.Mail.Content("text/html",
                            string.Format("<a href=\"{0}\">Confirm Account</a>",
                                Request.Url.GetLeftPart(UriPartial.Authority) + "/Account/Confirm/" + confirmationToken +
                                "?email=" + model.EmailAddress));

                        message.AddContent(contents.Type, contents.Value);
                        SendGrid.Response response = await client.SendEmailAsync(message);



                        return RedirectToAction("ConfirmSent");
                    }
                    else
                    {
                        ModelState.AddModelError("EmailAddress", "Unable to register with this email address");
                    }
                }
                
            }
            return View(model);  
        }

        public ActionResult ConfirmSent()
        {
            return View();
        }

        public ActionResult Confirm(string id, string email)
        {
            using (IdentityModels entities = new IdentityModels())
            {
                var userStore = new UserStore<User>(entities);

                var manager = new UserManager<User>(userStore);
                manager.UserTokenProvider = new EmailTokenProvider<User>();
                var user = manager.FindByName(email);
                if (user != null)
                {
                    var result = manager.ConfirmEmail(user.Id, id);
                    if (result.Succeeded)
                    {
                        TempData.Add("AccountConfirmed", true);
                        return RedirectToAction("Login");
                    }
                }
            }

            return RedirectToAction("Index", "Home");
        }
        public ActionResult Login()
        {
            return View(new LoginModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                using (IdentityModels entities = new IdentityModels())
                {
                    var userStore = new UserStore<User>(entities);

                    var manager = new UserManager<User>(userStore);

                    var user = manager.FindByEmail(model.EmailAddress);

                    if (manager.CheckPassword(user, model.Password))
                    {
                        FormsAuthentication.SetAuthCookie(model.EmailAddress, true);
                        return RedirectToAction("Index", "Home");
                    }
                    ModelState.AddModelError("EmailAddress", "Could not sign in with this username and/or password");
                }
            }
            return View(model);
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}