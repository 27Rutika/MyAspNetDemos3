using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyDemos.Data;
using MyDemos.Models;

namespace MyDemos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            return await _context.Categories
                                .Include(c => c.Books)
                                .ToListAsync();
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, Category category)
        {
            if (id != category.CategoryId)
            {
                return BadRequest();
            }

            // Sanitize the Data
            category.CategoryName = category.CategoryName.Trim();

            // Server Side Validation
            bool isDuplicateFound = _context.Categories.Any(c => c.CategoryName == category.CategoryName);
            if (isDuplicateFound)
            {
                ModelState.AddModelError("POST", "Duplicate Category Found!");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Entry(category).State = EntityState.Modified;

                    await _context.SaveChangesAsync();
                    return NoContent();
                }
                catch (DbUpdateConcurrencyException exp)
                {
                    if (!CategoryExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        ModelState.AddModelError("PUT", exp.Message);
                    }
                }
            }

            return BadRequest(ModelState);
        }

        // POST: api/Categories
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            // Sanitize the Data
            category.CategoryName = category.CategoryName.Trim();

            // Server Side Validation
            bool isDuplicateFound = _context.Categories.Any(c => c.CategoryName == category.CategoryName);
            if (isDuplicateFound)
            {
                ModelState.AddModelError("POST", "Duplicate Category Found!");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Categories.Add(category);
                    await _context.SaveChangesAsync();

                    // return CreatedAtAction("GetCategory", new { id = category.CategoryId }, category);

                    // To enforce that the HTTP RESPONSE CODE 201 "CREATED", package the response.
                    var result = CreatedAtAction("GetCategory", new { id = category.CategoryId }, category);
                    return Ok(result);
                }
                catch (System.Exception exp)
                {
                    ModelState.AddModelError("POST", exp.Message);
                }
            }

            return BadRequest(ModelState);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Category>> DeleteCategory(int id)
        {
            try
            {
                var category = await _context.Categories.FindAsync(id);
                if (category == null)
                {
                    return NotFound();
                }

                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();

                return Ok(category);
            }

            catch (System.Exception exp)
            {
                ModelState.AddModelError("DELETE", exp.Message);
                return BadRequest(ModelState);
            }
        }
        

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.CategoryId == id);
        }
    }
}
