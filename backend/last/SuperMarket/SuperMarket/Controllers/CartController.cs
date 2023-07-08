using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperMarket.Models;

namespace SuperMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly SMDbContext _context;
        public CartController(SMDbContext context)
        {
            _context = context;
        }
        private List<Carts> carts = new List<Carts>();
        private int nextCartId = 1;

        // GET api/carts
        [HttpGet]
        public async Task<IActionResult> GetCarts()
        {
            var carts = await _context.Carts.ToListAsync();
            return Ok(carts);
        }

        // GET: api/categories/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var cart = await _context.Carts.FindAsync(id);
            if (cart == null)
                return NotFound();

            return Ok(cart);
        }

        // POST: api/carts
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateCategory(Carts cart)
        {
            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCarts), new { id = cart.CartId }, cart);
        }

        // PUT: api/carts/{id}
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
       
        public async Task<IActionResult> UpdateCart(int id, Carts updatedCart)
        {
            if (id != updatedCart.CartId)
                return BadRequest();

            _context.Entry(updatedCart).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CartExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }


        // DELETE: api/categories/{id}
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        
        public async Task<IActionResult> DeleteCart(int id)
        {
            var cart = await _context.Carts.FindAsync(id);
            if (cart == null)
                return NotFound();

            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CartExists(int id)
        {
            return _context.Carts.Any(c => c.CartId == id);
        }
    }
}
