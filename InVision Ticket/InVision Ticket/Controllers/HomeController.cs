using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InVision_Ticket.ViewModels;
using InVision_Ticket.Models;
using InVision_Ticket.Utilities;

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
                var user = db.Logins.Where(l => l.Deleted == false).SingleOrDefault(l => l.Email == User.Identity.Name);

                HomeViewModel HVM = new HomeViewModel();
                HVM.Announcements = db.Announcement.OrderByDescending(l => l.CreatedDateTime).ToList();
                HVM.StagnantTickets = SearchUtility.TicketSearch("", Convert.ToInt32(user.LocationID.Value), null, null, true, null, null, DateTime.Now.AddDays(-7));
                return View(HVM);
            }
        }
        public ActionResult CloseWindow()
        {

            return View();
        }
    }
}
