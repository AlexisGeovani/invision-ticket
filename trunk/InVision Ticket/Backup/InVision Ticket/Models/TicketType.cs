using System;
using System.Collections.Generic;

namespace InVision_Ticket.Models
{
    public class TicketType
    {
        public TicketType()
        {
            this.TypeTickets = new List<Ticket>();
        }

        public long TicketTypeID { get; set; }
        public string TicketType1 { get; set; }
        public virtual ICollection<Ticket> TypeTickets { get; set; }
    }
}
