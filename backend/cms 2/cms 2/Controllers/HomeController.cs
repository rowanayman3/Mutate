using cms_2.data;
using cms_2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace cms_2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly AppDBContext _context;

        public HomeController(AppDBContext context , ILogger<HomeController>logger )
        {
            _context = context;
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        
        [Route("login")]
        public IActionResult login()
        {
            return View();
        }
        [Route("/home/register")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(register model)
        {
            if (ModelState.IsValid)
            {
                // Map the data from the view model to a new User entity
                var user = new register
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    // Set other properties as needed
                };

                // Save the user to the database using EF Core
                _context.registerentity.Add(user);
                _context.SaveChanges();

                // Redirect to a success page or perform any other necessary actions
                return RedirectToAction("RegistrationSuccess");
            }

            // If the model is not valid, redisplay the registration form with validation errors
            return View(model);
        }



        /* public async Task<IActionResult> register([Bind("Id,UserName,Password,Email")] register register)
        {
            if (ModelState.IsValid)
            {
                //_context.Add(register);
                //await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));


            }
            return View(register);
        }
        */

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
        [Route("/login/forget_password")]
        public IActionResult forgetPass()
        {
            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [Route("/home/register")]
        [HttpGet]
        public IActionResult register()
        {
            return View();
        }
        private bool registerExists(int id)
        {
            return (_context.registerentity?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}