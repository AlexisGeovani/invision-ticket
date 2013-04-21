using System;
using System.Collections.Generic;

namespace InVision_Ticket.Models
{
    public class BillRate
    {
        public BillRate()
        {
            this.BillRateUpdates = new List<Update>();
        }

        public long BillRateID { get; set; }
        public decimal TicketBillRate { get; set; }
        public string BillRateDescription { get; set; }
        public virtual ICollection<Update> BillRateUpdates { get; set; }
    }
}
