﻿using System;
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

namespace InVision_Ticket.Controllers
{
    public class TicketController : Controller
    {
        //
        // GET: /Ticket/

        public ActionResult Index()
        {
            var userData = ((FormsIdentity)User.Identity).Ticket.UserData.Split(':');
            var role = userData[1];
            var store = userData[0];

            using(InVisionTicketContext db = new InVisionTicketContext())
            {
                var ticketsq = db.Tickets.Where(t => t.TicketStatus.Open == true);
                return View(Mapper.Map<List<Ticket>, List<TicketListViewModel>>(ticketsq.ToList()));
            }
            


         //   return View();
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

                vm.BillRateList = db.BillRates.ToList();
                vm.LocationList = db.Locations.ToList();
                vm.TicketStatusList = db.TicketStatus.ToList();
                vm.TicketTypeList = db.TicketTypes.ToList();
                vm.LoginList = db.Logins.ToList();
                vm.Updates = db.Updates.Where(x => x.TicketID == vm.TicketID).ToList();
                vm.Customers = db.Customers.Include("CustomerContacts").Where(c => c.CustomerContacts.Count > 0).ToList();
                return View(vm);
            }
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
                    var ticket = db.Tickets.Find(id);
#region OldShit
                    //var tquery = (from t in db.Tickets
                    //              join br in db.BillRates on t.BillRateID equals br.BillRateID
                    //              join ts in db.TicketStatus on t.StatusID equals ts.TicketStatusID
                    //              join tt in db.TicketTypes on t.TicketTypeID equals tt.TicketTypeID
                    //              join c in db.Customers on t.CustomerID equals c.CustomerID
                    //              join cc in db.CustomerContacts on c.CustomerID equals cc.CustomerID
                    //              join sl in db.Logins on t.SalesmenLoginID equals sl.LoginID
                    //              join tl in db.Logins on t.TechnicianLoginID equals tl.LoginID
                    //              join cl in db.Logins on t.CreatedByLoginID equals cl.LoginID
                    //              join loc in db.Locations on t.LocationID equals loc.LocationID
                    //              join sys in db.Systems on t.SystemID equals sys.SystemID
                    //              //join lm in db.Logins on t.LastModifiedBy equals lm.LoginID
                    //                    where t.TicketID == id
                    //                    select new { ticket = t, billrate = br, ticketstatus = ts, 
                    //                        tickettype = tt,	cc.FirstName, cc.LastName, cc.CustomerContactID,
                    //                        c.CustomerName, cc.Phone, c.BusinessCustomer, cc.Email,
                    //                        Salesman = sl.DisplayName, SalesmanID = sl.LoginID,
                    //                        TechnicnanID = tl.LoginID, Technician = tl.DisplayName,
                    //                        Createdby = cl.DisplayName, location = loc, system = sys }
                    //                    ).Single();
                    //if (tquery != null)
                    //{

                    //    TicketViewModel vm = new TicketViewModel();
                    //    vm.TicketID = id;
                    //    vm.TicketStatus = tquery.ticket.TicketStatus;
                    //    //vm.BillRate.TicketBillRate = tquery.billrate.TicketBillRate;
                    //    //vm.BillRate.BillRateDescription = tquery.billrate.BillRateDescription;
                    //    vm.TicketStatus = tquery.ticketstatus;
                    //    vm.BillRate = tquery.billrate;
                    //    vm.TicketType = tquery.tickettype;
                    //    vm.Summary = tquery.ticket.Summary;
                    //    vm.Details = tquery.ticket.Details;
                    //    vm.CreatedDateTime = tquery.ticket.CreatedDateTime;
                    //    vm.Priority = tquery.ticket.Priority;
                    //    vm.CustomerID = tquery.ticket.CustomerID;
                    //    vm.TicketTypeID = tquery.ticket.TicketTypeID;
                    //    vm.LastModifiedDateTime = tquery.ticket.LastModifiedDateTime;
                    //    vm.ResolvedDateTime = tquery.ticket.ResolvedDateTime;
                    //    vm.LastModifiedBy = tquery.ticket.LastModifiedBy;
                    //    vm.Phone = tquery.Phone;
                    //    vm.CustomerContactID = tquery.CustomerContactID;
                    //    if (tquery.ticket.CreatedByLoginID.HasValue)
                    //    {
                    //        vm.CreatedByLoginID = tquery.ticket.CreatedByLoginID.Value;
                    //    }
                    //    else
                    //    {
                    //        vm.CreatedByCustomerID = tquery.ticket.CreatedByCustomerID.Value;
                    //    }
                    //    vm.Location = tquery.location;
                    //    vm.System = tquery.system;
                    //    if (tquery.BusinessCustomer.Value)
                    //    {
                    //        vm.CustomerContactName = tquery.LastName + ", " + tquery.FirstName;
                    //        vm.BusinessName = tquery.CustomerName;
                    //    }
                    //    else
                    //    {
                    //        vm.CustomerContactName = tquery.CustomerName;
                    //    }
                    //    if (tquery.ticket.CreatedByCustomerID.HasValue)
                    //    {
                    //        vm.CreatedByCustomer = true;
                    //    }
                    //    else
                    //    {
                    //        vm.CreatedByLogin = tquery.Createdby;
                    //    } 
                    #endregion
                    TicketViewModel vm = Mapper.Map<Ticket, TicketViewModel>(ticket);
          
                    
                    
                    
                    vm.BillRateList = db.BillRates.ToList();
                    vm.LocationList = db.Locations.ToList();
                    vm.TicketStatusList = db.TicketStatus.ToList();
                    vm.TicketTypeList = db.TicketTypes.ToList();
                    vm.LoginList = db.Logins.Where(l => l.LocationID == vm.Location.LocationID).ToList();
                    vm.Updates = db.Updates.Where(x => x.TicketID == vm.TicketID).ToList();

                    return View(vm);
			}
		}


        //
        // POST: /Ticket/Edit/5

        [HttpPost]
        public ActionResult Edit(TicketViewModel vm)
        {
            Markdown md = new Markdown();
            vm.Details = md.Transform(vm.DetailsMarkDown);
            Ticket ticket = Mapper.Map<TicketViewModel, Ticket>(vm);

            return RedirectToAction("Index");
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