using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Practical_test_application.Models
{
    public class User_view_model
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Enter name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter Phone No")]
        public string Phone { get; set; }
        
        [Required(ErrorMessage = "Enter Email No")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter Password")]
        public string Password { get; set; }

        public Nullable<bool> IsAdmin { get; set; }
        
    }
}