using System;
using System.Collections.Generic;

namespace InVision_Ticket.Models
{
    public class BillRate
    {
        public BillRate()
        {
            this.BillRateTickets = new List<Ticket>();
        }

        public long BillRateID { get; set; }
        public decimal TicketBillRate { get; set; }
        public string BillRateDescription { get; set; }
        public virtual ICollection<Ticket> BillRateTickets { get; set; }
    }
}
