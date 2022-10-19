using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookEvent.Models;

namespace BookEvent.Controllers
{
    public class CommentsController : Controller
    {
        private readonly AppDbContext _context;

        public CommentsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Comments
        public async Task<IActionResult> Index()
        {
            return View(await _context.Comments.ToListAsync());
        }

        // GET: Comments/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments
                .FirstOrDefaultAsync(m => m.CommentAdded == id);
            if (comment == null)
            {
                return NotFound();

            }

            return View(comment);
        }

        // GET: Comments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Comments/Create
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CommentAdded")] Comment comment)
        {
            comment.Date = DateTime.Now;
            comment.EventId = Shared.CurrUser.currEvent;
            comment.EmailId = Shared.CurrUser.currUser;
            comment.Time = DateTime.Now.ToString();

            if (ModelState.IsValid && comment.EventId!=null && comment.EmailId!=null)

            {
                _context.Comments.Add(comment);
                await _context.SaveChangesAsync();
                string x = "/Events/Details/";
                x += Shared.CurrUser.currEvent;


                return Redirect(x);
            }
           else return RedirectToAction("Login", "LoginUsers");

        }



        // GET: Comments/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (Shared.CurrUser.currUser != null)
            {

                var @comment = await _context.Comments.FindAsync(id);
                if (@comment == null)
                {
                    return NotFound();
                }
              


                return View(@comment);
            }
            return RedirectToAction("Login", "LoginUsers");
        }

        // POST: Comments/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id,  [Bind("CommentAdded,Date,Time,EventId,EmailId")] Comment comment)
        {
            if (id != comment.Time)
            {
                return NotFound();
            }

            if (ModelState.IsValid && Shared.CurrUser.currUser==comment.EmailId && Shared.CurrUser.currEvent!=null  )
            {
               
                try
                {
                    Comment @c1 = comment;
                    _context.Comments.Update(@c1);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {

                    if (!CommentExists(comment.Time))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                var @event =  _context.Events.FindAsync(comment.EventId);

                string x = "/Events/Details/";
                x+=Shared.CurrUser.currEvent;

                return Redirect(x);
            }
            return RedirectToAction("Login","LoginUsers");
        
        }

        // GET: Comments/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments
                .FirstOrDefaultAsync(m => m.Time== id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var comment = await _context.Comments.FindAsync(id);
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommentExists(string id)
        {
            return _context.Comments.Any(e => e.CommentAdded == id);
        }
    }
}
