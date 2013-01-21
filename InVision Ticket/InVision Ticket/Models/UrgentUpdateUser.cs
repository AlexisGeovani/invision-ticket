using System;
using System.Collections.Generic;

namespace InVision_Ticket.Models
{
    public class UrgentUpdateUser
    {
        public long UrgentUpdateUserID { get; set; }
        public long LoginID { get; set; }
        public long UpdateID { get; set; }
        public virtual Login Login { get; set; }
        public virtual Update Update { get; set; }
    }
}
