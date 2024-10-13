using Practical_test_application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Practical_test_application.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult User_home_page()
        {
            practical_test_dbEntities db = new practical_test_dbEntities();
            int UserId = (int)Session["UserId"];
            List<user_category> listUser = db.UserCategories.Where(x => x.UserId == UserId).Select(x => new user_category { CategoryId = x.CategoryId, UserId = x.UserId}).ToList();
            
            
            ViewBag.UserList = listUser;

            return View();
        }



    }
}