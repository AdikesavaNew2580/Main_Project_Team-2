using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Employee_Travel_Booking_System_WebApplication.Models;
using Employee_Travel_Booking_System_WebApplication.Services;

namespace Employee_Travel_Booking_App.Controllers.TravelAgentMenu
{
    public class AgentDashboardController : Controller
    {
        private readonly Employee_Travel_Booking_SystemDB1Entities db;
        private readonly SmsService _smsService;

        public AgentDashboardController()
        {
            db = new Employee_Travel_Booking_SystemDB1Entities();

            // Initialize the SmsService with your Twilio credentials
            _smsService = new SmsService("ACd8b56e926f90382215ce59b78675aac6", "f0df3a732cd7a0f811561797c626a253", "+19044400460");
        }


        // GET: AgentDash
        public ActionResult AgentIndex()
        {

            // Retrieve pending travel requests and pass them to the view
            var pendingRequests = db.travelrequests;
            return View(pendingRequests);
        }




        // Action to update the status of a travel request
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateStatus(int requestId, string status)
        {
            try
            {
                var travelRequest = db.travelrequests.FirstOrDefault(tr => tr.requestid == requestId);
                if (travelRequest == null)
                {
                    throw new Exception("Travel request not found.");
                }

                travelRequest.bookingstatus = status;
                db.SaveChanges();
                TempData["SuccessMessage"] = "Booking status updated successfully.";

                if (status.Equals("Confirmed", StringComparison.OrdinalIgnoreCase))
                {
                    var employee = db.employees.FirstOrDefault(e => e.employeeid == travelRequest.employeeid);
                    if (employee != null)
                    {
                        string message = $"Dear {employee.emp_name}, your travel request (ID: {requestId}) has been confirmed. You will get further Details Soon.... Thank you";
                        await _smsService.SendSmsAsync(employee.phonenumber, message);
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return RedirectToAction("AgentIndex");
        }
    }
}