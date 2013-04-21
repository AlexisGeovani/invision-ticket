using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InVision_Ticket.Models;
using InVision_Ticket.Utilities;
using AutoMapper;
using InVision_Ticket.ViewModels;

namespace InVision_Ticket.Controllers
{ 
    public class LoginController : Controller
    {
        private InVisionTicketContext db = new InVisionTicketContext();

        //
        // GET: /Login/
        public ActionResult LoginTickets(int id)
        {
            return View("../Ticket/Index", SearchUtility.TicketSearch(null, null, null, null, null, null, id));
        }
        public ViewResult Index()
        {
            var logins = db.Logins.Include(l => l.Location).Include(l => l.UserType).ToList();
            List<LoginViewModel> LoginViews = Mapper.Map<List<Login>, List<LoginViewModel>>(logins);

            return View(LoginViews);
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

            LoginViewModel login = new LoginViewModel();
            login.LocationList = db.Locations.ToList();
            login.UserTypeList = db.UserTypes.ToList();
            return View(login);
        } 

        //
        // POST: /Login/Create

        [HttpPost]
        public ActionResult Create(LoginViewModel login)
        {
			if (ModelState.IsValid)
			{

                Login newLogin = Mapper.Map<LoginViewModel, Login>(login);
                newLogin.Password = PasswordHash.CreateHash(login.Email);

				db.Logins.Add(newLogin);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

            return View(login);
        }
        
        //
        // GET: /Login/Edit/5
 
        public ActionResult Edit(long id)
        {
            Login login = db.Logins.Find(id);
			
            
            LoginViewModel loginView = Mapper.Map<Login, LoginViewModel>(login);

            loginView.UserTypeList = db.UserTypes.ToList();
            loginView.LocationList = db.Locations.ToList();
            return View(loginView);
        }

        //
        // POST: /Login/Edit/5

        [HttpPost]
        public ActionResult Edit(LoginViewModel login)
        {

            if (ModelState.IsValid)
            {
				Login dbLogin = db.Logins.Find(login.LoginID);

                dbLogin.LocationID = login.LocationID;
				dbLogin.Email = login.Email;
				dbLogin.DisplayName = login.DisplayName;
				dbLogin.UserTypeID = login.UserTypeID;
				dbLogin.Theme = login.Theme;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(login);
        }

        public ActionResult ChangePassword(int id)
        {
            //Login login = db.Logins.Find(id);

            ChangePasswordViewModel vm = new ChangePasswordViewModel();
            vm.LoginID = id;

            return View(vm);
        
        }
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel vm)
        {
            db.Logins.Find(vm.LoginID).Password = PasswordHash.CreateHash(vm.Password);
            db.SaveChanges();
            return RedirectToAction("Index");
            
        }

        //
        // GET: /Login/Delete/5
 
        public ActionResult Delete(long id)
        {
            Login login = db.Logins.Find(id);
            LoginViewModel loginView = Mapper.Map<Login, LoginViewModel>(login);
            

            return View(loginView);
        }

        //
        // POST: /Login/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {            
            db.Logins.Remove(db.Logins.Find(id));
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