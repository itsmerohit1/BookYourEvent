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
    public class RegisterUsersController : Controller
    {
        private readonly AppDbContext _context;

        public RegisterUsersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: RegisterUsers
         
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }



        // POST: RegisterUsers/Create
     

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("UserName,EmailId,Password")] RegisterUser registerUser)
        {

            User user = new RegisterUserToUser().RegisterUserModelToUserMapping(registerUser);


            if (ModelState.IsValid )
            {
                if (RegisterUserExists(registerUser.EmailId))
                {
                    string x = CurrUser.currUser;
                    return RedirectToAction("Login","LoginUsers");
                }
                else
                {
                    try
                    {
                        _context.Users.Add(user);
                        await _context.SaveChangesAsync();
                        return RedirectToAction("Login", "LoginUsers");
                    }
                    catch
                    {
                        return RedirectToAction("Register", "RegisterUsers");
                    }
                }
            }
               return RedirectToAction("Register", "RegisterUsers");
        }

        // GET: RegisterUsers/Edit/5
      
        private bool RegisterUserExists(string id)
        {
            return _context.Users.Any(e => e.EmailId == id);
        }










































        /*
         *   public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registerUser = await _context.Users.FindAsync(id);
            if (registerUser == null)
            {
                return NotFound();
            }
            return View(registerUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("UserName,EmailId,Password")] RegisterUser registerUser)
        {
            if (id != registerUser.EmailId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(registerUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegisterUserExists(registerUser.EmailId))
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
            return View(registerUser);
        }
        

        // GET: RegisterUsers/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registerUser = await _context.Users
                .FirstOrDefaultAsync(m => m.EmailId == id);
            if (registerUser == null)
            {
                return NotFound();
            }

            return View(registerUser);
        }

        // POST: RegisterUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var registerUser = await _context.Users.FindAsync(id);
            _context.Users.Remove(registerUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        */

    }
}
