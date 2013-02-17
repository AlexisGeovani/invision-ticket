using System;
using System.Collections.Generic;

namespace InVision_Ticket.Models
{
    public class UserType
    {
        public UserType()
        {
            this.Logins = new List<Login>();
        }

        public long UserTypeID { get; set; }
        public string UserType1 { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Login> Logins { get; set; }
    }
}
