using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace Ticketing_System.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string Email { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema:false)
        {
           
        }

        public System.Data.Entity.DbSet<Ticketing_System.Models.Projects> Projects { get; set; }

        public System.Data.Entity.DbSet<Ticketing_System.Models.Tickets> Tickets { get; set; }

        public System.Data.Entity.DbSet<Ticketing_System.Models.Messages> Messages { get; set; }
    }
}