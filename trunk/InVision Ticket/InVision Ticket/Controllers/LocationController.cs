using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InVision_Ticket.Models;
using InVision_Ticket.Utilities;

namespace InVision_Ticket.Controllers
{ 
    public class LocationController : Controller
    {
        private InVisionTicketContext db = new InVisionTicketContext();

        //
        // GET: /Location/

        public ViewResult Index()
        {
            if (RoleCheck.IsAdministrator(User.Identity.Name))
            {
                return View(db.Locations.ToList()); 
            }
            throw new HttpException(401, "Access Denied");
        }

        //
        // GET: /Location/Details/5

        public ViewResult Details(long id)
        {
            if (RoleCheck.IsAdministrator(User.Identity.Name))
            {
                Location location = db.Locations.Find(id);
                return View(location);
            }
            throw new HttpException(401, "Access Denied");
        }

        //
        // GET: /Location/Create

        public ActionResult Create()
        {
            if (RoleCheck.IsAdministrator(User.Identity.Name))
            {
                return View();
            }
            throw new HttpException(401, "Access Denied");
        } 

        //
        // POST: /Location/Create

        [HttpPost]
        public ActionResult Create(Location location)
        {
            if (RoleCheck.IsAdministrator(User.Identity.Name))
            {
                if (ModelState.IsValid)
                {
                    db.Locations.Add(location);
                    db.SaveChanges();
                    return RedirectToAction("Index");  
                }

                return View(location);
            }
            throw new HttpException(401, "Access Denied");
        }
        
        //
        // GET: /Location/Edit/5
 
        public ActionResult Edit(long id)
        {
            if (RoleCheck.IsAdministrator(User.Identity.Name))
            {
                Location location = db.Locations.Find(id);
                return View(location);
            }
            throw new HttpException(401, "Access Denied");
        }

        //
        // POST: /Location/Edit/5

        [HttpPost]
        public ActionResult Edit(Location location)
        {
            if (RoleCheck.IsAdministrator(User.Identity.Name))
            {
                if (ModelState.IsValid)
                {
                    db.Entry(location).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(location);
            }
            throw new HttpException(401, "Access Denied");
        }

        //
        // GET: /Location/Delete/5
 
        public ActionResult Delete(long id)
        {
            if (RoleCheck.IsAdministrator(User.Identity.Name))
            {
                Location location = db.Locations.Find(id);
                return View(location);
            }
            throw new HttpException(401, "Access Denied");
        }

        //
        // POST: /Location/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            if (RoleCheck.IsAdministrator(User.Identity.Name))
            {
                Location location = db.Locations.Find(id);
                db.Locations.Remove(location);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            throw new HttpException(401, "Access Denied");
        }

        protected override void Dispose(bool disposing)
        {
                db.Dispose();
                base.Dispose(disposing);
        }
    }
}