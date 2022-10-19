using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookEvent.Models;
using Shared;
using BookEvent.Helper;

namespace BookEvent.Controllers
{
    public class EventsController : Controller
    {
        private readonly AppDbContext _context;

        public EventsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Events
        public async Task<IActionResult> Index()
        {

            IEnumerable<Events> events = _context.Events;
            List<Event> events1 = new List<Event>();

            foreach (Events i in events)
            {
                Event j = new EventsToEventModel().EventsToEventModelMapping(i);
                if ( i.EmailId == CurrUser.currUser )
                {
                    events1.Add(j);
                }
            }
            await _context.SaveChangesAsync();
            return View(events1);
        }

        [HttpGet]
        public async Task<IActionResult> PublicEvents()
        {

            IEnumerable<Events> events = _context.Events;
            List<Event> events1 = new List<Event>();
            List<Event> past = new List<Event>();
            List<Event> upcoming = new List<Event>();

            foreach (Events i in events)
            {
                Event j = new EventsToEventModel().EventsToEventModelMapping(i);
                if (i.Type==0)
                {

                    if (i.Date < DateTime.Today)
                    {
                        past.Add(j);
                    }
                    else
                    {
                        upcoming.Add(j);
                    }
                    events1.Add(j);
                }
            }
            await _context.SaveChangesAsync();
            return View(events1);
        }


        [HttpGet]
        public async Task<IActionResult> InvitedTo()
        {

            IEnumerable<Events> events = _context.Events;
            List<Event> invitedTo = new List<Event>();
        


            foreach (Events i in events)
            {
                Event j = new EventsToEventModel().EventsToEventModelMapping(i);

                if (i.InviteByEmail==CurrUser.currUser)
                {
                   
                    invitedTo.Add(j);
                }
            }
            await _context.SaveChangesAsync();
            return View(invitedTo);
        }

        //past Events


        public async Task<IActionResult> PastEvents()
        {

            IEnumerable<Events> events = _context.Events;
            List<Event> events1 = new List<Event>();
            List<Event> past = new List<Event>();
            List<Event> upcoming = new List<Event>();

            foreach (Events i in events)
            {
                Event j = new EventsToEventModel().EventsToEventModelMapping(i);
                if (i.Type == 0)
                {

                    if (i.Date < DateTime.Today)
                    {
                        past.Add(j);
                    }
                    else
                    {
                        upcoming.Add(j);
                    }
                    events1.Add(j);
                }
            }
            await _context.SaveChangesAsync();
            return View(past);
        }



        //upcoming events

        public async Task<IActionResult> upcomingEvents()
        {
            IEnumerable<Events> events = _context.Events;
            List<Event> events1 = new List<Event>();
            List<Event> past = new List<Event>();
            List<Event> upcoming = new List<Event>();

            foreach (Events i in events)
            {
                Event j = new EventsToEventModel().EventsToEventModelMapping(i);
                if (i.Type == 0)
                {

                    if (i.Date < DateTime.Today)
                    {
                        past.Add(j);
                    }
                    else
                    {
                        upcoming.Add(j);
                    }
                    events1.Add(j);
                }
            }
            await _context.SaveChangesAsync();
            return View(upcoming);
        }



        // GET: Events/Create
        public IActionResult Create()
        {
            if (Shared.CurrUser.currUser != null)
            {
                return View();
            }
            return RedirectToAction("Login", "LoginUsers");
        }


        // POST: Events/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Date,Location,StartTime,Type,Duration,Description,OtherDetails,InviteByEmail")] Event @event)
        {
            Events e1 = new EventModelToEvents().EventModelToEventsMapping(@event);

            if (ModelState.IsValid)
            {
                try
                {
                    var email = CurrUser.currUser;
                    e1.EmailId = email;
                    _context.Events.Add(e1);
                    await _context.SaveChangesAsync();
                }
                catch
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            return RedirectToAction("Index", "Home");
        }

       

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .FirstOrDefaultAsync(m => m.Title == id);

            if (@event == null)
            {
                return NotFound();
            }

            CurrUser.currEvent = id;

            IEnumerable<Comment> comments = _context.Comments;

            ViewData["Comments"] = comments;

            return View(@event);
        }


        // GET: Events/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }
            return View(@event);
        }

        // POST: Events/Edit/5
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Title,Date,Location,StartTime,Type,Duration,Description,OtherDetails,InviteByEmail")] Events @event)
        {

            Events e1 = @event;
            
            if (id != e1.Title )
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    e1.EmailId = Shared.CurrUser.currUser;
                    _context.Events.Update(e1);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {


                    if (!EventExists(e1.Title))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(e1);
        }

        // GET: Events/Delete/5
        public async Task<IActionResult> Delete(string id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .FirstOrDefaultAsync(m => m.Title == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }


        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {

            var @event = await _context.Events.FindAsync(id);
            _context.Events.RemoveRange(@event);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventExists(string id)
        {
            return _context.Events.Any(e => e.Title == id);
        }


        public async Task<IActionResult> CommentIndex()
        {
            return View(await _context.Comments.ToListAsync());
        }

        // GET: Comments/Create
        public IActionResult CommentsCreate()
        {
            return View();
        }

        // POST: Comments/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CommentsCreate([Bind("CommentAdded")] Comment comment)
        {
            comment.Date = DateTime.Now;
            comment.EventId = Shared.CurrUser.currEvent;
            comment.EmailId = Shared.CurrUser.currUser;

            if (ModelState.IsValid && comment.EventId != null && comment.EmailId != null)

            {
                _context.Comments.Add(comment);
                await _context.SaveChangesAsync();
                return RedirectToAction("CommentIndex");
            }
            return RedirectToAction("Login", "LoginUsers");

        }

    }
}
