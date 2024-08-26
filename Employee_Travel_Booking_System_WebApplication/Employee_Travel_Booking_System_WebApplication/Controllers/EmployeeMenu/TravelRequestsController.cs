using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Employee_Travel_Booking_System_WebApplication.Models;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;



namespace Employee_Travel_Booking_App.Controllers.EmployeeMenu
{
    public class TravelRequestsController : Controller
    {
        private Employee_Travel_Booking_SystemDB1Entities db = new Employee_Travel_Booking_SystemDB1Entities();
        

        // GET: travelrequests
        public ActionResult Index()
        {
            // Check if EmployeeId is stored in the session
            if (Session["EmployeeId"] != null)
            {
                var employeeId = (int)Session["EmployeeId"];

                // Filter travel requests by EmployeeId
                var travelrequests = db.travelrequests
                    .Where(t => t.employeeid == employeeId)
                    .Include(t => t.employee)
                    .ToList();

                return View(travelrequests);
            }
            else
            {
                // Handle case where EmployeeId is not stored in session
                // Redirect to login page or display an error message
                return RedirectToAction("Login", "EmployeeLogin");
            }
        }


        // GET: travelrequests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            travelrequest travelrequest = db.travelrequests.Find(id);
            if (travelrequest == null)
            {
                return HttpNotFound();
            }
            return View(travelrequest);
        }



        // GET: travelrequests/Create
        public ActionResult Create()
        {
            ViewBag.employeeid = new SelectList(db.employees, "employeeid", "emp_name");
            return View();
        }

        // POST: travelrequests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "reasonfortravel,flightneeded,hotelneeded,departurecity,arrivalcity,departuredate,departuretime,additionalinformation,IdentityProofPath")] travelrequest travelrequest, HttpPostedFileBase IdentityProofFile)
        {

            if (ModelState.IsValid)
            {
                // Check if the EmployeeId session variable exists and is of the correct type
                if (Session["EmployeeId"] != null && Session["EmployeeId"] is int employeeId)
                {
                    var employeeName = (string)Session["EmployeeName"];
                    var employeeEmail = (string)Session["EmployeeEmail"];

                    travelrequest.employeeid = employeeId;
                    travelrequest.employeeemail = employeeEmail;
                    travelrequest.employeename = employeeName;
                    travelrequest.approvalstatus = "Pending";
                    travelrequest.bookingstatus = "Pending";

                    // Handle file upload
                    if (IdentityProofFile != null && IdentityProofFile.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(IdentityProofFile.FileName);
                        var path = Path.Combine(Server.MapPath("~/Uploads/IdentityProofs"), fileName);
                        IdentityProofFile.SaveAs(path);
                        travelrequest.IdentityProofPath = "/Uploads/IdentityProofs/" + fileName;
                    }

                    db.travelrequests.Add(travelrequest);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    throw new Exception("EmployeeId session variable is missing or invalid.");
                }
            }

            ViewBag.employeeid = new SelectList(db.employees, "employeeid", "emp_name", travelrequest.employeeid);
            return View(travelrequest);
        }

        // GET: travelrequests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            travelrequest travelrequest = db.travelrequests.Find(id);
            if (travelrequest == null)
            {
                return HttpNotFound();

            }
            ViewBag.employeeid = new SelectList(db.employees, "employeeid", "emp_name", travelrequest.employeeid);
            return View(travelrequest);
        }

        // POST: travelrequests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, string approvalstatus)
        {
            travelrequest travelrequest = db.travelrequests.Find(id);
            if (travelrequest == null)
            {
                return HttpNotFound();
            }

            // Update only the approval status
            travelrequest.approvalstatus = approvalstatus;

            // Save changes
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: travelrequests/DownloadDetailsPDF/5
        public ActionResult DownloadDetailsPDF(int id)
        {
            var travelRequest = db.travelrequests.Find(id);
            if (travelRequest == null)
            {
                return HttpNotFound();
            }

            // Create a new PDF document
            using (var memoryStream = new MemoryStream())
            {
                var document = new Document(PageSize.A4);
                PdfWriter.GetInstance(document, memoryStream);

                document.Open();

                // Add content to the PDF
                document.Add(new Paragraph("Status of Your Travel Request Details are Available Below"));
                document.Add(new Paragraph($"Travel Request Details for Request Id: {travelRequest.requestid} are as Below"));
                document.Add(new Paragraph($"Employee ID: {travelRequest.employeeid}"));
                document.Add(new Paragraph($"Employee Name: {travelRequest.employeename}"));
                document.Add(new Paragraph($"Employee Mail ID: {travelRequest.employeeemail}"));
                document.Add(new Paragraph($"Reason for Travel: {travelRequest.reasonfortravel}"));
              
                document.Add(new Paragraph($"Departure Date: {travelRequest.departuredate?.ToString("dd-MM-yyyy") ?? "N/A"}"));
                document.Add(new Paragraph($"Departure City: {travelRequest.departurecity}"));
                document.Add(new Paragraph($"Arrival City: {travelRequest.arrivalcity}"));
                document.Add(new Paragraph($"Approval Status: {travelRequest.approvalstatus}"));
                document.Add(new Paragraph($"Booking Status: {travelRequest.bookingstatus}"));

                document.Close();

                // Return the PDF as a file download
                var pdfBytes = memoryStream.ToArray();
                return File(pdfBytes, "application/pdf", "TravelRequestDetails.pdf");
            }
        }











    }
}