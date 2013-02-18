using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InVision_Ticket.Utilities
{
    public class TimeAgo
    {
        public static string getStringTime(DateTime dt)
        {
            TimeSpan span = System.DateTime.Now.Subtract(dt);

            if (span.TotalHours < 17)
            {
                if (span.TotalHours < 2)
                {
                    return ((int)span.TotalMinutes + " Minutes Ago");
                    
                }
                return ((int)span.TotalHours + " Hours ago");

            }
            if (span.TotalHours > 16)
            {
                if (span.TotalHours > 36)
                {
                    return ((int)span.TotalDays + " Days Ago");
                }
                return ((int)span.TotalHours + " Hours ago");

            }
            return "";
        }
    }
}