using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InVision_Ticket.Models;
using InVision_Ticket.ViewModels;
using System.Reflection;
using AutoMapper;
using System.Security.Principal;
using System.Web.Security;
using MarkdownDeep;
using System.Data.Objects.SqlClient;
using System.Data.Entity.Validation;
using System.Data;
using System.Data.Entity;
using System.IO;
using InVision_Ticket.Utilities;

namespace InVision_Ticket.Controllers
{
    public class TicketController : Controller
    {
        public ActionResult DeleteUpdate(int id)
        {
            using (InVisionTicketContext db = new InVisionTicketContext())
            {
                ViewBag.id = id;
                return View(db.Updates.Find(id));
            }
            
        }
        [HttpPost]
        public ActionResult DeleteUpdate(FormCollection form)
        {
            using (InVisionTicketContext db = new InVisionTicketContext())
            {
                int id = int.Parse(form[0]);
                db.Updates.Remove(db.Updates.Find(id));
                db.SaveChanges();
            }

            return RedirectToAction("CloseWindow", "Home");
        }
        public ActionResult DownloadUpload(int id)
        {
            using (InVisionTicketContext db = new InVisionTicketContext())
            {
                Upload upload = db.Uploads.Find(id);
                Byte[] data = upload.Data;
                var cd = new System.Net.Mime.ContentDisposition
                {
                    FileName = upload.FileName,
                    Inline = false,

                };
                Response.AppendHeader("Content-Disposition", cd.ToString());
                return (File(data, "application/octet-stream"));
            }
        
        }
        public ActionResult DeleteUpload(int id)
        { 
            using(InVisionTicketContext db = new InVisionTicketContext())
            {
                Upload upload = db.Uploads.Find(id);
                ViewBag.Description = upload.Description;
                ViewBag.FileName = upload.FileName;
                ViewBag.id = id;
                return View();
            }
            
        }
        [HttpPost]
        public ActionResult DeleteUpload(FormCollection form)
        {
            using (InVisionTicketContext db = new InVisionTicketContext())
            {
                int id = int.Parse(form[0]);
                db.Uploads.Remove(db.Uploads.Find(id));
                db.SaveChanges();
            }
            return RedirectToAction("CloseWindow", "Home");
        
        }
        public ActionResult Upload(int id)
        {
            

            UploadViewModel upload = new UploadViewModel();
            upload.TicketID = id;

            return View(upload);
        }
        [HttpPost]
        public ActionResult Upload(UploadViewModel upload)
        {
            using(InVisionTicketContext db = new InVisionTicketContext())
            {
                
                var u = new Upload();
                u.TicketID = upload.TicketID;
                MemoryStream target = new MemoryStream();
                upload.File.InputStream.CopyTo(target);
                u.Data = target.ToArray();
                u.Description = upload.Description;
                u.FileName = upload.File.FileName;
                db.Uploads.Add(u);
                db.SaveChanges();
            }
            return RedirectToAction("CloseWindow", "Home");
        }
        public ActionResult UpdateTicket(int id)
        {
            using(InVisionTicketContext db = new InVisionTicketContext())
            {
                var userID = db.Logins.Single(l => l.Email == User.Identity.Name).LoginID;
                UpdateViewModel UVM = new UpdateViewModel();
                UVM.BillRateList = db.BillRates.ToList();
                UVM.LoginID = userID;
                UVM.TicketID = id;
                return View(UVM);
            }
        }
        [HttpPost]
        public ActionResult UpdateTicket(UpdateViewModel UVM)
        {
            if (ModelState.IsValid)
            {
                using (InVisionTicketContext db = new InVisionTicketContext())
                {

                    Update update = Mapper.Map<UpdateViewModel, Update>(UVM);
                    update.ActivityDateTime = System.DateTime.Now;

                    Markdown md = new Markdown();
                    update.Comment = md.Transform(UVM.CommentMarkDown);
                    db.Updates.Add(update);
                    db.SaveChanges();
                    return RedirectToAction("CloseWindow", "Home");
                }
            }

            return View(UVM);
        }
        //
        // GET: /Ticket/

