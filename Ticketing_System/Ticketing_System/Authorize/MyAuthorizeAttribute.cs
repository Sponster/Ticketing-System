using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ticketing_System.Models;

namespace Ticketing_System.Authorize
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class MyAuthorizeAttribute : AuthorizeAttribute
    {
        ApplicationDbContext context = new ApplicationDbContext();
       
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var authorized = base.AuthorizeCore(httpContext);
            if (!authorized)
            {
                return false;
            }
             var rd = httpContext.Request.RequestContext.RouteData;

             var id = rd.Values["id"];
         
            var userName = httpContext.User.Identity.Name;
            
            UserManager<ApplicationUser> UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            Tickets ticket = context.Tickets.Find(id);
            ApplicationUser user = UserManager.FindByName(userName);

            rd.Values["model"] = ticket;


            return ticket.Sender == user.UserName;
        }

             
    }
}