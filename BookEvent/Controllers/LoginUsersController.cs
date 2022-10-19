using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookEvent.Models;
using Shared;
using System.Web;
using BookEvent.Helper;

namespace BookEvent.Controllers
{
    public class LoginUsersController : Controller
    {
        private readonly AppDbContext _context;

        public LoginUsersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: LoginUsers


        // GET: LoginUsers/Create
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult ErrorLoginUsers()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Logout()
        {
            CurrUser.currUser = null;
            return RedirectToAction("", "Home");

        }


        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("EmailId,Password")] LoginUser loginUser)
        {

            User user = new LoginUserToUser().LoginUserToUserMapping(loginUser);


            if (ModelState.IsValid)
            {
                if (LoginUserExists(loginUser.EmailId , loginUser.Password))
                {

                    var x = CurrUser.currUser;

                    CurrUser.currUser= loginUser.EmailId;

                    x = loginUser.EmailId;

                    return RedirectToAction("", "Home");

                }
                else
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction("ErrorLoginUsers");
                }
            }
            return RedirectToAction("ErrorLoginUsers");
        }

        private bool LoginUserExists(string id, string password)
        {
           
           bool c2 =  _context.Users.Any(e => e.Password == password && e.EmailId==id);

           return c2;
        }
    }
}




        /*
        // GET: LoginUsers/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loginUser = await _context.Users.FindAsync(id);
            if (loginUser == null)
            {
                return NotFound();
            }
            return View(loginUser);
        }

        // POST: LoginUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("EmailId,Password")] LoginUser loginUser)
        {
            if (id != loginUser.EmailId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loginUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoginUserExists(loginUser.EmailId))
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
            return View(loginUser);
        }

        // GET: LoginUsers/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loginUser = await _context.Users
                .FirstOrDefaultAsync(m => m.EmailId == id);
            if (loginUser == null)
            {
                return NotFound();
            }

            return View(loginUser);
        }

        // POST: LoginUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var loginUser = await _context.Users.FindAsync(id);
            _context.Users.Remove(loginUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

       
    }
}

/*
   public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        // GET: LoginUsers/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loginUser = await _context.Users
                .FirstOrDefaultAsync(m => m.EmailId == id);
            if (loginUser == null)
            {
                return NotFound();
            }

            return View(loginUser);
        }
*/