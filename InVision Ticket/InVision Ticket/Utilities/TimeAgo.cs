using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InVision_Ticket.Utilities
{
    public class TimeAgo
    {
        public static string getStringTime(DateTime date)
        {
            TimeSpan span = System.DateTime.Now.Subtract(date);

            if (span.Hours < 17)
            {
                if (span.Hours < 2)
                {
                    return (span.Minutes.ToString() + " Minutes Ago");
                    
                }
                return (span.Hours.ToString() + " Hours ago");

            }
            if (span.Hours > 16)
            {
                if (span.Hours > 36)
                {
                    return (span.TotalDays.ToString());
                }
                return (span.Hours.ToString() + " Hours ago");

            }
            return "";
        }
    }
}