using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace InVision_Ticket.Models
{
	public class CustomerViewModel
	{
		public long? CustomerID { get; set; }
		public bool BusinessCustomer { get; set; }
		public string CustomerNotes { get; set; }
		public long? CustomerContactID { get; set; }
		[Required(ErrorMessage = "Name Required")]
		public string FirstName { get; set; }
		[Required(ErrorMessage = "Name Required")]
		public string LastName { get; set; }
		[Required(ErrorMessage="Phone # Required")]
		[RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered phone format is not valid.")]
		public string PhoneString { get; set; }
		public string Address1 { get; set; }
		public string Address2 { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public int? Zip { get; set; }
		[Required]
		[RegularExpression(@"^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,4})$", ErrorMessage="Email address format is not valid.")]
		public string Email { get; set; }
		public bool PromotionalEmails { get; set; }
		public string CustomerName { get; set; }

	
	}
}