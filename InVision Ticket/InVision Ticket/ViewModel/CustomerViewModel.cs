using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InVision_Ticket.Models
{
	public class CustomerViewModel
	{
		public Customer Customer { get; set; }
		public List<CustomerContact> CustomerContact {get;set;}
	}
}