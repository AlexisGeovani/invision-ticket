﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace InVision_Ticket.Controllers
{
	public class LogoutController : Controller
	{
		//
		// GET: /Logout/

		public ActionResult Index()
		{
			FormsAuthentication.SignOut();
			return RedirectToAction("Index", "Home");
		}

	}
}
