using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InVision_Ticket.Models
{
    public class Login
    {
        public Login()
        {
            this.Tickets = new List<Ticket>();
            this.LoginHistories = new List<LoginHistory>();
            this.Tickets1 = new List<Ticket>();
            this.Tickets2 = new List<Ticket>();
            this.Updates = new List<Update>();
            this.UrgentUpdateUsers = new List<UrgentUpdateUser>();
        }
		public long LoginID { get; set; }
		[Required]

        public string Email { get; set; }
		[Required]
        public string Password { get; set; }
		
        public string DisplayName { get; set; }
        public long UserTypeID { get; set; }
        public Nullable<long> LocationID { get; set; }
        
        public virtual Location Location { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
        public virtual UserType UserType { get; set; }
        public virtual ICollection<LoginHistory> LoginHistories { get; set; }
        public virtual ICollection<Ticket> Tickets1 { get; set; }
        public virtual ICollection<Ticket> Tickets2 { get; set; }
        public virtual ICollection<Update> Updates { get; set; }
        public virtual ICollection<UrgentUpdateUser> UrgentUpdateUsers { get; set; }
    }
}
