using Practical_test_application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Practical_test_application.Controllers
{
    
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginUser(LoginViewModel model)
        {
            practical_test_dbEntities db = new practical_test_dbEntities();

            User User = db.Users.SingleOrDefault(x => x.Email == model.EmailId && x.Password == model.Password);
            string result = "fail";
            
            if (User != null)
            {
                
                if (User.IsAdmin == true)
                {
                    result = "Admin";
                    Session["UserName"] = User.Name;
                }
                else
                {
                    result = "User";
                    Session["UserName"] = User.Name;
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Login Failed";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Registration()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            return View();
        }

        [HttpPost]
        public JsonResult RegisterUser(Registration model)
        {
            practical_test_dbEntities db = new practical_test_dbEntities();

            User User = new User();

            User.Name = model.Name;
            User.Phone = model.Phone;
            User.Email = model.Email;
            User.Password = model.Password;
            User.IsAdmin = false;
            User.Isdeleted = false;

            db.Users.Add(User);
            db.SaveChanges();


            return Json("success", JsonRequestBehavior.AllowGet);
        }

        public ActionResult Showuser()
        {
            
            if (Session["UserName"]== null)
            {
               
                return RedirectToAction("Login", "Admin");
            }
                practical_test_dbEntities db = new practical_test_dbEntities();

                List<User_view_model> listUser = db.Users.Where(x => x.IsAdmin == false).Select(x => new User_view_model { Name = x.Name, Phone = x.Phone, Email = x.Email,Password=x.Password}).ToList();
                ViewBag.UserList = listUser;
                return View();
          
        }

        public ActionResult EditUserInfo()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            practical_test_dbEntities db = new practical_test_dbEntities();

            List<User_view_model> listUser = db.Users.Where(x => x.Isdeleted == false && x.IsAdmin == false).Select(x => new User_view_model { ID=x.Id,Name = x.Name, Phone = x.Phone, Email = x.Email, Password = x.Password }).ToList();
            ViewBag.UserList = listUser;
            return View();

        }

        [HttpPost]
        public ActionResult EditUserInfo(User_view_model model)
        {
            try
            {

                practical_test_dbEntities db = new practical_test_dbEntities();
                List<User> list = db.Users.ToList();
               
                if (model.ID > 0)
                {
                    //update

                    User user = db.Users.SingleOrDefault(x => x.Id == model.ID && x.IsAdmin == false);

                    user.Name = model.Name;
                    user.Name = model.Name;
                    user.Phone = model.Phone;
                    user.Email = model.Email;
                    user.Password = model.Password;
                    db.SaveChanges();

                }
                else
                {
                    //save
                    User user = new User();

                    user.Name = model.Name;
                    user.Phone = model.Phone;
                    user.Email = model.Email;
                    user.Password = model.Password;
                    user.IsAdmin = false;
                    user.Isdeleted = false;
                    db.Users.Add(user);
                    db.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                string x = ex.ToString();
            }
            return View(model);
        }

        public ActionResult AddEditUser(int Id)
        {

            practical_test_dbEntities db = new practical_test_dbEntities();
           
            User_view_model model = new User_view_model();

            if (Id > 0)
            {
                User user = db.Users.SingleOrDefault(x => x.Id == Id && x.IsAdmin == false);
                model.ID = Id;
                model.Name = user.Name; 
                model.Phone = user.Phone;
                model.Email = user.Email;
                model.Password = user.Password;

            }
            return PartialView("Partial2", model);
        }

        public JsonResult DeleteEmployee(int Id)
        {
            practical_test_dbEntities db = new practical_test_dbEntities();
            bool result = false;
            User user= db.Users.SingleOrDefault(x => x.Isdeleted== false &&
            x.Id== Id);
            if (user!= null)
            {
                user.Isdeleted= true;
                db.SaveChanges();
                result = true;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

       
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login", "Admin");
        }

    }
}