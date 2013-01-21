using System;
using System.Collections.Generic;

namespace InVision_Ticket.Models
{
    public class Update
    {
        public Update()
        {
            this.UrgentUpdateUsers = new List<UrgentUpdateUser>();
        }

        public long UpdateID { get; set; }
        public long TicketID { get; set; }
        public string Comment { get; set; }
        public Nullable<int> BilledMinutes { get; set; }
        public int ActualMinutes { get; set; }
        public DateTime ActivityDateTime { get; set; }
        public Nullable<bool> Urgent { get; set; }
        public long LoginID { get; set; }
        public virtual Login Login { get; set; }
        public virtual Ticket Ticket { get; set; }
        public virtual ICollection<UrgentUpdateUser> UrgentUpdateUsers { get; set; }
    }
}
