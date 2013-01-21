using System;
using System.Collections.Generic;

namespace InVision_Ticket.Models
{
    public class Part
    {
        public string Name { get; set; }
        public long PartID { get; set; }
        public Nullable<decimal> Price { get; set; }
    }
}
