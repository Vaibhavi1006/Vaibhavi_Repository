﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Practical_test_application.Models
{
    public class LoginViewModel
    {


        //[RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" + @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" + @".)+))(a-zA-Z]{2,4}|[0-9]{1,3}(\]?)$", ErrorMessage = "Email is not valid")]

        [Required(ErrorMessage = "Please Enter email")]
        public string EmailId { get; set; }


        [Required(ErrorMessage = "Please Enter Password")]


        public string Password { get; set; }

    }
}