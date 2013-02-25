using System;
using System.Collections.Generic;

namespace InVision_Ticket.Models
{
    public class Update
    {
        public long UpdateID { get; set; }
        public long TicketID { get; set; }
        public string Comment { get; set; }
        public int BilledMinutes { get; set; }
        public Nullable<int> ActualMinutes { get; set; }
        public DateTime ActivityDateTime { get; set; }
        public Nullable<bool> Urgent { get; set; }
        public int LoginID { get; set; }
        public virtual Login Login { get; set; }
        public virtual Ticket Ticket { get; set; }
    }
}
