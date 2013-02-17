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
    public class BillRateController : Controller
    {
        private InVisionTicketContext db = new InVisionTicketContext();

        //
        // GET: /BillRate/

        public ViewResult Index()
        {
            return View(db.BillRates.ToList());
        }

        //
        // GET: /BillRate/Details/5

        public ViewResult Details(long id)
        {
            BillRate billrate = db.BillRates.Find(id);
            return View(billrate);
        }

        //
        // GET: /BillRate/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /BillRate/Create

        [HttpPost]
        public ActionResult Create(BillRate billrate)
        {
            if (ModelState.IsValid)
            {
                db.BillRates.Add(billrate);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(billrate);
        }
        
        //
        // GET: /BillRate/Edit/5
 
        public ActionResult Edit(long id)
        {
            BillRate billrate = db.BillRates.Find(id);
            return View(billrate);
        }

        //
        // POST: /BillRate/Edit/5

        [HttpPost]
        public ActionResult Edit(BillRate billrate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(billrate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(billrate);
        }

        //
        // GET: /BillRate/Delete/5
 
        public ActionResult Delete(long id)
        {
            BillRate billrate = db.BillRates.Find(id);
            return View(billrate);
        }

        //
        // POST: /BillRate/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {            
            BillRate billrate = db.BillRates.Find(id);
            db.BillRates.Remove(billrate);
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