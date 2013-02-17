using System;
using System.Collections.Generic;

namespace InVision_Ticket.Models
{
    public class System
    {
        public System()
        {
            this.SystemTickets = new List<Ticket>();
        }

        public long SystemID { get; set; }
        public string Type { get; set; }
        public long CustomerID { get; set; }
        public string Desciption { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<Ticket> SystemTickets { get; set; }
    }
}
