using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InVision_Ticket.ViewModels
{
	public class TicketListViewModel
	{
		public long TicketID { get; set; }
		public string Summary { get; set; }
		public string Details { get; set; }
		public DateTime CreatedDateTime { get; set; }
		public Nullable<int> Priority { get; set; }
		public Nullable<DateTime> LastModifiedDateTime { get; set; }
		public Nullable<DateTime> ResolvedDateTime { get; set; }
		public Nullable<DateTime> LastModified { get; set; }
		public string Status { get; set; }
		public long CustomerID { get; set; }
		public string CustomerPhone { get; set; }
		public string CustomerName { get; set; }
		public string TicketType { get; set; }
		public string CreatedBy { get; set; }
		public Nullable<int> SalesmenLoginID { get; set; }
		public string Salesman { get; set; }
		public Nullable<int> TechnicianLoginID { get; set; }
		public string Technician { get; set; }
		public Nullable<long> LocationID { get; set; }
		public Nullable<long> SystemID { get; set; }
		public decimal BillRate { get; set; }
        public string BillRateDescription { get; set; }
        public UpdateListView RecentUpdate { get; set; }
        public bool TicketStatusOpen { get; set; }
        public bool TicketStatusAttentionRequired { get; set; }
	}
}