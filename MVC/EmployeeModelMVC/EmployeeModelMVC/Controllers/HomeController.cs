using EmployeeModelMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeModelMVC.Controllers
{
    public class HomeController : Controller
    {
        FSEEntities ee = new FSEEntities();

        public ActionResult Index()
        {

            EmployeeInformation ei = new EmployeeInformation();
            string UserName = Session["UserName"].ToString();
            UserProfile up = ee.UserProfiles.Where(x => x.UserName == UserName).FirstOrDefault();
            if (up.WorkingArea == "Employee")
            {

                return RedirectToAction("Employee");
            }
            else if (up.WorkingArea == "Admin")
            {
                ei.employeeDetails = ee.EmployeeLeaveDeails.Where(x => x.SupervisorId == up.UserId).ToList();

                ViewData["employeeDetails"] = ee.EmployeeLeaveDeails.Where(x => x.SupervisorId == up.UserId).ToList();
                ViewData["EmployeeLeaveDeail"] = ee.EmployeesLeaveTrackings.Where(x => x.SupervisorId == up.UserId && x.IsApproved == false).ToList();
            }
            return View();
        }

        public ActionResult Employee()
        {
            EmployeeInformation ei = new EmployeeInformation();
            string UserName = Session["UserName"].ToString();
            UserProfile up = ee.UserProfiles.Where(x => x.UserName == UserName).FirstOrDefault();
            ei.employeeDetails = ee.EmployeeLeaveDeails.Where(x => x.UserId == up.UserId).ToList();
            ei.employeeTracking = ee.EmployeesLeaveTrackings.Where(x => x.UserId == up.UserId).ToList();

            ViewData["employeeDetails"] = ei.employeeDetails;
            ViewData["EmployeeLeaveDeail"] = ei.employeeTracking;
            return View();
        }

        public ActionResult ApproveEmployee(int Id)
        {
            EmployeesLeaveTracking elt = ee.EmployeesLeaveTrackings.Where(r => r.RowId == Id).FirstOrDefault();
            elt.IsApproved = true;
            ee.SaveChanges();
            return RedirectToAction("Index");

        }
        public ActionResult ApplyLeave(int Id)
        {
            EmployeeLeaveDeail eld = ee.EmployeeLeaveDeails.Where(r => r.UserId == Id).FirstOrDefault();
            EmployeesLeaveTracking elt = new EmployeesLeaveTracking();
           // elt.UserId = eld.UserId;
            return View();
        }
        [HttpPost]
        [ActionName("ApplyLeave")]
        public ActionResult ApplyLeave_Post(EmployeesLeaveTracking elt)
        {
            EmployeesLeaveTracking et = new EmployeesLeaveTracking();
            if (ModelState.IsValid)
            {
                string UserName = Session["UserName"].ToString();
                UserProfile up = ee.UserProfiles.Where(x => x.UserName == UserName).FirstOrDefault();
                EmployeeLeaveDeail eld = ee.EmployeeLeaveDeails.Where(x => x.UserId == up.UserId).First();
                
                TryUpdateModel(et);
                et.UserId = up.UserId;
                et.IsApproved = false;
                et.SupervisorId = up.EmployeeLeaveDeail.SupervisorId;
                ee.EmployeesLeaveTrackings.Add(et);
                eld.LeaveBalances = eld.LeaveBalances - (et.LeaveToDate - et.LeaveFromDate).Days;
                
                ee.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}