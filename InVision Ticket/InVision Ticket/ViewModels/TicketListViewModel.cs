using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace InVision_Ticket.ViewModels
{
	public class TicketListViewModel
	{
		public long TicketID { get; set; }
		public string Summary { get; set; }
        //[DisplayFormat(DataFormatString = "{0:MM/dd hh:mm tt}")]
        public DateTime CreatedDateTime { get; set; }
        //[DisplayFormat(DataFormatString = "{0:MM/dd hh:mm tt}")]
        public Nullable<DateTime> LastModifiedDateTime { get; set; }
		public Nullable<int> Priority { get; set; }

        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd hh:mm tt}")]
        //public Nullable<DateTime> ResolvedDateTime { get; set; }
		public string TicketStatus { get; set; }
		public long CustomerID { get; set; }
		public string CustomerPhone { get; set; }
		public string CustomerName { get; set; }
		public string TicketType { get; set; }
		public string CreatedBy { get; set; }
		public Nullable<int> SalesmanLoginID { get; set; }
		public string Salesman { get; set; }
		public Nullable<int> TechnicianLoginID { get; set; }
		public string Technician { get; set; }
		public Nullable<long> LocationID { get; set; }
        public string Location { get; set; }
		public Nullable<long> SystemID { get; set; }
        public UpdateListView RecentUpdate { get; set; }
        public bool TicketStatusOpen { get; set; }
        public bool TicketStatusAttentionRequired { get; set; }
	}
}