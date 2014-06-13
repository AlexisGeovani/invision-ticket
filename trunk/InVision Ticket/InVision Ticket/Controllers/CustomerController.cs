using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InVision_Ticket.Models;
using System.Data.Entity.Validation;
using System.Data.Entity;
using System.Diagnostics;
using InVision_Ticket.Utilities;
using System.Web.Security;
using System.IO;

namespace InVision_Ticket.Controllers
{
    public class CustomerController : Controller
    {
        //
        // GET: /Customer/

        public ActionResult Index()
        {
			using(InVisionTicketContext db = new InVisionTicketContext())
			{
				var Customers = from c in db.Customers
								join cc in db.CustomerContacts on c.CustomerID equals cc.CustomerID
								select new { c, cc};
				List<CustomerViewModel> CVML = new List<CustomerViewModel>();
				foreach (var c in Customers)
				{
					CustomerViewModel CVM = CustomerCustomerView.ConvertToCustomerViewModel(c.c, c.cc);
					CVML.Add(CVM);				
				}
				return View(CVML);
			}
        }
        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            using (InVisionTicketContext db = new InVisionTicketContext())
            {

                string Search = null;
                if (!string.IsNullOrWhiteSpace(form["SearchText"]))
                {
                    Search = form["SearchText"];
                }
                DateTime? StartDate = null;
                DateTime? EndDate = null;
                if (!string.IsNullOrWhiteSpace(form["StartDate"]))
                {
                    StartDate = Convert.ToDateTime(form["StartDate"]);
                }
                if (!string.IsNullOrWhiteSpace(form["EndDate"]))
                {
                    EndDate = Convert.ToDateTime(form["EndDate"]);
                }
                int? LocationID = null;
                if (form["SearchLocation"] == true.ToString())
                {
                    var user = db.Logins.Where(l => l.Deleted == false).SingleOrDefault(l => l.Email == User.Identity.Name);
                    LocationID = Convert.ToInt32(user.LocationID);
                }
                
                return View(SearchUtility.CustomerSearch(Search, LocationID, StartDate, EndDate));
            }
        }

