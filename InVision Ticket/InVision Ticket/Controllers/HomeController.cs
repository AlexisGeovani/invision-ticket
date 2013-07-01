using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InVision_Ticket.ViewModels;
using InVision_Ticket.Models;

namespace InVision_Ticket.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            using(InVisionTicketContext db = new InVisionTicketContext())
            {
                HomeViewModel HVM = new HomeViewModel();
                HVM.Announcements = db.Announcement.OrderByDescending(l => l.CreatedDateTime).ToList();
                return View(HVM);
            }
        }
        public ActionResult CloseWindow()
        {

            return View();
        }
    }
}
