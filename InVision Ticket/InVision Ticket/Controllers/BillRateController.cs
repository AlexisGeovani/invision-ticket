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
    public class BillRateController : Controller
    {
        private InVisionTicketContext db = new InVisionTicketContext();

        //
        // GET: /BillRate/

        public ViewResult Index()
        {
            if (RoleCheck.IsAdministrator(User.Identity.Name))
            {
                return View(db.BillRates.ToList());
            }
            throw new HttpException(401, "Access Denied");
        }

        //
        // GET: /BillRate/Details/5

        public ViewResult Details(long id)
        {
            if (RoleCheck.IsAdministrator(User.Identity.Name))
            {
                BillRate billrate = db.BillRates.Find(id);
                return View(billrate);
            }
            throw new HttpException(401, "Access Denied");
        }

        //
        // GET: /BillRate/Create

        public ActionResult Create()
        {
            if (RoleCheck.IsAdministrator(User.Identity.Name))
            {
                return View();
            }
            throw new HttpException(401, "Access Denied");
        } 

        //
        // POST: /BillRate/Create

        [HttpPost]
        public ActionResult Create(BillRate billrate)
        {
            if (RoleCheck.IsAdministrator(User.Identity.Name))
            {
                if (ModelState.IsValid)
                {
                    db.BillRates.Add(billrate);
                    db.SaveChanges();
                    return RedirectToAction("Index");  
                }

                return View(billrate);
            }
            throw new HttpException(401, "Access Denied");
        }
        
        //
        // GET: /BillRate/Edit/5
 
        public ActionResult Edit(long id)
        {
            if (RoleCheck.IsAdministrator(User.Identity.Name))
            {
                BillRate billrate = db.BillRates.Find(id);
                return View(billrate);
            }
            throw new HttpException(401, "Access Denied");
        }

        //
        // POST: /BillRate/Edit/5

        [HttpPost]
        public ActionResult Edit(BillRate billrate)
        {
            if (RoleCheck.IsAdministrator(User.Identity.Name))
            {
                if (ModelState.IsValid)
                {
                    db.Entry(billrate).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(billrate);
            }
            throw new HttpException(401, "Access Denied");
        }

        //
        // GET: /BillRate/Delete/5
 
        public ActionResult Delete(long id)
        {
            if (RoleCheck.IsAdministrator(User.Identity.Name))
            {
                BillRate billrate = db.BillRates.Find(id);
                return View(billrate);
            }
            throw new HttpException(401, "Access Denied");
        }

        //
        // POST: /BillRate/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            if (RoleCheck.IsAdministrator(User.Identity.Name))
            {
                BillRate billrate = db.BillRates.Find(id);
                db.BillRates.Remove(billrate);
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