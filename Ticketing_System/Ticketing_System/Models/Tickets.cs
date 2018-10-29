using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ticketing_System.Models
{
    public class Tickets
    {

        public int Id { get; set; }
        private DateTime _date = DateTime.Now;
        public DateTime SendDate { get { return _date; } set { _date = value; } }
        private string SenderName = System.Web.HttpContext.Current.User.Identity.Name;
        public string Sender { get { return SenderName; } set { SenderName = value; } }
        public string Description { get; set; }
        public string Heading { get; set; }
     
        public string Type { get; set; }
        public string State { get; set; }

        [ForeignKey("ProjectId")]
        public virtual Projects Project { get; set; }
       
        public int ProjectId { get; set; }

      
        public ICollection<Messages> Messages { get; set; }
     
    }    
}