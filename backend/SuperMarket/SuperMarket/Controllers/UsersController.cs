using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperMarket.Models;

namespace SuperMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly SMDbContext _context;

        public UsersController(SMDbContext context)
        {
            _context = context;
        }

        private List<Users> users = new List<Users>();
        private int nextUserId = 1;


        // GET: api/users
        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _context.Users.ToList();
            return Ok(users);
        }

        // GET: api/users/{id}
        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var user = _context.Users.Find(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // POST: api/users
        [HttpPost]
        public IActionResult CreateUser(Users user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        // PUT: api/users/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, Users updatedUser)
        {
            var user = _context.Users.Find(id);

            if (user == null)
            {
                return NotFound();
            }

            user.Name = updatedUser.Name;
            user.Username = updatedUser.Username;
            user.Email = updatedUser.Email;
            // Update other properties as needed

            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/users/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = _context.Users.Find(id);

            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            _context.SaveChanges();

            return NoContent();
        }
    }
}


    

