using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Employee_Travel_Booking_System_WebApplication.Models;
using System.Web.Mvc.Html;

namespace Employee_Travel_Booking_System_WebApplication.Controllers.EmployeeMenu
{
    public class EmployeeLoginController : Controller
    {

        private readonly Employee_Travel_Booking_SystemDB1Entities db; // Replace YourDbContext with your actual DbContext class

        public EmployeeLoginController()
        {
            db = new Employee_Travel_Booking_SystemDB1Entities(); // Initialize your DbContext
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                {
                    throw new Exception("Email and password are required.");
                }

                // Retrieve the admin by email
                var emp = db.employees.FirstOrDefault(a => a.email == email && a.emp_password == password);

                // Validate admin existence and password
                if (emp != null)
                {
                    Session["EmployeeId"] = emp.employeeid;
                    Session["EmployeeName"] = emp.emp_name;
                    Session["EmployeeEmail"] = emp.email;
                    // FormsAuthentication.SetAuthCookie(email, false);
                    // Redirect to the dashboard controller
                    return RedirectToAction("Index", "TravelRequests");
                }
                else
                {
                    throw new Exception("Invalid email or password.");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        public ActionResult Logout()
        {
            //FormsAuthentication.SignOut();
            return RedirectToAction("Login", "EmployeeLogin");
        }

        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                {
                    throw new Exception("Email is required.");
                }

                var emp = db.employees.FirstOrDefault(a => a.email == email);
                if (emp == null)
                {
                    throw new Exception("Email not found.");
                }

                // Redirect to the ResetPassword action with the employee ID
                return RedirectToAction("ResetPassword", new { id = emp.employeeid });
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }


        [HttpGet]
        public ActionResult ResetPassword(int id)
        {
            var emp = db.employees.FirstOrDefault(a => a.employeeid == id);
            if (emp == null)
            {
                TempData["ErrorMessage"] = "Invalid password reset request.";
                return RedirectToAction("Login");
            }

            ViewBag.EmployeeId = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(int id, string password, string confirmPassword)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirmPassword))
                {
                    throw new Exception("Password and confirm password are required.");
                }

                if (password != confirmPassword)
                {
                    ModelState.AddModelError("PasswordMismatch", "Passwords do not match.");
                    ViewBag.EmployeeId = id;
                    return View();
                }

                var emp = db.employees.FirstOrDefault(a => a.employeeid == id);
                if (emp == null)
                {
                    throw new Exception("Invalid password reset request.");
                }

                emp.emp_password = password;
                db.SaveChanges();

                TempData["SuccessMessage"] = "Password has been reset successfully.";
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }
    }
}