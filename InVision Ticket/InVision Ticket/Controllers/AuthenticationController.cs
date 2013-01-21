using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InVision_Ticket.Models;
using System.Web.Security;
using InVision_Ticket.Utilities;
namespace InVision_Ticket.Controllers
{
    public class AuthenticationController : Controller
    {
        //
        // GET: /Authenticaton/

        public ActionResult Index()
        {
            return View();
        }
		[HttpPost]
		public ActionResult Index(Login login)
		{
			

			if (ModelState.IsValid)
			{
				using (InVisionTicketContext data = new InVisionTicketContext())
				{
					if (data.Logins.Count(l => l.Email == login.Email) == 1)
					{
						if (PasswordHash.ValidatePassword(login.Password, data.Logins.SingleOrDefault(l => l.Email.ToLower() == login.Email.ToLower()).Password))
						{
							FormsAuthentication.SetAuthCookie(login.Email, false);
							return RedirectToAction("Index", "Home");
						}
					}
				}
			}
			ModelState.AddModelError("", "Invalid username or password.");
			return View();
		}
    }
}
