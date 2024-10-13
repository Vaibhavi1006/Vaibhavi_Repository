using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Practical_test_application.Models
{
    public class user_category
    {
        public int Id { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<int> CategoryId { get; set; }

     
        public string Name { get; set; }
    }
}