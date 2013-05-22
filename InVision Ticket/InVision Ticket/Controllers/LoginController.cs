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
using System.Data.Entity.Validation;

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
            ViewBag.LoginID = db.Logins.Single(l => l.Email == User.Identity.Name).LoginID;
            var logins = db.Logins.Include(l => l.Location).Include(l => l.UserType).Where(l => l.Deleted != true).ToList();
            List<LoginViewModel> LoginViews = Mapper.Map<List<Login>, List<LoginViewModel>>(logins);

            return View(LoginViews);
        }

        //
        // GET: /Login/Details/5

        public ViewResult Details(long id)
        {
            if (RoleCheck.IsAdministrator(User.Identity.Name))
            {

                Login login = db.Logins.Find(id);
                return View(login);
            } 

            throw new HttpException(401, "Access Denied");
        }

        //
        // GET: /Login/Create

        public ActionResult Create()
        {
            if (RoleCheck.IsAdministrator(User.Identity.Name))
            {
            
                LoginViewModel login = new LoginViewModel();
                login.LocationList = db.Locations.ToList();
                login.UserTypeList = db.UserTypes.ToList();
                return View(login);
            }
            throw new HttpException(401, "Access Denied");
        } 

        //
        // POST: /Login/Create

        [HttpPost]
        public ActionResult Create(LoginViewModel login)
        {
            if (RoleCheck.IsAdministrator(User.Identity.Name))
            {
            
			    if (ModelState.IsValid)
			    {
                    try
                    {
                        Login newLogin = Mapper.Map<LoginViewModel, Login>(login);
                        newLogin.Password = PasswordHash.CreateHash(login.Email);
                        newLogin.Location = null;
                        newLogin.UserType = null;
                        newLogin.Deleted = false;
                        db.Logins.Add(newLogin);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    catch (DbEntityValidationException e)
                    {
                        foreach (var eve in e.EntityValidationErrors)
                        {
                            Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                                eve.Entry.Entity.GetType().Name, eve.Entry.State);
                            foreach (var ve in eve.ValidationErrors)
                            {
                                Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                    ve.PropertyName, ve.ErrorMessage);
                            }
                        }
                        throw;
                    }
			    }


                return View(login);
            }
            throw new HttpException(401, "Access Denied");
        }
        
        //
        // GET: /Login/Edit/5
 
        public ActionResult Edit(long id)
        {
            if (RoleCheck.IsAdministrator(User.Identity.Name))
            {
           
                Login login = db.Logins.Find(id);
			
            
                LoginViewModel loginView = Mapper.Map<Login, LoginViewModel>(login);

                loginView.UserTypeList = db.UserTypes.ToList();
                loginView.LocationList = db.Locations.ToList();
                return View(loginView);
            }
            throw new HttpException(401, "Access Denied");

        }

        //
        // POST: /Login/Edit/5

        [HttpPost]
        public ActionResult Edit(LoginViewModel login)
        {
            if (RoleCheck.IsAdministrator(User.Identity.Name))
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
            throw new HttpException(401, "Access Denied");
        }

        public ActionResult ChangePassword(int id)
        {
            //Login login = db.Logins.Find(id);
            if (RoleCheck.IsAdministrator(User.Identity.Name) || (db.Logins.Single(l => l.Email == User.Identity.Name).LoginID == id))
            {
                ChangePasswordViewModel vm = new ChangePasswordViewModel();
                vm.LoginID = id;
                return View(vm);
            }
            throw new HttpException(401, "Access Denied");
        
        }
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel vm)
        {
            if (RoleCheck.IsAdministrator(User.Identity.Name) || (db.Logins.Single(l => l.Email == User.Identity.Name).LoginID == vm.LoginID))
            {
                db.Logins.Find(vm.LoginID).Password = PasswordHash.CreateHash(vm.Password);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            throw new HttpException(401, "Access Denied");
            
        }

        //
        // GET: /Login/Delete/5
 
        public ActionResult Delete(long id)
        {
            if (RoleCheck.IsAdministrator(User.Identity.Name))
            {
                Login login = db.Logins.Find(id);
                LoginViewModel loginView = Mapper.Map<Login, LoginViewModel>(login);
                return View(loginView);
            }
            throw new HttpException(401, "Access Denied");
        }

        //
        // POST: /Login/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            if (RoleCheck.IsAdministrator(User.Identity.Name))
            {
                db.Logins.Find(id).Deleted = true;
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