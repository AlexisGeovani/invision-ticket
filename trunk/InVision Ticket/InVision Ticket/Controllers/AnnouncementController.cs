using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InVision_Ticket.Models;
using MarkdownDeep;
using System.Data.Entity;

namespace InVision_Ticket.Controllers
{
    public class AnnouncementController : Controller
    {
        //
        // GET: /Announcement/

        public ActionResult Index()
        {
            using (InVisionTicketContext db = new InVisionTicketContext())
            {
                return View(db.Announcement.Include(a => a.Login).ToList());
            }
        }

        //
        // GET: /Announcement/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Announcement/Create

        public ActionResult Create()
        {

            return View();
        } 

        //
        // POST: /Announcement/Create

        [HttpPost, ValidateInput(false)]
        public ActionResult Create(Announcement announcement)
        {
            using (InVisionTicketContext db = new InVisionTicketContext())
            {
                announcement.CreatedDateTime = System.DateTime.Now;
                announcement.CreatedByLoginID = db.Logins.Single(l => l.Email == User.Identity.Name).LoginID;
                
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
        
        //
        // GET: /Announcement/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Announcement/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
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

        //
        // GET: /Announcement/Delete/5
 
        public ActionResult Delete(int id)
        {
            using (InVisionTicketContext db = new InVisionTicketContext())
            {
                Announcement a = db.Announcement.Find(id);
                return View(a);
            }
        }

        //
        // POST: /Announcement/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
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
    }
}
