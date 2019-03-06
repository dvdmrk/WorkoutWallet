using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WorkoutWallet.Models;
using WorkoutWallet.ViewModels;

namespace WorkoutWallet.Controllers
{
    public class ExerciseController : Controller
    {
        private readonly WorkoutWalletContext _context;

        public ExerciseController(WorkoutWalletContext context)
        {
            _context = context;
        }

        public IActionResult Index(int id)
        {
            ViewBag.Id = id;
            return View(_context.Exercises.Where(i => i.Routine.Id == id).ToList() ?? new List<Exercise>());
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exercise = _context.Exercises.Find(id);
            if (exercise == null)
            {
                return NotFound();
            }

            return View(exercise);
        }

        public IActionResult Create(int id)
        {
            return View(new ExerciseViewModel
            {
                RoutineId = id
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ExerciseViewModel exercise)
        {
            if (ModelState.IsValid)
            {
                _context.Exercises.Add(new Exercise
                {
                    Routine = _context.Routines.Find(exercise.RoutineId),
                    Name = exercise.Name,
                    Directions = exercise.Directions
                });
                _context.SaveChanges();
                return RedirectToAction(nameof(Index), new { id = exercise.RoutineId });
            }
            return View(exercise);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exercise = _context.Exercises.Find(id);
            if (exercise == null)
            {
                return NotFound();
            }
            return View(exercise);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Exercise exercise)
        {
            if (id != exercise.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exercise);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExerciseExists(exercise.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(exercise);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exercise = _context.Exercises.Find(id);
            if (exercise == null)
            {
                return NotFound();
            }

            return View(exercise);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var exercise = _context.Exercises.Find(id);
            _context.Exercises.Remove(exercise);
            _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExerciseExists(int id)
        {
            return _context.Exercises.Any(e => e.Id == id);
        }
    }
}
