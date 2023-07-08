using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperMarket.Models;
using System.Data;

namespace SuperMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly SMDbContext _context;

        public ProductsController(SMDbContext context)
        {
            _context = context;
        }

        // GET: api/products
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _context.Products.ToListAsync();
            return Ok(products);
        }

        // GET: api/products/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            return Ok(product);
        }

        // POST: api/products
        [Authorize(Roles ="Admin")]
        [HttpPost]
        public IActionResult CreateProduct(Products products)
        {
            _context.Products.Add(products);
            products.Name = products.Name;
            products.Description = products.Description;
            products.price = products.price;
            products.quantity = products.quantity;
            products.discount = products.discount;
            products.TotalPrice = products.TotalPrice;


            _context.SaveChanges();

            return CreatedAtAction(nameof(GetProduct), new { id = products.Id }, products);
        }

        // PUT: api/products/{id}
        [Authorize(Roles ="Admin")]
        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, Products updatedProduct)
        {
            var product = _context.Products.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            product.Name = updatedProduct.Name;
            product.Description = updatedProduct.Description;
            product.price = updatedProduct.price;
            product.quantity = updatedProduct.quantity;
            product.discount = updatedProduct.discount;
            product.TotalPrice = updatedProduct.TotalPrice;


            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/products/{id}
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(p => p.Id == id);
        }

        /*GET api/products: Retrieves all products.
        GET api/products/{id}: Retrieves a specific product by ID.
        POST api/products: Creates a new product.
        PUT api/products/{id}: Updates an existing product by ID.
        DELETE api/products/{id}: Deletes a product by ID.*/

    }
}
