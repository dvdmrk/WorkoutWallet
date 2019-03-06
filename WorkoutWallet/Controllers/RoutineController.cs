using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WorkoutWallet.Models;

namespace WorkoutWallet
{
    public class RoutineController : Controller
    {
        private readonly WorkoutWalletContext _context;

        public RoutineController(WorkoutWalletContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Routines.ToList());
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exercises = _context.Routines.Include(e => e.Exercises).FirstOrDefault(i => i.Id == id).Exercises;
            if (exercises == null)
            {
                return NotFound();
            }

            return View(exercises);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Routine routine)
        {
            if (ModelState.IsValid)
            {
                _context.Add(routine);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(routine);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var routine = _context.Routines.Find(id);
            if (routine == null)
            {
                return NotFound();
            }
            return View(routine);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Routine routine)
        {
            if (id != routine.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(routine);
                    _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoutineExists(routine.Id))
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
            return View(routine);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var routine = _context.Routines.Find(id);
            if (routine == null)
            {
                return NotFound();
            }

            return View(routine);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var routine = _context.Routines.Find(id);
            _context.Routines.Remove(routine);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool RoutineExists(int id)
        {
            return _context.Routines.Any(e => e.Id == id);
        }
    }
}
