using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InVision_Ticket.Models;

namespace InVision_Ticket.Utilities
{
    public class RoleCheck
    {
        public static bool IsAdministrator(string Email)
        {
            using (InVisionTicketContext db = new InVisionTicketContext())
            {
                if (db.Logins.Single(l => l.Email == Email).UserTypeID == 100)
                    return true;
                return false;
            }
        }
        public static bool IsAdministrator(int id)
        {
            using (InVisionTicketContext db = new InVisionTicketContext())
            {
                if (db.Logins.Find(id).UserTypeID == 100)
                    return true;
                return false;
            }
        }
    }
}