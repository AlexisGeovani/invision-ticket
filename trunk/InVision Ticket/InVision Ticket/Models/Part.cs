using System;
using System.Collections.Generic;

namespace InVision_Ticket.Models
{
    public class Part
    {
        public string Summary { get; set; }
        public long PartID { get; set; }
        public Nullable<decimal> Cost { get; set; }
        public decimal Charge { get; set; }
        public long TicketID { get; set; }
        public virtual Ticket Ticket { get; set; }
    }
}
