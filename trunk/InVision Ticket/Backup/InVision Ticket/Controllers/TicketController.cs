using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InVision_Ticket.Models;
using InVision_Ticket.ViewModels;
using System.Reflection; 

namespace InVision_Ticket.Controllers
{
    public class TicketController : Controller
    {
        //
        // GET: /Ticket/

        public ActionResult Index()
        {
            return View();
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
            return View();
        } 

        //
        // POST: /Ticket/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /Ticket/Edit/5
 
        public ActionResult Edit(int id)
        {
			using (InVisionTicketContext db = new InVisionTicketContext())
			{
				var tquery = (from t in db.Tickets
							  join br in db.BillRates on t.BillRateID equals br.BillRateID
							  join ts in db.TicketStatus on t.StatusID equals ts.TicketStatusID
							  join tt in db.TicketTypes on t.TicketTypeID equals tt.TicketTypeID
							  join c in db.Customers on t.CustomerID equals c.CustomerID
							  join cc in db.CustomerContacts on c.CustomerID equals cc.CustomerID
							  join sl in db.Logins on t.SalesmenLoginID equals sl.LoginID
							  join tl in db.Logins on t.TechnicianLoginID equals tl.LoginID
							  join cl in db.Logins on t.CreatedByLoginID equals cl.LoginID
							  //join lm in db.Logins on t.LastModifiedBy equals lm.LoginID
									where t.TicketID == id
									select new { ticket = t, billrate = br, ticketstatus = ts, 
										tickettype = tt,	cc.FirstName, cc.LastName,
										c.CustomerName, cc.Phone, c.BusinessCustomer, cc.Email,
										Salesman = sl.DisplayName, SalesmanID = sl.LoginID,
										TechnicnanID = tl.LoginID, Technician = tl.DisplayName,
										Createdby = cl.DisplayName }
									).Single();
				if (tquery != null)
				{

					TicketView vm = new TicketView();
					vm.TicketID = id;
					vm.TicketStatus = tquery.ticket.TicketStatus;
					//vm.BillRate.TicketBillRate = tquery.billrate.TicketBillRate;
					//vm.BillRate.BillRateDescription = tquery.billrate.BillRateDescription;
					vm.TicketStatus = tquery.ticketstatus;
					vm.BillRate = tquery.billrate;
					vm.TicketType = tquery.tickettype;
					vm.Summary = tquery.ticket.Summary;
					vm.Details = tquery.ticket.Details;
					vm.CreatedDateTime = tquery.ticket.CreatedDateTime;
					vm.SalesmenLoginID = tquery.ticket.SalesmenLoginID;
					vm.TechnicianLoginID = tquery.ticket.TechnicianLoginID;
					vm.Priority = tquery.ticket.Priority;
					vm.CustomerID = tquery.ticket.CustomerID;
					vm.TicketTypeID = tquery.ticket.TicketTypeID;
					vm.LastModifiedDateTime = tquery.ticket.LastModifiedDateTime;
					vm.ResolvedDateTime = tquery.ticket.ResolvedDateTime;
					vm.LastModifiedBy = tquery.ticket.LastModifiedBy;
					vm.Phone = tquery.Phone;
					if (tquery.ticket.CreatedByLoginID.HasValue)
					{
						vm.CreatedByLoginID = tquery.ticket.CreatedByLoginID.Value;
					}
					else
					{
						vm.CreatedByCustomerID = tquery.ticket.CreatedByCustomerID.Value;
					}
					vm.LocationID = tquery.ticket.LocationID;
					vm.SystemID = tquery.ticket.SystemID;
					vm.SalesmanName = tquery.Salesman;
					vm.TechnicianName = tquery.Technician;
					if (tquery.BusinessCustomer.Value)
					{
						vm.CustomerContactName = tquery.LastName + ", " + tquery.FirstName;
						vm.BusinessName = tquery.CustomerName;
					}
					else
					{
						vm.CustomerContactName = tquery.CustomerName;
					}
					if (tquery.ticket.CreatedByCustomerID.HasValue)
					{
						vm.CreatedByCustomer = true;
					}
					else
					{
						vm.CreatedByLogin = tquery.Createdby;
					}

                    vm.BillRateList = db.BillRates.ToList();
                    vm.LocationList = db.Locations.ToList();
                    vm.TicketStatusList = db.TicketStatus.ToList();
                    vm.TicketTypeList = db.TicketTypes.ToList();

                    vm.Updates = db.Updates.Where(x => x.TicketID == vm.TicketID).ToList();
                    
					return View(vm);
				}
				return View();
			}
		}


        //
        // POST: /Ticket/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
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