        public ActionResult Index()
        {
            
            using(InVisionTicketContext db = new InVisionTicketContext())
            {
                var userID = db.Logins.Single(l => l.Email == User.Identity.Name).LoginID;
                ViewBag.userID = db.Logins.Single(l => l.Email == User.Identity.Name).LoginID;
                var tickets = SearchUtility.TicketSearch(null, null, null, null, true, null, null);
               
                //var ticketsq = db.Tickets.Where(t => t.TicketStatus.Open == true);
                return View(tickets);
            }
         //   return View();
        }
        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            string Search = null;
            if(!string.IsNullOrWhiteSpace(form["SearchText"]))
            {
                Search = form["SearchText"];
            }
            DateTime? StartDate = null;
            DateTime? EndDate = null;
            if(!string.IsNullOrWhiteSpace(form["StartDate"]))
            {
                StartDate = Convert.ToDateTime(form["StartDate"]);
            }
            if (!string.IsNullOrWhiteSpace(form["EndDate"]))
            {
                EndDate = Convert.ToDateTime(form["EndDate"]);
            }
            bool? OpenStatusOnly = null;
            if (!string.IsNullOrWhiteSpace(form["StatusSearch"]))
            {
                OpenStatusOnly = Convert.ToBoolean(form["StatusSearch"]);
            }
            int? LocationID = null;
            if(!string.IsNullOrWhiteSpace(form["SearchLocation"]))
            {
                var userData = ((FormsIdentity)User.Identity).Ticket.UserData.Split(':');
                LocationID = Convert.ToInt32(userData[0]);
            }
            long? CustomerID = null;
            if(!string.IsNullOrWhiteSpace(form["CustomerID"]))
            {
                CustomerID = Convert.ToInt64(form["CustomerID"]);
            }
            int? LoginID = null;
            if (!string.IsNullOrEmpty(form["LoginID"]))
            {
                LoginID = Convert.ToInt32(form["LoginID"]);
            }
            using (InVisionTicketContext db = new InVisionTicketContext())
            {
                var userID = db.Logins.Single(l => l.Email == User.Identity.Name).LoginID;
                ViewBag.userID = db.Logins.Single(l => l.Email == User.Identity.Name).LoginID;
                var tickets = SearchUtility.TicketSearch(Search, LocationID, StartDate, EndDate, OpenStatusOnly, CustomerID, LoginID);

                //var ticketsq = db.Tickets.Where(t => t.TicketStatus.Open == true);
                return View(tickets);
            }
           
        }
		////
		//// GET: /Ticket/Details/5

		//public ActionResult Details(int id)
		//{
		//    return View();
		//}

        //
        // GET: /Ticket/Create

        public ActionResult Create()
        {
            using(InVisionTicketContext db = new InVisionTicketContext())
            {
                TicketViewModel vm = new TicketViewModel();

                vm.LocationList = db.Locations.ToList();
                vm.TicketStatusList = db.TicketStatus.ToList();
                vm.TicketTypeList = db.TicketTypes.ToList();
                vm.LoginList = db.Logins.Where(l=>l.Deleted == false).ToList();
                //vm.Updates = db.Updates.Where(x => x.TicketID == vm.TicketID).ToList();
                var tempCustomers =
                    from c in db.Customers
                    let cc = db.CustomerContacts.FirstOrDefault(x => x.CustomerID == c.CustomerID)
                    where cc.Phone != null && c.CustomerName != null
                    select new { CustomerContactID = cc.CustomerContactID, CustomerValue = c.CustomerName + " | " + SqlFunctions.StringConvert((double)cc.Phone) };
                vm.Customers = new SelectList(
                        tempCustomers.ToList(),
                        "CustomerContactID",
                        "CustomerValue"
                    );
                List<SelectListItem> systems = new List<SelectListItem>();
                var emptyItem = new SelectListItem(){
                    Value = "1",
                    Text = "Select A Customer"
                };
                systems.Add(emptyItem);
                vm.Systems = new SelectList(systems);
                
                return View(vm);
            }
        } 

        //
        // POST: /Ticket/Create

