using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RalphStore.Models
{
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        [MinLength(7)]
        public string Password { get; set; }
    }
}