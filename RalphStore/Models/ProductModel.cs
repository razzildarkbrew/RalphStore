using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RalphStore.Models
{
    public class ProductModel
    {
        public int? ID { get; set; }

        public string ProductName { get; set; }

        public string ProductDescription { get; set; }

        public decimal? ProductPrice { get; set; }

        public string[] Images { get; set; }
    }
}