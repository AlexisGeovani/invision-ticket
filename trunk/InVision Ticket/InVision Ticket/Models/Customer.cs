using System;
using System.Collections.Generic;

namespace InVision_Ticket.Models
{
    public class Customer
    {
        public Customer()
        {
            this.Tickets = new List<Ticket>();
            this.CustomerContacts = new List<CustomerContact>();
            this.Tickets1 = new List<Ticket>();
        }

        public long CustomerID { get; set; }
        public Nullable<bool> BusinessCustomer { get; set; }
        public string CustomerName { get; set; }
        public string CustomerNotes { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
        public virtual ICollection<CustomerContact> CustomerContacts { get; set; }
        public virtual ICollection<Ticket> Tickets1 { get; set; }
    }
}
