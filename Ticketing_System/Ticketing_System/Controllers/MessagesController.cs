using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ticketing_System.Models;
using Ticketing_System.Authorize;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
namespace Ticketing_System.Controllers
{
    public class MessagesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Messages/
       [Authorize]
        public ActionResult Index()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            if (User.IsInRole("Client"))
            {
                string userName = User.Identity.Name.ToString();
                var messages = this.db.Messages.Where(e => e.Author == userName).Include(t => t.Ticket);
                return View(messages);
            }
            else
            {
                var messages = db.Messages.Include(t => t.Ticket);
                return View(messages.ToList());
            }
        }

        // GET: /Messages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Messages messages = db.Messages.Find(id);
            if (messages == null)
            {
                return HttpNotFound();
            }
            return View(messages);
        }

        [Authorize]
        // GET: /Messages/Create
        public ActionResult Create()
        {
           
         
            ViewBag.TicketId = new SelectList(db.Tickets, "Id", "Heading");
            
            return View();
        }

        // POST: /Messages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,SendDate,Author,State,Content,TicketId")] Messages messages)
        {
            Tickets ticket = new Tickets();
            ticket.Id = messages.TicketId;
            var Selected = db.Tickets.First(e => e.Id == ticket.Id);
            var State = Selected.State;
           

            if (ModelState.IsValid && State != "Closed")
            {
                db.Messages.Add(messages);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ErrorMessage = "The Selected Ticket is closed";
            ViewBag.TicketId = new SelectList(db.Tickets, "Id", "Heading", messages.TicketId);
            return View(messages);
        }
           [Authorize]
        // GET: /Messages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Messages messages = db.Messages.Find(id);
            if (messages == null)
            {
                return HttpNotFound();
            }
            ViewBag.TicketId = new SelectList(db.Tickets, "Id", "Sender", messages.TicketId);
            return View(messages);
        }

        // POST: /Messages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,SendDate,Author,State,Content,TicketId")] Messages messages)
        {
            if (ModelState.IsValid)
            {
                db.Entry(messages).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TicketId = new SelectList(db.Tickets, "Id", "Sender", messages.TicketId);
            return View(messages);
        }
          [Authorize]
        // GET: /Messages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Messages messages = db.Messages.Find(id);
            if (messages == null)
            {
                return HttpNotFound();
            }
            return View(messages);
        }

        // POST: /Messages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Messages messages = db.Messages.Find(id);
            db.Messages.Remove(messages);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
