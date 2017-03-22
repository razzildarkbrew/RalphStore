using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace RalphStore.Models
{
    public class IdentityModels : IdentityDbContext<User>
    {
        public IdentityModels()
            : base("name=RalphStore")
        {
            
        }
    }

    public class User : IdentityUser
    {
        
    }
}
