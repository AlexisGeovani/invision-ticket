using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InVision_Ticket.ViewModels
{
	public class UpdateListView
	{
		public long UpdateID { get; set; }
		public long TicketID { get; set; }
		public string Comment { get; set; }
		public int BilledMinutes { get; set; }
		public Nullable<int> ActualMinutes { get; set; }
		public DateTime ActivityDateTime { get; set; }
        public long? BillRateID { get; set; }
        public decimal? OtherCharges { get; set; }
        public string OtherChargesDescription { get; set; }

		public Nullable<bool> Urgent { get; set; }
		public int LoginID { get; set; }
		public string Login {get; set; }
	}
}

