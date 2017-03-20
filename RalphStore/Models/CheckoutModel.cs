using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RalphStore.Models
{
    public class CheckoutModel
    {
        [Display(Name ="Address")]
        public string ShippingAddress1 { get; set; }

        [Display(Name = "Shipping Address Line 2")]
        public string ShippingAddress2 { get; set; }

        [Display(Name = "Shipping City")]
        public string ShippingCity { get; set; }

        [Display(Name = "Shipping State")]
        public string ShippingState { get; set; }

        [Required]
        [Display(Name = "Country")]
        public string ShippingCountry { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(12)]
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }
    }
}