        //
        // GET: /Customer/Details/5
        public ActionResult DetailsPartial(int id)
        {
            using (InVisionTicketContext db = new InVisionTicketContext())
            {
                CustomerContact CC = db.CustomerContacts.Find(id);
                Customer C = db.Customers.Find(CC.CustomerID);
                CustomerViewModel CVM = CustomerCustomerView.ConvertToCustomerViewModel(C, CC);
                return View(CVM);
            }
        
        }
        public ActionResult Details(int id)
        {
			using (InVisionTicketContext db = new InVisionTicketContext())
			{
				CustomerContact CC = db.CustomerContacts.Find(id);
				Customer C = db.Customers.Find(CC.CustomerID);
				CustomerViewModel CVM = CustomerCustomerView.ConvertToCustomerViewModel(C, CC);
				return View(CVM);
			}
            
        }
        public ActionResult CustomerTickets(long id)
        {
            return View("../Ticket/Index", SearchUtility.TicketSearch(null, null, null, null, null, id, null, null));
        }
        public ActionResult CustomerCSV()
        {
            if (RoleCheck.IsAdministrator(User.Identity.Name))
            {
                using (InVisionTicketContext db = new InVisionTicketContext())
                {
                    var customers = db.CustomerContacts.Include(c => c.Customer).Where(c => c.PromotionalEmails == true);
                    StringWriter sw = new StringWriter();
                    sw.WriteLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\",\"{8}\", \"{9}\", \"{10}\", \"{11}\", \"{12}\", \"{13}\"",
                        new string[] { "CustomerID", "CustomerContactID", "CustomerName", "FirstName", "LastName", "BusinessCustomer", "EmailAddress", "Address1", "Address2", "City", "State", "Zip", "Phone", "Notes" }));

                    foreach (var customer in customers)
                    {
                        sw.WriteLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\",\"{8}\", \"{9}\", \"{10}\", \"{11}\", \"{12}\", \"{13}\"",
                            new string[] {customer.CustomerID.Value.ToString(), customer.CustomerContactID.ToString(), customer.Customer.CustomerName, customer.FirstName, customer.LastName, customer.Customer.BusinessCustomer.Value.ToString(),
                        customer.Email, customer.AddressLine1, customer.AddressLine2, customer.City, customer.State, customer.Zip.Value.ToString(), customer.Phone.ToString(), customer.Customer.CustomerNotes}));

                    }
                    //Byte[] data = upload.Data;
                    //var cd = new System.Net.Mime.ContentDisposition
                    //{
                    //    FileName = upload.FileName,
                    //    Inline = false,

                    //};
                    return File(new System.Text.UTF8Encoding().GetBytes(sw.ToString()), "text/csv", "Customers.csv");
                }
            }
            throw new HttpException(401, "Access Denied");

        
        }

        //
        // GET: /Customer/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Customer/Create

        [HttpPost]
        public ActionResult Create(CustomerViewModel CVM)
        {
			if (ModelState.IsValid)
			{
				try
				{
					using (InVisionTicketContext db = new InVisionTicketContext())
					{
						Customer C = new Customer();
						C.CustomerName = CVM.CustomerName;
						C.CustomerNotes = CVM.CustomerNotes;
						C.BusinessCustomer = C.BusinessCustomer;
						if (C.BusinessCustomer.HasValue)
						{
							if (!C.BusinessCustomer.Value)
							{
								C.CustomerName = CVM.LastName + ", " + CVM.FirstName;
							}
						}
						else 
						{
							C.CustomerName = CVM.LastName + ", " + CVM.FirstName;
						}
						db.Customers.Add(C);
						db.SaveChanges();
						CustomerContact CC = new CustomerContact();
						CC.CustomerID = C.CustomerID;
						CC.FirstName = CVM.FirstName;

						CC.LastName = CVM.LastName;
						CC.Phone = Convert.ToInt64(CVM.PhoneString.Replace("(", "").Replace(")", "").Replace("-", "").Replace(".", ""));
						CC.AddressLine1 = CVM.Address1;
						CC.AddressLine2 = CVM.Address2;
						CC.City = CVM.City;
						CC.Email = CVM.Email;
						CC.PromotionalEmails = CVM.PromotionalEmails;
						CC.State = CVM.State;
						if (CVM.Zip.HasValue)
						{
							CC.Zip = CVM.Zip.Value;
						}
						db.CustomerContacts.Add(CC);
						db.SaveChanges();
						return RedirectToAction("Index");
					}
				}
				catch (DbEntityValidationException dbEx)
				{
					foreach (var validationErrors in dbEx.EntityValidationErrors)
					{
						foreach (var validationError in validationErrors.ValidationErrors)
						{
							Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
						}
					}
				}
				
			}
			return View();
        }
        
        //
        // GET: /Customer/Edit/5
 
        public ActionResult Edit(int id)
        {
			using (InVisionTicketContext db = new InVisionTicketContext())
			{
				CustomerContact CC = db.CustomerContacts.Find(id);
				Customer C = db.Customers.Find(CC.CustomerID);
				CustomerViewModel CVM = CustomerCustomerView.ConvertToCustomerViewModel(C, CC);
				return View(CVM);
			}
        }

        //
        // POST: /Customer/Edit/5

        [HttpPost]
        public ActionResult Edit(CustomerViewModel CVM)
        {
			if (ModelState.IsValid)
			{
				try
				{
					using (InVisionTicketContext db = new InVisionTicketContext())
					{
						Customer C = db.Customers.Find(CVM.CustomerID);
						if (CVM.CustomerName != null)
							C.CustomerName = CVM.CustomerName;
						else
							C.CustomerName = CVM.LastName + ", " + CVM.FirstName;
						if(CVM.CustomerNotes != null)
							C.CustomerNotes = CVM.CustomerNotes;
						C.BusinessCustomer = CVM.BusinessCustomer;

						
						db.SaveChanges();
						CustomerContact CC = db.CustomerContacts.Find(CVM.CustomerContactID);
						CC.CustomerID = C.CustomerID;
						CC.FirstName = CVM.FirstName;

						CC.LastName = CVM.LastName;
						CC.Phone = Convert.ToInt64(CVM.PhoneString.Replace("(", "").Replace(")", "").Replace("-", "").Replace(".", ""));
						CC.AddressLine1 = CVM.Address1;
						CC.AddressLine2 = CVM.Address2;
						CC.City = CVM.City;
						CC.Email = CVM.Email;
						CC.PromotionalEmails = CVM.PromotionalEmails;
						CC.State = CVM.State;
						if (CVM.Zip.HasValue)
						{
							CC.Zip = CVM.Zip.Value;
						}
						db.SaveChanges();
					}
				}
				catch (DbEntityValidationException dbEx)
				{
					foreach (var validationErrors in dbEx.EntityValidationErrors)
					{
						foreach (var validationError in validationErrors.ValidationErrors)
						{
							Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
						}
					}
				}

			}
			return RedirectToAction("Index");
        }

        //
        // GET: /Customer/Delete/5
 
        //public ActionResult Delete(long id)
        //{
        //    using (InVisionTicketContext db = new InVisionTicketContext())
        //    {
        //        CustomerContact CC = db.CustomerContacts.Find(id);
        //        Customer C = db.Customers.Find(CC.CustomerID);
        //        CustomerViewModel CVM = CustomerCustomerView.ConvertToCustomerViewModel(C, CC);

				
        //        return View(CVM);
        //    }
        //}

        ////
        //// POST: /Customer/Delete/5

        //[HttpPost, ActionName("Delete")]
        //public ActionResult DeleteConfirmed(long id)
        //{
        //    //try
        //    //{

        //        using (InVisionTicketContext db = new InVisionTicketContext())
        //        {
        //            long CustomerID = db.CustomerContacts.Find(id).CustomerID.Value;
        //            db.CustomerContacts.Remove(db.CustomerContacts.Find(id));
        //            db.SaveChanges();
        //            db.Customers.Remove(db.Customers.Find(CustomerID));
        //            db.SaveChanges();
        //            return RedirectToAction("Index");
        //        }
        //    //}
        //    //catch
        //    //{
        //    //    return View();
        //    //}
        //}
    }
}
