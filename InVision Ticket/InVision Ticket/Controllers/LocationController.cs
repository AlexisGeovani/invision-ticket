﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InVision_Ticket.Models;

namespace InVision_Ticket.Controllers
{ 
    public class LocationController : Controller
    {
        private InVisionTicketContext db = new InVisionTicketContext();

        //
        // GET: /Location/

        public ViewResult Index()
        {
            return View(db.Locations.ToList());
        }

        //
        // GET: /Location/Details/5

        public ViewResult Details(long id)
        {
            Location location = db.Locations.Find(id);
            return View(location);
        }

        //
        // GET: /Location/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Location/Create

        [HttpPost]
        public ActionResult Create(Location location)
        {
            if (ModelState.IsValid)
            {
                db.Locations.Add(location);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(location);
        }
        
        //
        // GET: /Location/Edit/5
 
        public ActionResult Edit(long id)
        {
            Location location = db.Locations.Find(id);
            return View(location);
        }

        //
        // POST: /Location/Edit/5

        [HttpPost]
        public ActionResult Edit(Location location)
        {
            if (ModelState.IsValid)
            {
                db.Entry(location).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(location);
        }

        //
        // GET: /Location/Delete/5
 
        public ActionResult Delete(long id)
        {
            Location location = db.Locations.Find(id);
            return View(location);
        }

        //
        // POST: /Location/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {            
            Location location = db.Locations.Find(id);
            db.Locations.Remove(location);
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