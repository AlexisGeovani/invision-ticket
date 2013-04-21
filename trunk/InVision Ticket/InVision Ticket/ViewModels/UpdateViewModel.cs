using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using InVision_Ticket.Models;

namespace InVision_Ticket.ViewModels
{
    public class UpdateViewModel
    {

        public long UpdateID { get; set; }
        [Required]
        public long TicketID { get; set; }
        
        public string Comment { get; set; }
        [Required]
        public string CommentMarkDown { get; set; }
        public int? BilledMinutes { get; set; }
        public Nullable<int> ActualMinutes { get; set; }
        public DateTime ActivityDateTime { get; set; }
        public Nullable<bool> Urgent { get; set; }
        public int LoginID { get; set; }
        public long? BillRateID { get; set; }
        public decimal? OtherCharges { get; set; }
        public string OtherChargesDescription { get; set; }
        public virtual Login Login { get; set; }
        public virtual Ticket Ticket { get; set; }

        public virtual BillRate BillRate { get; set; }

        public List<BillRate> BillRateList { get; set; }

    }
}
