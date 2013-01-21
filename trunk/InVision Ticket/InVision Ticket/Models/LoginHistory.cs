using System;
using System.Collections.Generic;

namespace InVision_Ticket.Models
{
    public class LoginHistory
    {
        public long LoginHistoryID { get; set; }
        public long LoginID { get; set; }
        public Nullable<DateTime> DateTime { get; set; }
        public virtual Login Login { get; set; }
    }
}
