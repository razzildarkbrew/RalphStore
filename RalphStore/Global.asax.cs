using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RalphStore.Models;

namespace RalphStore
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        //create a few roles -- if they don't exist
        //TODO: create roles
        //using (IdentityModels entities = new  IdentityModels())
        //{
        //    var rm = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(Entities));
        //    if (!rm.RoleExists("Administrator"))
        //    {
        //        rm.Create(new IdentityRole( {Name = "Administrator"}))
        //    }
        //    var adminRole = rm.FindByName("Administrator");
        //    if (adminRole.Users.Count == 0)
        //    {
        //        var userStore = new UserStore<>();
        //    }
        //}
    
    }
}
