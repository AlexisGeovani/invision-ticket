using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using InVision_Ticket.Models;
using System.Web.Mvc;

namespace InVision_Ticket.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string Email { get; set; }

        //[StringLength(1000, MinimumLength = 6, ErrorMessage = "Password must be atleast 6 characters")]
        //public string Password { get; set; }


        [Required]
        public string DisplayName { get; set; }
        [Required]
        public long UserTypeID { get; set; }
        [Required]
        public Nullable<long> LocationID { get; set; }
        [Key]
        public int LoginID { get; set; }
        public string Theme { get; set; }
        public string StoreLocation { get; set; }
        public string UserType { get; set; }

        public List<Location> LocationList { get; set; }
        public List<UserType> UserTypeList { get; set; }
    }
}