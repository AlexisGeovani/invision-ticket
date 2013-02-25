using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InVision_Ticket.Models
{
    public class Login
    {
        public Login()
        {
            this.LoginHistories = new List<LoginHistory>();
            this.SalesmanTickets = new List<Ticket>();
            this.TechnicianTickets = new List<Ticket>();
            this.CreatedTickets = new List<Ticket>();
            this.EditingTickets = new List<Ticket>();
            this.Updates = new List<Update>();
        }
		[Required]
        public string Email { get; set; }

		[StringLength(1000, MinimumLength = 6, ErrorMessage = "field must be atleast 6 characters")]
        public string Password { get; set; }


		[Required]
        public string DisplayName { get; set; }
        public long UserTypeID { get; set; }
        public Nullable<long> LocationID { get; set; }
        public int LoginID { get; set; }
        public string Theme { get; set; }
        public virtual Location Location { get; set; }
		//[Required]
        public virtual UserType UserType { get; set; }
        public virtual ICollection<LoginHistory> LoginHistories { get; set; }
        public virtual ICollection<Ticket> SalesmanTickets { get; set; }
        public virtual ICollection<Ticket> TechnicianTickets { get; set; }
        public virtual ICollection<Ticket> CreatedTickets { get; set; }
        public virtual ICollection<Ticket> EditingTickets { get; set; }
        public virtual ICollection<Update> Updates { get; set; }
    }
}
