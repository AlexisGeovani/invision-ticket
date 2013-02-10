using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InVision_Ticket.Models;

namespace InVision_Ticket.ViewModels
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
		public Nullable<DateTime> ResolvedDateTime { get; set; }
		public Nullable<long> CurrentlyEditByLoginID { get; set; }
		public Nullable<long> LastModifiedBy { get; set; }
		public long CreatedByLoginID { get; set; }
		public long CreatedByCustomerID { get; set; }
		public Nullable<long> LocationID { get; set; }
		public Nullable<long> SystemID { get; set; }
		//public long StatusID { get; set; }
		//public long BillRateID { get; set; }
		//public long TicketTypeID { get; set; }
		public string SalesmanName { get; set; }
		public string TechnicianName { get; set; }
		public string CustomerContactName { get; set; }
		public string CreatedByLogin { get; set; }
		public bool CreatedByCustomer { get; set; }
		public string BusinessName { get; set; }
		public bool BusinessCustomer { get; set; }
		public long Phone { get; set; }
		public virtual BillRate BillRate {get; set;}
		public virtual TicketStatus TicketStatus { get; set; }
		public virtual TicketType TicketType { get; set; }



		public List<Location> LocationList { get; set; }
		public List<BillRate> BillRateList { get; set; }
		public List<TicketType> TicketTypeList { get; set; }
		public List<TicketStatus> TicketStatusList { get; set; }

	}
}