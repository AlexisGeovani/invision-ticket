using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace InVision_Ticket.Models
{
    public class Upload
    {
        public long UploadID { get; set; }
 
        public string Description { get; set; }
        [Required]
        public byte[] Data { get; set; }
        [Required]
        public long TicketID { get; set; }
        [Required]
        public string FileName { get; set; }
        public virtual Ticket Ticket { get; set; }
    }
}