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
        public bool Open { get; set; }
        public bool AttentionRequired { get; set; }
        public virtual ICollection<Ticket> StatusTickets { get; set; }
    }
}
