using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Models;
using Web.ViewModels.ExerciseViewModels;

namespace Web.Controllers
{
    [Authorize]
    public class ExerciseController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExerciseController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Set<Exercise>().Include(e => e.ExerciseRoutines).Include(e => e.ExerciseSets).Include(e => e.ExerciseMuscleGroups).Include(e => e.CreateBy).ThenInclude(e => e.Profile).Select(e => new ExerciseIndexViewModel
            {
                CreatedByName = e.CreateBy.Profile.UserName,
                CreatedById = e.CreateBy.Id,
                CreatedDate = e.CreateDate,
                HasVideo = !String.IsNullOrEmpty(e.VideoUrl),
                ExerciseType = e.ExerciseType,
                NumberOfRoutines = e.ExerciseRoutines.Count,
                NumberOfExerciseSetsCompleted = e.ExerciseSets.Count()
            }).ToList());
        }

        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exercise = _context.Set<Exercise>()
                .FirstOrDefault(m => m.Id == id);
            if (exercise == null)
            {
                return NotFound();
            }

            return View(exercise);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Exercise exercise)
        {
            if (ModelState.IsValid)
            {
                exercise.Id = Guid.NewGuid();
                _context.Add(exercise);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(exercise);
        }

        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exercise =  _context.Set<Exercise>().Find(id);
            if (exercise == null)
            {
                return NotFound();
            }
            return View(exercise);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Exercise exercise)
        {
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

        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exercise =  _context.Set<Exercise>()
                .FirstOrDefault(m => m.Id == id);
            if (exercise == null)
            {
                return NotFound();
            }

            return View(exercise);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Guid id)
        {
            var exercise =  _context.Set<Exercise>().Find(id);
            _context.Set<Exercise>().Remove(exercise);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool ExerciseExists(Guid id)
        {
            return _context.Set<Exercise>().Any(e => e.Id == id);
        }
    }
}
