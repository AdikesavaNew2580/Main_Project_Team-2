using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Employee_Travel_Booking_System_WebApplication.Models;
using System.Net;

namespace Employee_Travel_Booking_System_WebApplication.Controllers.EmployeeMenu
{
    public class employeesDController : Controller
    {
        // GET: Employees
        private Employee_Travel_Booking_SystemDB1Entities db = new Employee_Travel_Booking_SystemDB1Entities();



        // GET: employees/Details/5
        public ActionResult ERDetails()
        {
            var employeeId = (int)Session["EmployeeId"];
            if (employeeId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            employee employee = db.employees.Find(employeeId);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
