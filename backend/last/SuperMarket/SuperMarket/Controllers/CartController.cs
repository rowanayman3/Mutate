﻿using Microsoft.AspNetCore.Authorization;
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

        // GET: api/carts/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCart(int id)
        {
            var cart = await _context.Carts.FindAsync(id);
            if (cart == null)
                return NotFound();

            return Ok(cart);
        }

        // POST: api/carts
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult CreateCart(Carts cart)
        {
            _context.Carts.Add(cart);
            cart.UserId = cart.UserId;
            cart.ProductId = cart.ProductId;
            cart.Price = cart.Price;
            cart.Quantity = cart.Quantity;
            cart.Status = cart.Status;
            cart.Discount = cart.Discount;
            cart.TotalPrice = cart.TotalPrice;

            _context.SaveChanges();

            return CreatedAtAction(nameof(GetCart), new { id = cart.CartId }, cart);
        }

        // PUT: api/carts/{id}
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
       
        public async Task<IActionResult> UpdateCart(int id, Carts updatedCart)
        {
            var cart = _context.Carts.Find(id);

            if (cart == null)
            {
                return NotFound();
            }
            cart.UserId = updatedCart.UserId;
            cart.ProductId = updatedCart.ProductId;
            cart.Price = updatedCart.Price;
            cart.Quantity = updatedCart.Quantity;
            cart.Status = updatedCart.Status;
            cart.Discount = updatedCart.Discount;
            cart.TotalPrice = updatedCart.TotalPrice;


            _context.SaveChanges();

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