        [HttpPost]
        public ActionResult Create(TicketViewModel vm)
        {
            try
            {
                using (InVisionTicketContext db = new InVisionTicketContext())
                //TODO: Add insert logic here
                {
                    var user = HttpContext.User.Identity;
                    vm.CustomerID = db.CustomerContacts.Find(vm.CustomerContactID).CustomerID.Value;
                    Ticket ticket = Mapper.Map<TicketViewModel, Ticket>(vm);
                    ticket.CreatedDateTime = DateTime.Now;
                    ticket.CustomerID = vm.CustomerID;
                    Models.System system = new Models.System();
                    Markdown md = new Markdown();
                    ticket.Details = md.Transform(vm.DetailsMarkDown);

                    if (vm.NewSystem)
                    {
                        system.Desciption = vm.System.Desciption;
                        system.Type = vm.System.Type;
                        system.CustomerID = ticket.CustomerID;
                    }
                    else
                    {
                        ticket.SystemID = vm.System.SystemID;
                    }


                    if (vm.NewSystem)
                    {
                        db.Systems.Add(system);
                        db.SaveChanges();
                        ticket.SystemID = system.SystemID;
                    }
                    var username = HttpContext.User.Identity.Name;
                    ticket.CreatedByLoginID = db.Logins.Single(u => u.Email == username).LoginID;
                    ticket.LocationID = db.Logins.Single(u => u.Email == username).LocationID;
                    ticket.System = null;
                    ticket.StatusID = 103;
                    db.Tickets.Add(ticket);
                    db.SaveChanges();

                }
                return RedirectToAction("Index");
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    System.Diagnostics.Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }
        
        //
        // GET: /Ticket/Edit/5
 
        public ActionResult Edit(int id)
        {
			using (InVisionTicketContext db = new InVisionTicketContext())
			{
                
                var ticket = db.Tickets.Find(id);
                TicketViewModel vm = Mapper.Map<Ticket, TicketViewModel>(ticket);

                vm.SalesLogin = db.Logins.Find(ticket.SalesmanLoginID);
                vm.TechLogin = db.Logins.Find(ticket.TechnicianLoginID);
                vm.Location = db.Locations.Find(ticket.LocationID);
                vm.TicketType = db.TicketTypes.Find(ticket.TicketTypeID);
                    
                    
                vm.LocationList = db.Locations.ToList();
                vm.TicketStatusList = db.TicketStatus.ToList();
                vm.TicketTypeList = db.TicketTypes.ToList();
                vm.LoginList = db.Logins.Where(l => l.LocationID == vm.Location.LocationID).Where(l => l.Deleted == false).ToList();
                vm.Updates = db.Updates.Include(u => u.Login).Include(u => u.BillRate).Where(x => x.TicketID == vm.TicketID).ToList();
                vm.Uploads = db.Uploads.Where(u => u.TicketID == vm.TicketID).ToList();
                return View(vm);
			}
		}


        //
        // POST: /Ticket/Edit/5

        [HttpPost]
        public ActionResult Edit(TicketViewModel vm)
        {
            using (InVisionTicketContext db = new InVisionTicketContext())
            {
                long userID = db.Logins.Single(l => l.Email == User.Identity.Name).LoginID;

                var ticket = db.Tickets.Find(vm.TicketID);
                ticket.TechnicianLoginID = vm.TechLogin.LoginID;
                ticket.SalesmanLoginID = vm.SalesLogin.LoginID;

                ticket.LastModifiedDateTime = System.DateTime.Now;
                ticket.LastModifiedBy = userID;
                if (!vm.Summary.Equals(ticket.Summary))
                    ticket.Summary = vm.Summary;
                if (!vm.DetailsMarkDown.Equals(ticket.DetailsMarkDown))
                {
                    Markdown md = new Markdown();
                    ticket.Details = md.Transform(vm.DetailsMarkDown);
                    ticket.DetailsMarkDown = vm.DetailsMarkDown;
                }
                if (ticket.Priority != vm.Priority)
                    ticket.Priority = vm.Priority;
                if (ticket.TicketTypeID != vm.TicketTypeID)
                    ticket.TicketTypeID = vm.TicketTypeID;
                if (ticket.LocationID != vm.Location.LocationID)
                {
                    ticket.LocationID = vm.Location.LocationID;
                    ticket.SalesmanLoginID = null;
                    ticket.TechnicianLoginID = null;
                }
                if (ticket.TicketStatus.TicketStatusID != vm.TicketStatus.TicketStatusID)
                    ticket.StatusID = vm.TicketStatus.TicketStatusID;
                db.SaveChanges();
                return RedirectToAction("CloseWindow", "Home");
            }
        }

        //
        // GET: /Ticket/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Ticket/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

    }
}
