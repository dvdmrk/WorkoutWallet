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
using Web.ViewModels.ExerciseViewModels;

namespace Web.Controllers
{
    [Authorize]
    public class ExerciseController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public ExerciseController(ApplicationDbContext context) : base (context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Set<Exercise>().Include(e => e.ExerciseRoutines).Include(e => e.ExerciseSets).Include(e => e.ExerciseMuscleGroups).Include(e => e.CreateBy).ThenInclude(e => e.Profile).Select(e => new ExerciseIndexViewModel
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description,
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

            var exercise = _context.Set<Exercise>().Include(e => e.CreateBy).ThenInclude(e => e.Profile).Where(c => c.Id == id).Select(e => new ExerciseViewModel
            {
                Id = e.Id,
                CreatedByName = e.CreateBy.Profile.UserName,
                CreatedDate = e.CreateDate,
                ExerciseType = e.ExerciseType,
                Name = e.Name,
                Description = e.Description,
                VideoUrl = e.VideoUrl
            }).FirstOrDefault();

            if (exercise == null)
            {
                return NotFound();
            }

            return View(exercise);
        }

        public IActionResult Create(Guid id)
        {
            return View(new ExerciseViewModel
            {
                RoutineId = id,
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ExerciseViewModel vm)
        {
            if (ModelState.IsValid)
            {
                ExerciseRoutine exerciseRoutine = new ExerciseRoutine(); 
                var exercise = new Exercise
                {
                    Name = vm.Name,
                    Description = vm.Description,
                    CreateBy = _context.Set<User>().Find(GetUserId()),
                    CreateDate = DateTime.Now,
                    ExerciseType = vm.ExerciseType,
                    VideoUrl = vm.VideoUrl
                };
                _context.Set<Exercise>().Add(exercise);

                if (vm.RoutineId != null && vm.RoutineId != Guid.Empty) { 
                    exerciseRoutine = new ExerciseRoutine
                    {
                        Routine = _context.Set<Routine>().Find(vm.RoutineId),
                        Exercise = exercise
                    };
                    _context.Set<ExerciseRoutine>().Add(exerciseRoutine);
                }

                var erd = new ExerciseRoutineDetail
                {
                    ExerciseRoutine = exerciseRoutine,
                    OrderInRoutine = _context.Set<ExerciseRoutine>().Where(c => c.RoutineId == vm.RoutineId).Sum(c => c.ExerciseRoutineDetails.Count())
                };

                _context.Set<ExerciseRoutineDetail>().Add(erd);
                _context.SaveChanges();
                if (vm.RoutineId != null && vm.RoutineId != Guid.Empty)
                    return RedirectToAction("Edit", "Routine", new { id = vm.RoutineId });
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exercise =  _context.Set<Exercise>().Include(c => c.ExerciseMuscleGroups).Where(c => c.Id == id).Select(e => new ExerciseViewModel
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description,
                ExerciseType = e.ExerciseType,
                VideoUrl = e.VideoUrl,
                MuscleGroups = e.ExerciseMuscleGroups.Select(c => new BaseNamedEntity
                {
                    Id = c.MuscleGroupId,
                    Name = c.MuscleGroup.Name,
                    Description = c.MuscleGroup.Description
                }).ToList()
            }).FirstOrDefault();

            ViewBag.MuscleGroupListItems = GetDropdownViewModels<MuscleGroup>().Where(c => exercise.MuscleGroups.All(e => e.Id != Guid.Parse(c.Value)));

            if (exercise == null)
            {
                return NotFound();
            }
            return View(exercise);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ExerciseViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var exercise = _context.Set<Exercise>().Find(vm.Id);
                    exercise.Name = vm.Name;
                    exercise.ExerciseType = vm.ExerciseType;
                    exercise.Description = vm.Description;
                    exercise.VideoUrl = vm.VideoUrl;
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EntityExists<BaseEntity>(vm.Id))
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
            return View(vm);
        }

        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exercise = _context.Set<Exercise>().Include(e => e.CreateBy).ThenInclude(e => e.Profile).Where(c => c.Id == id).Select(e => new ExerciseViewModel
            {
                Id = e.Id,
                CreatedByName = e.CreateBy.Profile.UserName,
                CreatedDate = e.CreateDate,
                Name = e.Name,
                Description = e.Description,
                ExerciseType = e.ExerciseType,
                VideoUrl = e.VideoUrl
            }).FirstOrDefault();

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

        public JsonResult AddMuscleGroupToExercise(Guid muscleGroupId, Guid exerciseId)
        {
            AddRelationship(new ExerciseMuscleGroup
            {
                MuscleGroupId = muscleGroupId,
                ExerciseId = exerciseId
            });
            var muscleGroup = _context.Set<MuscleGroup>().Find(muscleGroupId);
            return Json(new { Description = muscleGroup.Description, Name = muscleGroup.Name });
        }
    }
}
