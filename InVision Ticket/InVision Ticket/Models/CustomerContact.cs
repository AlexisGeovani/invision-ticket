using System;
using System.Collections.Generic;

namespace InVision_Ticket.Models
{
    public class CustomerContact
    {
        public long CustomerContactID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Phone { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public Nullable<int> Zip { get; set; }
        public Nullable<long> CustomerID { get; set; }
        public string Email { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
