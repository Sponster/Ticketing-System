using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ticketing_System.Models
{
    public class Messages
    {
        public int Id { get; set; }
        private DateTime _date = DateTime.Now;
        public DateTime SendDate { get { return _date; } set { _date = value; } }

        private string AuthorName = System.Web.HttpContext.Current.User.Identity.Name;
        public string Author { get { return AuthorName; } set { AuthorName = value; } }
        public string State { get; set; }
        public string Content { get; set; }

        [ForeignKey("TicketId")]
        public Tickets Ticket { get; set; }
        public int TicketId { get; set; }

    }
}