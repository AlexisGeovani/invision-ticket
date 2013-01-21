using System;
using System.Collections.Generic;

namespace InVision_Ticket.Models
{
    public class Ticket
    {
        public Ticket()
        {
            this.Updates = new List<Update>();
        }

        public long TicketID { get; set; }
        public string Summary { get; set; }
        public string Details { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public Nullable<long> SalesmenID { get; set; }
        public Nullable<long> TechnicianID { get; set; }
        public Nullable<int> Priority { get; set; }
        public Nullable<long> CustomerID { get; set; }
        public string TicketType { get; set; }
        public DateTime LastModifiedDateTime { get; set; }
        public string Status { get; set; }
        public string ResolvedDateTime { get; set; }
        public Nullable<DateTime> LastModified { get; set; }
        public long CurrentlyEditByLogin { get; set; }
        public long CreatedBy { get; set; }
        public long LocationID { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Customer Customer1 { get; set; }
        public virtual Location Location { get; set; }
        public virtual Login Login { get; set; }
        public virtual Login Login1 { get; set; }
        public virtual Login Login2 { get; set; }
        public virtual ICollection<Update> Updates { get; set; }
    }
}
