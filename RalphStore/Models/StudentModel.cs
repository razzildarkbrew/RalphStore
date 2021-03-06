﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RalphStore.Models
{
    public class StudentModel
    {

        public int? ID { get; set; }

        [System.ComponentModel.DataAnnotations.Display(Name ="FirstName")]
        public string FirstName { get; set; }


        public string LastName { get; set; }

        public string FavoriteFood { get; set; }
    }
}