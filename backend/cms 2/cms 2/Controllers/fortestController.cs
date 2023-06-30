using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using cms_2.Models;
using cms_2.data;
using Microsoft.Win32;

namespace cms_2.Controllers
{
    public class fortestController : Controller
    {
        private readonly AppDBContext _context;
        private object _logger;

        public fortestController(AppDBContext context, object logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: fortest
        public async Task<IActionResult> Index()
        {
            return _context.registerentity != null ?
                        View(await _context.registerentity.ToListAsync()) :
                        Problem("Entity set 'AppDBContext.registerentity'  is null.");
        }

        // GET: fortest/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.registerentity == null)
            {
                return NotFound();
            }

            var register = await _context.registerentity
                .FirstOrDefaultAsync(m => m.Id == id);
            if (register == null)
            {
                return NotFound();
            }

            return View(register);
        }

        // GET: fortest/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: fortest/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserName,Password,Email")] register register)
        {
            if (ModelState.IsValid)
            {
                _context.Add(register);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(register);
        }

        // GET: fortest/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.registerentity == null)
            {
                return NotFound();
            }

            var register = await _context.registerentity.FindAsync(id);
            if (register == null)
            {
                return NotFound();
            }
            return View(register);
        }

        // POST: fortest/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserName,Password,Email")] register register)
        {
            if (id != register.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(register);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!registerExists(register.Id))
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
            return View(register);
        }

        // GET: fortest/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.registerentity == null)
            {
                return NotFound();
            }

            var register = await _context.registerentity
                .FirstOrDefaultAsync(m => m.Id == id);
            if (register == null)
            {
                return NotFound();
            }

            return View(register);
        }

        // POST: fortest/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.registerentity == null)
            {
                return Problem("Entity set 'AppDBContext.registerentity'  is null.");
            }
            var register = await _context.registerentity.FindAsync(id);
            if (register != null)
            {
                _context.registerentity.Remove(register);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool registerExists(int id)
        {
            return (_context.registerentity?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        
        /*
          public IActionResult cre()
          {
              return View();
          }
          [HttpPost]
          [ValidateAntiForgeryToken]
          public async Task<IActionResult> reg(register user)
          {
              if (ModelState.IsValid)
              {
                  _context.registerentity.Add(user);
                  await _context.SaveChangesAsync();
                  return RedirectToAction(nameof(Index));
              }
              return View(user);
          }



          [HttpPost]
          [ValidateAntiForgeryToken]
          public async Task<IActionResult> Cre([Bind("Id,UserName,Password,Email")] register registerViewModel, object _logger)
          {
              if (ModelState.IsValid)
              {
                  // Map the RegisterViewModel to the Register entity
                  var register = new register
                  {
                      UserName = registerViewModel.UserName,
                      Password = registerViewModel.Password,
                      Email = registerViewModel.Email
                  };

                  try
                  {
                      // Save the register entity to the database
                      _context.registerentity.Add(register);
                      await _context.SaveChangesAsync();

                      // Redirect to a success page or perform other actions
                      return RedirectToAction("RegistrationSuccess");
                  }
                  catch (Exception ex)
                  {
                      // Handle any errors that occurred during registration
                      // Log the error, display an error message, etc.
                      return RedirectToAction("you are bad");

                  }
              }

              // If the model state is not valid, return to the registration page with validation errors
              return View("Register", registerViewModel);
          }

      }
        */

    }
}

