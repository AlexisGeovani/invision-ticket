using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InVision_Ticket.Models;

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
				var QueryResults = (from t in db.Tickets
									join br in db.BillRates on t.BillRateID equals br.BillRateID
									join ts in db.TicketStatus on t.StatusID equals ts.TicketStatusID
									join tt in db.TicketTypes on t.TicketTypeID equals tt.TicketTypeID
									join c in db.Customers on t.CustomerID equals c.CustomerID
									join cc in db.CustomerContacts on c.CustomerID equals cc.CustomerID
									join l in db.Logins on t.SalesmenLoginID equals l.LoginID
									join 
					
					)
			
			

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
