using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InVision_Ticket.Utilities
{
    public class TimeAgo
    {
        public string TimeAgo(DateTime date)
        {
            TimeSpan span = System.DateTime.Now.Subtract(date);

            if (span.Hours >= 16)
            {
                return (span.Hours.ToString() + " Hours ago");

            }


        }
    }
}