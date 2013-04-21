using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InVision_Ticket.Models;

namespace InVision_Ticket.ViewModels
{
    public class HomeViewModel
    {
        public List<Announcement> Announcements { get; set; }
    }
}