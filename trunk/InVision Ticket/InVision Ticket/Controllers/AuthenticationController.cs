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
			
			if(true)//if (ModelState.IsValid)
			{
				using (InVisionTicketContext data = new InVisionTicketContext())
				{
                    
					if (data.Logins.Count(l => l.Email == login.Email) == 1)
					{
                        var Login = data.Logins.SingleOrDefault(l => l.Email.ToLower() == login.Email.ToLower());
						if (PasswordHash.ValidatePassword(login.Password, Login.Password))
						{
                            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                                2,
                                Login.Email,
                                DateTime.Now,
                                DateTime.Now.AddHours(8),
                                false,
                                Login.LocationID + ":" + Login.UserTypeID);
                            string encryptedTicket = FormsAuthentication.Encrypt(ticket);
                            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
							this.Response.Cookies.Add(cookie);
                            //FormsAuthentication.SetAuthCookie(login.Email, false);
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
