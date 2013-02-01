using System;
using System.Collections.Generic;

namespace InVision_Ticket.Models
{
    public class TicketStatu
    {
        public TicketStatu()
        {
            this.StatusTickets = new List<Ticket>();
        }

        public long TicketStatusID { get; set; }
        public string TicketStatus { get; set; }
        public virtual ICollection<Ticket> StatusTickets { get; set; }
    }
}
