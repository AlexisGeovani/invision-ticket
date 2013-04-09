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
    public class TicketStatusController : Controller
    {
        private InVisionTicketContext db = new InVisionTicketContext();

        //
        // GET: /TicketStatus/

        public ViewResult Index()
        {
            return View(db.TicketStatus.ToList());
        }

        //
        // GET: /TicketStatus/Details/5

        public ViewResult Details(long id)
        {
            TicketStatus ticketstatus = db.TicketStatus.Find(id);
            return View(ticketstatus);
        }

        //
        // GET: /TicketStatus/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /TicketStatus/Create

        [HttpPost]
        public ActionResult Create(TicketStatus ticketstatus)
        {
            if (ModelState.IsValid)
            {
                db.TicketStatus.Add(ticketstatus);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(ticketstatus);
        }
        
        //
        // GET: /TicketStatus/Edit/5
 
        public ActionResult Edit(long id)
        {
            if (id == 103)
            {
                throw new HttpException(403, "This status cannot be modified.");
            
            }
            TicketStatus ticketstatus = db.TicketStatus.Find(id);
            return View(ticketstatus);
        }

        //
        // POST: /TicketStatus/Edit/5

        [HttpPost]
        public ActionResult Edit(TicketStatus ticketstatus)
        {
            if (ticketstatus.TicketStatusID == 103)
            {
                throw new HttpException(403, "This status cannot be modified.");

            }
            if (ModelState.IsValid)
            {
                db.Entry(ticketstatus).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ticketstatus);
        }

        //
        // GET: /TicketStatus/Delete/5
 
        public ActionResult Delete(long id)
        {
            if (id == 103)
            {
                throw new HttpException(403, "This status cannot be modified.");

            }
            TicketStatus ticketstatus = db.TicketStatus.Find(id);
            return View(ticketstatus);
        }

        //
        // POST: /TicketStatus/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            if (id == 103)
            {
                throw new HttpException(403, "This status cannot be modified.");

            }
            TicketStatus ticketstatus = db.TicketStatus.Find(id);
            db.TicketStatus.Remove(ticketstatus);
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