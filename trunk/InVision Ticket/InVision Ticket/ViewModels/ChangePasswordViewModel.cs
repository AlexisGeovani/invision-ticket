using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace InVision_Ticket.ViewModels
{
    public class ChangePasswordViewModel
    {
        
        public int LoginID { get; set; }
        [StringLength(1000, MinimumLength = 6, ErrorMessage = "field must be atleast 6 characters")]
        public string Password { get; set; }
    }
}