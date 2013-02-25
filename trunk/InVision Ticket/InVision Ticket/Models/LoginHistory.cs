using System;
using System.Collections.Generic;

namespace InVision_Ticket.Models
{
    public class LoginHistory
    {
        public long LoginHistoryID { get; set; }
        public int LoginID { get; set; }
        public DateTime DateTime { get; set; }
        public string IPAddress { get; set; }
        public string Browser { get; set; }
        public virtual Login Login { get; set; }
    }
}
