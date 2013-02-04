using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InVision_Ticket.ViewModel
{
	public class UpdateListView
	{
		public long UpdateID { get; set; }
		public long TicketID { get; set; }
		public string Comment { get; set; }
		public int BilledMinutes { get; set; }
		public Nullable<int> ActualMinutes { get; set; }
		public DateTime ActivityDateTime { get; set; }
		public Nullable<bool> Urgent { get; set; }
		public long LoginID { get; set; }
		public string Login {get; set;}
	}
}