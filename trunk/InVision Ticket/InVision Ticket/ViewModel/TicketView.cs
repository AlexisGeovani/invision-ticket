using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InVision_Ticket.ViewModel
{
	public class TicketView
	{
		public long TicketID { get; set; }
		public string Summary { get; set; }
		public string Details { get; set; }
		public DateTime CreatedDateTime { get; set; }
		public Nullable<long> SalesmenLoginID { get; set; }
		public Nullable<long> TechnicianLoginID { get; set; }
		public Nullable<int> Priority { get; set; }
		public long CustomerID { get; set; }
		public long TicketTypeID { get; set; }
		public Nullable<DateTime> LastModifiedDateTime { get; set; }
		public long StatusID { get; set; }
		public Nullable<DateTime> ResolvedDateTime { get; set; }
		public Nullable<DateTime> LastModified { get; set; }
		public Nullable<long> CurrentlyEditByLoginID { get; set; }
		public long CreatedByLoginID { get; set; }
		public long CreatedByCustomerID { get; set; }
		public Nullable<long> LocationID { get; set; }
		public Nullable<long> SystemID { get; set; }

	}
}