using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Employee_Travel_Booking_System_WebApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public FileResult GetTravelPolicy()
        {
            string filePath = Server.MapPath("~/Documents/EMPX_Travel_Policy.pdf");
            string contentType = "application/pdf";
            Response.AppendHeader("Content-Disposition", "inline; filename=EMPX_Travel_Policy.pdf");
            return File(filePath, contentType);
        }
    }
}
