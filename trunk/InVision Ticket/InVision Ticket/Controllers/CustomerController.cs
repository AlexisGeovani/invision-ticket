using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InVision_Ticket.Models;
using System.Data.Entity.Validation;
using System.Diagnostics;
using InVision_Ticket.Utilities;

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

        //
        // GET: /Customer/Details/5

        public ActionResult Details(int id)
        {
			using (InVisionTicketContext db = new InVisionTicketContext())
			{
				CustomerContact CC = new CustomerContact();
				CC = db.CustomerContacts.Find(id);
				Customer C = new Customer();
				C = db.Customers.Find(CC.CustomerID);
				CustomerViewModel CVM = CustomerCustomerView.ConvertToCustomerViewModel(C, CC);
				return View(CVM);
			}
            
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
 
        public ActionResult Delete(long id)
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
        // POST: /Customer/Delete/5

		[HttpPost, ActionName("Delete")]
		public ActionResult DeleteConfirmed(long id)
        {
			//try
			//{

				using (InVisionTicketContext db = new InVisionTicketContext())
				{
					long CustomerID = db.CustomerContacts.Find(id).CustomerID.Value;
					db.CustomerContacts.Remove(db.CustomerContacts.Find(id));
					db.SaveChanges();
					db.Customers.Remove(db.Customers.Find(CustomerID));
					db.SaveChanges();
					return RedirectToAction("Index");
				}
            //}
			//catch
			//{
			//    return View();
			//}
        }
    }
}
