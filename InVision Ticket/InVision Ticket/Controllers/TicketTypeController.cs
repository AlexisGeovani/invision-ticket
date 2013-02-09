using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InVision_Ticket.Models;

namespace InVision_Ticket.Controllers
{ 
    public class TicketTypeController : Controller
    {
        private InVisionTicketContext db = new InVisionTicketContext();

        //
        // GET: /TicketType/

        public ViewResult Index()
        {
            return View(db.TicketTypes.ToList());
        }

        //
        // GET: /TicketType/Details/5

        public ViewResult Details(long id)
        {
            TicketType tickettype = db.TicketTypes.Find(id);
            return View(tickettype);
        }

        //
        // GET: /TicketType/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /TicketType/Create

        [HttpPost]
        public ActionResult Create(TicketType tickettype)
        {
            if (ModelState.IsValid)
            {
                db.TicketTypes.Add(tickettype);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(tickettype);
        }
        
        //
        // GET: /TicketType/Edit/5
 
        public ActionResult Edit(long id)
        {
            TicketType tickettype = db.TicketTypes.Find(id);
            return View(tickettype);
        }

        //
        // POST: /TicketType/Edit/5

        [HttpPost]
        public ActionResult Edit(TicketType tickettype)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tickettype).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tickettype);
        }

        //
        // GET: /TicketType/Delete/5
 
        public ActionResult Delete(long id)
        {
            TicketType tickettype = db.TicketTypes.Find(id);
            return View(tickettype);
        }

        //
        // POST: /TicketType/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {            
            TicketType tickettype = db.TicketTypes.Find(id);
            db.TicketTypes.Remove(tickettype);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}