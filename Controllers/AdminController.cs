using Practical_test_application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Practical_test_application.Controllers
{
    
    public class AdminController : Controller
    {
        // GET: Admin
        

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult User_Registration()
        {
            //app_databaseEntities5 db = new app_databaseEntities5();

            //List<Department> list = db.Departments.ToList();
            //ViewBag.departmentlist = new SelectList(list, "DepartmentID", "Department_name");
            return View();
        }


        [HttpPost]
        public ActionResult User_Registration(Registration model)
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

            return View(model);
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
                    Session["UserId"] = User.Id;
                }
                else
                {
                    result = "User";
                    Session["UserName"] = User.Name;
                    Session["UserId"] = User.Id;
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
            if (Session["UserName"] == null || Session["UserName"].ToString() != "Admin")
            {
                return RedirectToAction("Login", "Admin");
            }
            practical_test_dbEntities db = new practical_test_dbEntities();

            List<Category> list = db.Categories.ToList();
            ViewBag.Categorylist = new SelectList(list, "Id", "Name");

            return View();
        }

        public List<Category> GetCountry()
        {
            practical_test_dbEntities db = new practical_test_dbEntities();

            List<Category> Categories = db.Categories.ToList();

            return Categories;
        }

        public ActionResult GetSubCategoryList(int Id)
        {
            practical_test_dbEntities db = new practical_test_dbEntities();
            List<SubCategory> Sub_category_List = db.SubCategories.Where(x => x.CategoryId == Id).ToList();

            ViewBag.Sub_Category_options = new SelectList(Sub_category_List, "Id", "Name");

            return PartialView("Sub_Category_options");
        }
    
    [HttpPost]
        public JsonResult RegisterUser(Registration model)
        {
            practical_test_dbEntities db = new practical_test_dbEntities();

            User User = new User();

            UserCategory usercategory = new UserCategory();

            User.Name = model.Name;
            User.Phone = model.Phone;
            User.Email = model.Email;
            User.Password = model.Password;
            User.IsAdmin = false;
            User.Isdeleted = false;

           
            db.Users.Add(User);
            db.SaveChanges();

            int latestUserId = User.Id;
            usercategory.UserId = latestUserId;
            usercategory.CategoryId = model.Category;
            db.UserCategories.Add(usercategory);
            db.SaveChanges();
            return Json("success", JsonRequestBehavior.AllowGet);
        }

        public ActionResult Showuser()
        {
            
            if (Session["UserName"]== null || Session["UserName"].ToString() != "Admin")
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
            if (Session["UserName"] == null || Session["UserName"].ToString() != "Admin")
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

        public ActionResult Admin_home()
        {
              if (Session["UserName"] == null || Session["UserName"].ToString() != "Admin")
            {
                return RedirectToAction("Login", "Admin");
            }
            return View();
        }

    }
}