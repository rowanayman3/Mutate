using cms_2.Models;
using Microsoft.AspNetCore.Mvc;

namespace cms_2.Controllers
{
    public class accountsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult login(string username, string password)
        {
            // Check if the user exists and validate the credentials
            bool userExists = IsUserValid(username, password);

            if (userExists)
            {
                // User exists and credentials are valid, perform successful login actions
                return RedirectToAction("index", "Home"); // Redirect to the Dashboard page
            }
            else
            {
                // User does not exist or invalid credentials, return error response
                return BadRequest(new { message = "Invalid credentials" });
            }
        }

        private bool IsUserValid(string username, string password)
        {
            // Perform the necessary logic to validate the user
            // This may involve checking against a user database or authentication system

            // Example logic: Check against hardcoded credentials
            if (username == "admin" && password == "password")
            {
                return true;
            }

            return false;
        }
    }
}
