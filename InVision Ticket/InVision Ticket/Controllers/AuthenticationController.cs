﻿using System;
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
                    
					if (data.Logins.Count(l => l.Email == login.Email) >= 1)
					{
                        var Login = data.Logins.Where(l => l.Deleted == false).SingleOrDefault(l => l.Email.ToLower() == login.Email.ToLower());
                        if(string.IsNullOrWhiteSpace(Login.Password))
                        {
                            ModelState.AddModelError("", "Invalid username or password.");
			                return View();
                        }


						if (PasswordHash.ValidatePassword(login.Password, Login.Password))
						{

                            FormsAuthentication.SetAuthCookie(Login.Email, true);
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
//FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
//2,
//Login.Email,
//DateTime.Now,
//DateTime.Now.AddHours(8),
//false,
//Login.LocationID + ":" + Login.UserTypeID);
//string encryptedTicket = FormsAuthentication.Encrypt(ticket);
//var x = FormsAuthentication.Timeout;
//HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
//this.Response.Cookies.Add(cookie);
//FormsAuthentication.SetAuthCookie(login.Email, false);