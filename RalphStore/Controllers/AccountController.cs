﻿using System;
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
                        EmailConfirmed = true,
                        Email = model.EmailAddress
                    };
                    IdentityResult result = manager.Create(user, model.Password);
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