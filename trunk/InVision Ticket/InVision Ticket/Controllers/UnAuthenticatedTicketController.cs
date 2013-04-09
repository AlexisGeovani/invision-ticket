using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InVision_Ticket.Models;
using InVision_Ticket.Utilities;
using InVision_Ticket.ViewModels;

namespace InVision_Ticket.Controllers
{ 
    public class UnAuthenticatedTicketController : Controller
    {
        private InVisionTicketContext db = new InVisionTicketContext();



        //
        // GET: /UnAuthenticatedTicket/Create
        public ActionResult GetPhone()
        {
            return View();
        }

        public ActionResult GetCustomerData(string lastName, string phone)
        {
            long lPhone = long.Parse(phone);
            Customer C = db.CustomerContacts.Where(c => c.Phone == lPhone && c.LastName == lastName).First().Customer;
            CustomerContact CC = C.CustomerContacts.First();
            CustomerViewModel CVM = CustomerCustomerView.ConvertToCustomerViewModel(C, CC);
            return View(CVM);
        
        }
        //
        // POST: /UnAuthenticatedTicket/Create

        public ActionResult Create(string CustomerID)
        {
            TicketViewModel vm = new TicketViewModel();

            vm.BillRateList = db.BillRates.ToList();
            vm.LocationList = db.Locations.ToList();
            vm.TicketStatusList = db.TicketStatus.ToList();
            vm.TicketTypeList = db.TicketTypes.ToList();
            vm.LoginList = db.Logins.ToList();
           

            return View(vm);
        }
        [HttpPost]
        public ActionResult Create(TicketViewModel ticket)
        {
            if (ModelState.IsValid)
            {
                db.SaveChanges();
                return RedirectToAction("Index");  
            }
            return View(ticket);
        }
        

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}