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
using MarkdownDeep;
using System.Data.Entity.Validation;

namespace InVision_Ticket.Controllers
{ 
    public class UnAuthenticatedTicketController : Controller
    {
        private InVisionTicketContext db = new InVisionTicketContext();



        //
        // GET: /UnAuthenticatedTicket/Create
        public ActionResult Index()
        {
            return RedirectToAction("GetPhone");
        }
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

        public ActionResult Create(int id)
        {
            if (!db.CustomerContacts.Any(cc => cc.CustomerContactID == id))
            { 
                throw new HttpException(400, "No Such Customer");
            }
            TicketViewModel vm = new TicketViewModel();
            vm.CustomerContactID = id;
            vm.CustomerID = db.CustomerContacts.Find(id).CustomerID.Value;
            vm.LocationList = db.Locations.ToList();
            vm.TicketStatusList = db.TicketStatus.ToList();
            vm.TicketTypeList = db.TicketTypes.ToList();
            vm.LoginList = db.Logins.ToList();
           

            return View(vm);
        }
        [HttpPost]
        public ActionResult Create(TicketViewModel TVM)
        {
            try
            {
                Ticket ticket = new Ticket();
                Models.System system = new Models.System();
                ticket.Summary = TVM.Summary;
                Markdown md = new Markdown();
                ticket.CustomerID = TVM.CustomerID;
                ticket.LocationID = TVM.Location.LocationID;
                ticket.Details = md.Transform(TVM.DetailsMarkDown);
                ticket.DetailsMarkDown = TVM.DetailsMarkDown;
                ticket.Priority = TVM.Priority;
                ticket.TicketTypeID = TVM.TicketTypeID;
                system.Desciption = TVM.System.Desciption;
                system.Type = TVM.System.Type;
                system.CustomerID = TVM.CustomerID;
                db.Systems.Add(system);
                db.SaveChanges();
                ticket.SystemID = system.SystemID;
                ticket.StatusID = 103;
                ticket.Priority = 4;
                ticket.CreatedDateTime = System.DateTime.Now;
                ticket.CreatedByCustomerID = ticket.CustomerID;
                db.Tickets.Add(ticket);
                db.SaveChanges();
                return RedirectToAction("Index");
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
        }
        

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}