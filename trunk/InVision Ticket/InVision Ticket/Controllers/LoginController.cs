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
    public class LoginController : Controller
    {
        private InVisionTicketContext db = new InVisionTicketContext();

        //
        // GET: /Login/

        public ViewResult Index()
        {
            var logins = db.Logins.Include(l => l.Location).Include(l => l.UserType);
            return View(logins.ToList());
        }

        //
        // GET: /Login/Details/5

        public ViewResult Details(long id)
        {
            Login login = db.Logins.Find(id);
            return View(login);
        }

        //
        // GET: /Login/Create

        public ActionResult Create()
        {

            ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "StoreLocation");
            ViewBag.UserTypeID = new SelectList(db.UserTypes, "UserTypeID", "UserType1");
            return View();
        } 

        //
        // POST: /Login/Create

        [HttpPost]
        public ActionResult Create(Login login)
        {
			if (login.Password == "")
			{
				ModelState.AddModelError("", "Password Required");
				return View();
			}
			if (ModelState.IsValid)
			{
				login.Password = PasswordHash.CreateHash(login.Password);
				db.Logins.Add(login);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

            ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "StoreLocation", login.LocationID);
            ViewBag.UserTypeID = new SelectList(db.UserTypes, "UserTypeID", "UserType1", login.UserTypeID);
            return View(login);
        }
        
        //
        // GET: /Login/Edit/5
 
        public ActionResult Edit(long id)
        {
            Login login = db.Logins.Find(id);
			login.Password = "";
			ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "StoreLocation", login.LocationID);
            ViewBag.UserTypeID = new SelectList(db.UserTypes, "UserTypeID", "UserType1", login.UserTypeID);
            return View(login);
        }

        //
        // POST: /Login/Edit/5

        [HttpPost]
        public ActionResult Edit(Login login)
        {

            if (ModelState.IsValid)
            {
				Login dbLogin = db.Logins.Find(login.LoginID);
				if (!string.IsNullOrWhiteSpace(login.Password))
				{
					dbLogin.Password = PasswordHash.CreateHash(login.Password);
				}
				
				dbLogin.Location = login.Location;
				dbLogin.Email = login.Email;
				dbLogin.DisplayName = login.DisplayName;
				dbLogin.UserType = login.UserType;
				dbLogin.Theme = login.Theme;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
			ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "StoreLocation", login.LocationID);
            ViewBag.UserTypeID = new SelectList(db.UserTypes, "UserTypeID", "UserType1", login.UserTypeID);
            return View(login);
        }

        //
        // GET: /Login/Delete/5
 
        public ActionResult Delete(long id)
        {
            Login login = db.Logins.Find(id);
            return View(login);
        }

        //
        // POST: /Login/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {            
            Login login = db.Logins.Find(id);
            db.Logins.Remove(login);
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