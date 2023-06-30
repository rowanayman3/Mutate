using cms_2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace cms_2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
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
        [Route("register")]
        public IActionResult register()
        {
            return View();
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
    }
}