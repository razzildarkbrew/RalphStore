using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RalphStore.Models;

namespace RalphStore.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View(new RegisterModel());
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
                        Email = model.EmailAddress,
                        EmailConfirmed = true
                    };

                    IdentityResult result = manager.Create(user, model.Password);
                    if (result.Succeeded)
                    {
                        FormsAuthentication.SetAuthCookie(model.EmailAddress, true);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("EmailAddress", "Unable to register with this email address");
                    }
                }
                
            }
            return View(model);
        }
    }
}