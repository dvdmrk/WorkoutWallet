using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Models;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoutineController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RoutineController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Routine
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Routine>>> GetRoutines()
        {
            return await _context.Routines.ToListAsync();
        }

        // GET: api/Routine/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Routine>> GetRoutine(Guid id)
        {
            var routine = await _context.Routines.FindAsync(id);

            if (routine == null)
            {
                return NotFound();
            }

            return routine;
        }

        // PUT: api/Routine/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoutine(Guid id, Routine routine)
        {
            if (id != routine.Id)
            {
                return BadRequest();
            }

            _context.Entry(routine).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoutineExists(id))
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

        // POST: api/Routine
        [HttpPost]
        public async Task<ActionResult<Routine>> PostRoutine(Routine routine)
        {
            _context.Routines.Add(routine);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRoutine", new { id = routine.Id }, routine);
        }

        // DELETE: api/Routine/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Routine>> DeleteRoutine(Guid id)
        {
            var routine = await _context.Routines.FindAsync(id);
            if (routine == null)
            {
                return NotFound();
            }

            _context.Routines.Remove(routine);
            await _context.SaveChangesAsync();

            return routine;
        }

        private bool RoutineExists(Guid id)
        {
            return _context.Routines.Any(e => e.Id == id);
        }
    }
}
