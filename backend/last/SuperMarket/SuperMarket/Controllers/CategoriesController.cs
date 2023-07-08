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
    public class CategoriesController : ControllerBase
    {
        private readonly SMDbContext _context;
        public CategoriesController(SMDbContext context)
        {
            _context = context;
        }
        private List<Categories> categories = new List<Categories>();
        private int nextCategoryId = 1;

        // GET api/categories
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _context.Categories.ToListAsync();
            return Ok(categories);
        }

        // GET: api/categories/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                return NotFound();

            return Ok(category);
        }

        // POST: api/categories
        [Authorize(Roles ="Admin")]
        [HttpPost]
        public IActionResult CreateCategory(Categories category)
        {
            _context.Categories.Add(category);
            category.CategoryName = category.CategoryName;
            category.Description = category.Description;
            category.Products = category.Products;


            _context.SaveChanges();

            return CreatedAtAction(nameof(GetCategory), new { id = category.CategoryId }, category);
        }


        // PUT: api/categories/{id}
        [Authorize(Roles ="Admin")]
        [HttpPut("{id}")]
        public IActionResult UpdateCategory(int id, Categories updatedCategory)
        {
            var category = _context.Categories.Find(id);

            if (category == null)
            {
                return NotFound();
            }

            category.CategoryName = updatedCategory.CategoryName;
            category.Description = updatedCategory.Description;
            category.Products = updatedCategory.Products;

            _context.SaveChanges();

            return NoContent();
        }


        // DELETE: api/categories/{id}
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                return NotFound();

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoriesExists(int id)
        {
            return _context.Categories.Any(c => c.CategoryId == id);
        }

        /*GET api/categories: Retrieves all categories.
        GET api/categories/{id}: Retrieves a specific category by ID.
        POST api/categories: Creates a new category.
        PUT api/categories/{id}: Updates an existing category by ID.
        DELETE api/categories/{id}: Deletes a category by ID.*/
    }
}
