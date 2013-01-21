using System;
using System.Collections.Generic;

namespace InVision_Ticket.Models
{
    public class Location
    {
        public Location()
        {
            this.Logins = new List<Login>();
            this.Tickets = new List<Ticket>();
        }

        public long LocationID { get; set; }
        public string Location1 { get; set; }
        public virtual ICollection<Login> Logins { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
