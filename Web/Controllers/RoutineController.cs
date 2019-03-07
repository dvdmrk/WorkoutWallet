using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web.Common;
using Web.Data;
using Web.Models;
using Web.ViewModels.RoutineViewModels;

namespace Web.Controllers
{
    [Authorize]

    public class RoutineController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public RoutineController(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.CurrentUserId = GetUserId();
            return View(_context.Routines.Include(e => e.ExerciseRoutines).Include(e => e.RoutineProfiles).Include(e => e.Workouts).Include(e => e.CreateBy).ThenInclude(e => e.Profile).Select(e => new RoutineIndexViewModel
            {
                Id = e.Id,
                CreatedByName = e.CreateBy.Profile.UserName,
                CreatedById = e.CreateBy.Id,
                CreatedDate = e.CreateDate,
                Name = e.Name,
                Description = e.Description,
                HasVideo = !String.IsNullOrEmpty(e.VideoUrl),
                NumberOfExercises = e.ExerciseRoutines == null ? 0 : e.ExerciseRoutines.Count,
                NumberOfUsers = e.RoutineProfiles == null ? 0 : e.RoutineProfiles.Count,
                NumberOfWorkouts = e.Workouts == null ? 0 : e.Workouts.Count,
            }).ToList());
        }

        public IActionResult Details(Guid? id)
        {
            ViewBag.CurrentUserId = GetUserId();

            if (id == null)
            {
                return NotFound();
            }
            var test = _context.Set<Routine>().Find(id);
            var routine = _context.Set<Routine>().Include(e => e.CreateBy).ThenInclude(e => e.Profile).Where(e => e.Id == id).Select(e => new RoutineViewModel
            {
                Id = e.Id,
                CreatedByName = e.CreateBy.Profile.UserName,
                CreatedById = e.CreateBy.Id,
                CreatedDate = e.CreateDate,
                Name = e.Name,
                Description = e.Description,
                VideoUrl = e.VideoUrl
            }).FirstOrDefault();

            if (routine == null)
            {
                return NotFound();
            }

            return View(routine);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RoutineViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var routine = new Routine
                {
                    VideoUrl = vm.VideoUrl,
                    Name = vm.Name,
                    Description = vm.Description,
                    CreateBy = _context.Set<User>().Find(GetUserId()),
                    CreateDate = DateTime.Now
                };
                _context.Add(routine);
                _context.SaveChanges();
                return RedirectToAction("Edit", new { id = routine.Id });
            }
            return View(vm);
        }

        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewBag.ExerciseListItems = GetDropdownViewModels<Exercise>();

            var routine = _context.Set<Routine>().Where(e => e.Id == id).Select(e => new RoutineViewModel
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description,
                VideoUrl = e.VideoUrl,
                Exercises = e.ExerciseRoutines.Select(c => new BaseNamedEntity {
                    Id = c.ExerciseId,
                    Name = c.Exercise.Name,
                    Description = c.Exercise.Description
                }).ToList()
            }).FirstOrDefault();
            if (routine == null)
            {
                return NotFound();
            }
            return View(routine);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(RoutineViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var routine = _context.Set<Routine>().Find(vm.Id);
                    routine.VideoUrl = vm.VideoUrl;
                    routine.Name = vm.Name;
                    routine.Description = vm.Description;
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoutineExists(vm.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        public IActionResult Delete(Guid? id)
        {
            ViewBag.CurrentUserId = GetUserId();

            if (id == null) return NotFound();

            var routine = _context.Set<Routine>().Include(e => e.CreateBy).ThenInclude(e => e.Profile).Where(e => e.Id == id).Select(e => new RoutineViewModel
            {
                Id = e.Id,
                CreatedByName = e.CreateBy.Profile.UserName,
                CreatedById = e.CreateBy.Id,
                CreatedDate = e.CreateDate,
                Name = e.Name,
                Description = e.Description,
                VideoUrl = e.VideoUrl
            }).FirstOrDefault();

            if (routine == null) return NotFound();

            return View(routine);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Guid id)
        {
            var routine = _context.Routines.Find(id);
            _context.Routines.Remove(routine);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public JsonResult AddExerciseToRoutine(Guid exerciseId, Guid routineId)
        {
            AddRelationship(new ExerciseRoutine
            {
                ExerciseId = exerciseId,
                RoutineId = routineId
            });
            var exercise = _context.Set<Exercise>().Find(exerciseId);
            return Json(new { Description = exercise.Description, Name = exercise.Name });
        }

        private bool RoutineExists(Guid id)
        {
            return _context.Routines.Any(e => e.Id == id);
        }
    }
}
