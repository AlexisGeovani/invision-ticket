using System;
using System.Collections.Generic;

namespace InVision_Ticket.Models
{
    public class TicketStatus
    {
        public TicketStatus()
        {
            this.StatusTickets = new List<Ticket>();
        }

        public long TicketStatusID { get; set; }
        public string Status { get; set; }
        public virtual ICollection<Ticket> StatusTickets { get; set; }
    }
}
