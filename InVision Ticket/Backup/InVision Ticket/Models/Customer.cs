using System;
using System.Collections.Generic;

namespace InVision_Ticket.Models
{
    public class Customer
    {
        public Customer()
        {
            this.CustomerContacts = new List<CustomerContact>();
            this.Systems = new List<System>();
            this.CreatedByCustomerTickets = new List<Ticket>();
            this.CustomerTickets = new List<Ticket>();
        }

        public long CustomerID { get; set; }
        public Nullable<bool> BusinessCustomer { get; set; }
        public string CustomerName { get; set; }
        public string CustomerNotes { get; set; }
        public virtual ICollection<CustomerContact> CustomerContacts { get; set; }
        public virtual ICollection<System> Systems { get; set; }
        public virtual ICollection<Ticket> CreatedByCustomerTickets { get; set; }
        public virtual ICollection<Ticket> CustomerTickets { get; set; }
    }
}
