using System;
using System.Collections.Generic;

namespace InVision_Ticket.Models
{
    public class Ticket
    {
        public Ticket()
        {
            this.Parts = new List<Part>();
            this.Updates = new List<Update>();
        }

        public long TicketID { get; set; }
        public string Summary { get; set; }
        public string Details { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public Nullable<long> SalesmenLoginID { get; set; }
        public Nullable<long> TechnicianLoginID { get; set; }
        public Nullable<int> Priority { get; set; }
        public long CustomerID { get; set; }
        public long TicketTypeID { get; set; }
        public Nullable<DateTime> LastModifiedDateTime { get; set; }
        public long StatusID { get; set; }
        public Nullable<DateTime> ResolvedDateTime { get; set; }
        public Nullable<DateTime> LastModified { get; set; }
        public Nullable<long> CurrentlyEditByLoginID { get; set; }
        public long CreatedByLoginID { get; set; }
        public long CreatedByCustomerID { get; set; }
        public Nullable<long> LocationID { get; set; }
        public Nullable<long> SystemID { get; set; }
        public Nullable<long> BillRateID { get; set; }
        public virtual BillRate BillRate { get; set; }
        public virtual Customer CreatedByCustomer { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Location Location { get; set; }
        public virtual Login Login { get; set; }
        public virtual Login Technician { get; set; }
        public virtual Login CreatedByLogin { get; set; }
        public virtual Login CurrentlyEditByLogin { get; set; }
        public virtual ICollection<Part> Parts { get; set; }
        public virtual System System { get; set; }
        public virtual TicketStatus TicketStatus { get; set; }
        public virtual TicketType TicketType { get; set; }
        public virtual ICollection<Update> Updates { get; set; }
    }
}
