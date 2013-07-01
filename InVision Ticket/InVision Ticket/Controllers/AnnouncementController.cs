using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InVision_Ticket.Models;
using MarkdownDeep;
using System.Data.Entity;
using InVision_Ticket.Utilities;

namespace InVision_Ticket.Controllers
{
    public class AnnouncementController : Controller
    {
        //
        // GET: /Announcement/

        public ActionResult Index()
        {
            if (RoleCheck.IsAdministrator(User.Identity.Name))
            {
                using (InVisionTicketContext db = new InVisionTicketContext())
                {
                    return View(db.Announcement.Include(a => a.Login).OrderByDescending(l => l.CreatedDateTime).ToList());
                }
            }
            throw new HttpException(401, "Access Denied");
        }

        //
        // GET: /Announcement/Details/5

        public ActionResult Details(int id)
        {
            if (RoleCheck.IsAdministrator(User.Identity.Name))
            {
                return View();
            }
            throw new HttpException(401, "Access Denied");
        }

        //
        // GET: /Announcement/Create

        public ActionResult Create()
        {
            if (RoleCheck.IsAdministrator(User.Identity.Name))
            {
                return View();
            }
            throw new HttpException(401, "Access Denied");
        } 

        //
        // POST: /Announcement/Create

        [HttpPost, ValidateInput(false)]
        public ActionResult Create(Announcement announcement)
        {
            if (RoleCheck.IsAdministrator(User.Identity.Name))
            {
                using (InVisionTicketContext db = new InVisionTicketContext())
                {
                    announcement.CreatedDateTime = System.DateTime.Now;
                    announcement.CreatedByLoginID = db.Logins.Where(l => l.Deleted == false).SingleOrDefault(l => l.Email == User.Identity.Name).LoginID;
                
                    //try
                    //{

                        if (announcement.DirectHTML)
                        {
                            announcement.Markup = "";
                        }
                        else
                        { 
                            Markdown md = new Markdown();
                            announcement.HTML = md.Transform(announcement.Markup);
                        }
                        db.Announcement.Add(announcement);
                        db.SaveChanges();


                        return RedirectToAction("Index");
                    //}
                    //catch
                    //{
                    //    return View();
                    //}
                }
            }
            throw new HttpException(401, "Access Denied");
        }
        
        //
        // GET: /Announcement/Edit/5
 
        public ActionResult Edit(int id)
        {
            if (RoleCheck.IsAdministrator(User.Identity.Name))
            {
                return View();
            }
            throw new HttpException(401, "Access Denied");
        }

        //
        // POST: /Announcement/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            if (RoleCheck.IsAdministrator(User.Identity.Name))
            {
                try
                {
                    // TODO: Add update logic here
 
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
            throw new HttpException(401, "Access Denied");
        }

        //
        // GET: /Announcement/Delete/5
 
        public ActionResult Delete(int id)
        {
            if (RoleCheck.IsAdministrator(User.Identity.Name))
            {
                using (InVisionTicketContext db = new InVisionTicketContext())
                {
                    Announcement a = db.Announcement.Find(id);
                    return View(a);
                }
            }
            throw new HttpException(401, "Access Denied");
        }

        //
        // POST: /Announcement/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            if (RoleCheck.IsAdministrator(User.Identity.Name))
            {
                try
                {
                    using (InVisionTicketContext db = new InVisionTicketContext())
                    {
                        db.Announcement.Remove(db.Announcement.Find(id));
                        db.SaveChanges();
                    }
 
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
            throw new HttpException(401, "Access Denied");
        }
    }
}
