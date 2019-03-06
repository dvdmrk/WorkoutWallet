using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkoutWallet.Models;

namespace WorkoutWallet
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoutineApiController : ControllerBase
    {
        private readonly WorkoutWalletContext _context;

        public RoutineApiController(WorkoutWalletContext context)
        {
            _context = context;
        }

        // GET: api/RoutineApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Routine>>> GetRoutines()
        {
            return await _context.Routines.ToListAsync();
        }

        // GET: api/RoutineApi/5
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Exercise>> GetExercises(int id)
        {
            var exercises = _context.Exercises.Where(c => c.Routine.Id == id);

            if (exercises == null)
            {
                return NotFound();
            }

            return exercises.ToList();
        }

        // POST: api/RoutineApi
        [HttpPost]
        public async Task<ActionResult<Routine>> PostSet(Set set1)
        {
            var set = new Set
            {
                Reps = set1.Reps,
                Weight = set1.Weight
            };
            _context.Sets.Add(set);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRoutine", new { id = set.Id }, set);
        }


        private bool RoutineExists(int id)
        {
            return _context.Routines.Any(e => e.Id == id);
        }
    }
}
