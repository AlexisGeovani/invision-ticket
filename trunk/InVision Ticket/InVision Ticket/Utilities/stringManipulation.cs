using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace InVision_Ticket.Utilities
{
    public class stringManipulation
    {
        //public static string CleanPhone(string phone)
        //{
        //    Regex digitsOnly = new Regex(@"[^\d]");
        //    return digitsOnly.Replace(phone, "");
        //}
        public static int CleanPhone(string phone)
        {
            Regex digitsOnly = new Regex(@"[^\d]");
            return int.Parse(digitsOnly.Replace(phone, ""));
        }
    }
}