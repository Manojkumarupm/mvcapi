using EmployeeModelMVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EmployeeModelMVC.Controllers
{
    public class AdminController : Controller
    {
        FSEEntities ee = new FSEEntities();
        //
        // GET: /Admin/
        public ActionResult Index()
        {
            
            return RedirectToAction("LogIn", "Admin");
        }
        [HttpGet]
        public ActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LogIn(Models.Registration userr)
        {
            //if (ModelState.IsValid)  
            //{  
            if (IsValid(userr.FirstName, userr.Password))
            {
                FormsAuthentication.SetAuthCookie(userr.FirstName, false);
                Session["UserName"] = userr.FirstName;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Login details are wrong.");
            }
            return View(userr);
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(Models.Registration user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UserProfile up = new UserProfile();
                    Registration newUser = new Registration();
                    up.UserName = user.FirstName;
                    up.FirstName = user.FirstName;
                    up.Email = user.Email;
                    up.LastName = user.LastName;
                    up.Password = user.Password;
                    up.WorkingArea = user.WorkingArea;
                    up.IsActive = true;
                    //  newUser.IPAddress = "642 White Hague Avenue";
                    ee.UserProfiles.Add(up);
                    ee.SaveChanges();
                    return RedirectToAction("Login", "Admin");
                }
                else
                {
                    ModelState.AddModelError("", "Data is not correct");
                }
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            return View();
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Admin");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private bool IsValid(string UserName, string password)
        {
            bool IsValid = false;
            var user = ee.UserProfiles.FirstOrDefault(u => u.UserName == UserName);
            if (user != null)
            {
                if (user.Password == password)
                {
                    IsValid = true;
                }
            }
            return IsValid;
        }
    }
}