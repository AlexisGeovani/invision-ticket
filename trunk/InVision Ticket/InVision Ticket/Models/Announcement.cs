using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InVision_Ticket.Models
{
    public class Announcement
    {
        public int AnnouncementID { get; set; }
        public string HTML { get; set; }
        public string Markup { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int CreatedByLoginID { get; set; }
        public bool DirectHTML { get; set; }
        public string CSS { get; set; }
        public virtual Login Login { get; set; }
    }
}