using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Employee_Travel_Booking_System_WebApplication.Models;

namespace Employee_Travel_Booking_System_WebApplication.Controllers.AdminMenu
{
    [Authorize]
    public class AdminDashboardController : Controller
    {
        // GET: AdminDashboard
        public ActionResult Index()
        {
            return View();
        }
    }
}