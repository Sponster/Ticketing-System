using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ticketing_System.Authorize;
using Ticketing_System.Models;

namespace Ticketing_System.Controllers
{
    public class TicketsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {   var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            if(User.IsInRole("Client"))
            {
            string userName = User.Identity.Name.ToString();
            var tickets = this.db.Tickets.Where(e => e.Sender == userName).Include(t => t.Project);
            return View(tickets);
            } else {
            var tickets = db.Tickets.Include(t => t.Project);
            return View(tickets.ToList());
            }

        }
        
        
      

        // GET: /Tickets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tickets tickets = db.Tickets.Find(id);
            Messages messages = new Messages();
            var AllMessages = db.Messages.ToArray();
            var TicketMessages = AllMessages.Where(e => e.TicketId == id);
            
            if (tickets == null)
            {
                return HttpNotFound();
            }
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            if (User.IsInRole("Client"))
            {
                string userName = User.Identity.Name.ToString();
                var UserMessages = this.db.Messages.Where(e => e.Author == userName && e.TicketId== id);
                return View(UserMessages);
            }
            else
            {
                return View(TicketMessages);
            }
            }

        [Authorize]
        // GET: /Tickets/Create
        public ActionResult Create()
        {
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name");
            return View();
        }

        // POST: /Tickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,SendDate,Sender,Description,Heading,Type,State,ProjectId")] Tickets tickets)
        {
            if (ModelState.IsValid)
            {
                db.Tickets.Add(tickets);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", tickets.ProjectId);
            return View(tickets);
        }
        [Authorize]
        // GET: /Tickets/Edit/5
        
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tickets tickets = db.Tickets.Find(id);
            if (tickets == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", tickets.ProjectId);
            return View(tickets);
        }

        // POST: /Tickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,SendDate,Sender,Description,Heading,Type,State,ProjectId")] Tickets tickets)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tickets).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", tickets.ProjectId);
            return View(tickets);
        }
        [Authorize]
        // GET: /Tickets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tickets tickets = db.Tickets.Find(id);
            if (tickets == null)
            {
                return HttpNotFound();
            }
            return View(tickets);
        }
        [Authorize]
        // POST: /Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tickets tickets = db.Tickets.Find(id);
            db.Tickets.Remove(tickets);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

      
    }
}
