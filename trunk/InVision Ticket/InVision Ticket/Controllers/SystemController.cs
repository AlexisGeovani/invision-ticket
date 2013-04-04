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
    public class SystemController : Controller
    {
        private InVisionTicketContext db = new InVisionTicketContext();

        public ActionResult SystemsList(long id)
        {
            long cid = db.CustomerContacts.Find(id).Customer.CustomerID;
            var systems = db.Systems.Where(s => s.CustomerID == cid);
            var x = systems.ToList();
            return View(systems.ToList());
        }
        public ActionResult DetailsPartial(long id)
        {
            Models.System system = db.Systems.Find(id);
            return View(system);
        }
        //
        // GET: /System/

        public ViewResult Index()
        {
            var systems = db.Systems.Include(s => s.Customer);
            return View(systems.ToList());
        }

        //
        // GET: /System/Details/5

        public ViewResult Details(long id)
        {
            Models.System system = db.Systems.Find(id);
            return View(system);
        }

        //
        // GET: /System/Create

        public ActionResult Create()
        {
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CustomerName");
            return View();
        } 

        //
        // POST: /System/Create
        public ActionResult CreatePartial(int id)
        {
            ViewBag.CustomerID = id;
            return View();
        }

        [HttpPost]
        public ActionResult Create(Models.System system)
        {
            if (ModelState.IsValid)
            {
                db.Systems.Add(system);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CustomerName", system.CustomerID);
            return View(system);
        }
        
        //
        // GET: /System/Edit/5
 
        public ActionResult Edit(long id)
        {
            Models.System system = db.Systems.Find(id);
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CustomerName", system.CustomerID);
            return View(system);
        }

        //
        // POST: /System/Edit/5

        [HttpPost]
        public ActionResult Edit(Models.System system)
        {
            if (ModelState.IsValid)
            {
                db.Entry(system).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CustomerName", system.CustomerID);
            return View(system);
        }

        //
        // GET: /System/Delete/5
 
        public ActionResult Delete(long id)
        {
            Models.System system = db.Systems.Find(id);
            return View(system);
        }

        //
        // POST: /System/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {            
            Models.System system = db.Systems.Find(id);
            db.Systems.Remove(system);
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