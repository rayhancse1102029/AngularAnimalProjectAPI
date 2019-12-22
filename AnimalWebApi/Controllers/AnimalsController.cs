using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AnimalWebApi.Models;
using Microsoft.AspNetCore.Cors;

namespace AnimalWebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Animals")]
    [EnableCors("CorsPolicy")]
    public class AnimalsController : Controller
    {
        private readonly AnimalApplicationDbContext _context;

        public AnimalsController(AnimalApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Animals
        [HttpGet]
        public IEnumerable<Animal> GetAnimal()
        {
            return _context.Animal;
        }

        // GET: api/Animals/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAnimal([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var animal = await _context.Animal.SingleOrDefaultAsync(m => m.Id == id);

            if (animal == null)
            {
                return NotFound();
            }

            return Ok(animal);
        }

        // PUT: api/Animals/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnimal([FromRoute] int id, [FromBody] Animal animal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != animal.Id)
            {
                return BadRequest();
            }

            _context.Entry(animal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnimalExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Animals
        [HttpPost]
        public async Task<IActionResult> PostAnimal([FromBody] Animal animal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Animal.Add(animal);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAnimal", new { id = animal.Id }, animal);
        }

        // DELETE: api/Animals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnimal([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var animal = await _context.Animal.SingleOrDefaultAsync(m => m.Id == id);
            if (animal == null)
            {
                return NotFound();
            }

            _context.Animal.Remove(animal);
            await _context.SaveChangesAsync();

            return Ok(animal);
        }

        private bool AnimalExists(int id)
        {
            return _context.Animal.Any(e => e.Id == id);
        }
    }
}