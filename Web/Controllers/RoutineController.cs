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
                NumberOfExercises = e.ExerciseRoutines.Sum(c => c.ExerciseRoutineDetails.Count),
                NumberOfUsers = e.RoutineProfiles == null ? 0 : e.RoutineProfiles.Count,
                NumberOfWorkouts = e.Workouts == null ? 0 : e.Workouts.Count,
            }).ToList());
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

        public IActionResult Details(Guid? id)
        {
            ViewBag.CurrentUserId = GetUserId();

            if (id == null)
            {
                return NotFound();
            }
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

        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewBag.ExerciseListItems = GetDropdownViewModels<Exercise>();

            var routine = _context.Set<Routine>().Include(c => c.ExerciseRoutines).Where(e => e.Id == id);
            var vm = routine.Select(e => new RoutineViewModel
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description,
                Exercises = _context.Set<ExerciseRoutineDetail>().Include(c => c.ExerciseRoutine).ThenInclude(c => c.Exercise).ThenInclude(c => c.CreateBy).Include(c => c.ExerciseRoutine).ThenInclude(c => c.Routine).Where(c => c.ExerciseRoutine.RoutineId == id).Select(c => new ExerciseIndexPartialViewModel
                {
                    Id = c.Id,
                    Name = c.ExerciseRoutine.Exercise.Name,
                    Description = c.ExerciseRoutine.Exercise.Description,
                    RecommendedNumberOfReps = c.RecommendedNumberOfReps,
                    RecommendedPercentOfMax = c.RecommendedPercentOfMax,
                    TillFailure = c.TillFailure,
                    OrderInRoutine = c.OrderInRoutine
                }).OrderBy(c => c.OrderInRoutine).ToList()
            }).FirstOrDefault();

            if (vm == null)
            {
                return NotFound();
            }
            return View(vm);
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
                    if (!EntityExists<Routine>(vm.Id)) return NotFound();
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

        public IActionResult DeleteExerciseRoutineDetail(Guid id)
        {
            var exerciseRoutineDetail = _context.Set<ExerciseRoutineDetail>().Include(c => c.ExerciseRoutine).ThenInclude(c => c.ExerciseRoutineDetails).FirstOrDefault(c => c.Id == id);
            var routineId = exerciseRoutineDetail.ExerciseRoutine.RoutineId;
            var exerciseRoutineDetails = _context.Set<Routine>().Include(e => e.ExerciseRoutines).ThenInclude(c => c.ExerciseRoutineDetails).FirstOrDefault(c => c.Id == routineId).ExerciseRoutines.SelectMany(c => c.ExerciseRoutineDetails).Where(c => c.Id != id).ToList();
            _context.Set<ExerciseRoutineDetail>().Remove(exerciseRoutineDetail);

            var i = 0;
            foreach (var set in exerciseRoutineDetails.OrderBy(c => c.OrderInRoutine)) {
                set.OrderInRoutine = i;
                i++;
            }
            _context.SaveChanges();

            return ExerciseRoutineIndexPartial(routineId);
        }

        public IActionResult OrderExercises(List<string> ids)
        {
            Guid routineId = Guid.Empty;
            var i = 0;
            foreach (var id in ids)
            {
                var erd = _context.Set<ExerciseRoutineDetail>().Include(c => c.ExerciseRoutine).FirstOrDefault(c => c.Id == Guid.Parse(id));
                if (i == 0) routineId = erd.ExerciseRoutine.RoutineId;
                erd.OrderInRoutine = i;
                i++;
            }
            _context.SaveChanges();

            return ExerciseRoutineIndexPartial(routineId);
        }

        public IActionResult CopyExerciseRoutineDetail(Guid id)
        {
            var erd = _context.Set<ExerciseRoutineDetail>().Include(c => c.ExerciseRoutine).FirstOrDefault(c => c.Id == id);
            var routineId = erd.ExerciseRoutine.RoutineId;

            var detail = new ExerciseRoutineDetail
            {
                ExerciseRoutine = erd.ExerciseRoutine,
                RecommendedNumberOfReps = erd.RecommendedNumberOfReps,
                RecommendedPercentOfMax = erd.RecommendedPercentOfMax,
                TillFailure = erd.TillFailure,
                OrderInRoutine = _context.Set<ExerciseRoutine>().Where(c => c.RoutineId == routineId).Sum(c => c.ExerciseRoutineDetails.Count()),
            };

            _context.Set<ExerciseRoutineDetail>().Add(detail);
            _context.SaveChanges();
            return ExerciseRoutineIndexPartial(routineId);
        }

        public IActionResult ExerciseRoutineIndexPartial(Guid id)
        {
            return PartialView("~/Views/Shared/Partials/_ExerciseRoutineIndexPartial.cshtml", _context.Set<ExerciseRoutineDetail>().Include(c => c.ExerciseRoutine).ThenInclude(c => c.Exercise).ThenInclude(c => c.CreateBy).Include(c => c.ExerciseRoutine).ThenInclude(c => c.Routine).Where(c => c.ExerciseRoutine.RoutineId == id).Select(c => new ExerciseIndexPartialViewModel
            {
                Id = c.Id,
                Name = c.ExerciseRoutine.Exercise.Name,
                Description = c.ExerciseRoutine.Exercise.Description,
                RecommendedNumberOfReps = c.RecommendedNumberOfReps,
                RecommendedPercentOfMax = c.RecommendedPercentOfMax,
                TillFailure = c.TillFailure,
                OrderInRoutine = c.OrderInRoutine
            }).OrderBy(c => c.OrderInRoutine).ToList());
        }

        public IActionResult ExerciseRoutineUpdatePartial(Guid id)
        {
            return PartialView("~/Views/Shared/Partials/_ExerciseRoutineUpdatePartial.cshtml", _context.Set<ExerciseRoutineDetail>().Where(e => e.Id == id).Select(e => new ExerciseIndexPartialViewModel
            {
                Id = e.Id,
                Name = e.ExerciseRoutine.Exercise.Name,
                Description = e.ExerciseRoutine.Exercise.Description,
                RecommendedNumberOfReps = e.RecommendedNumberOfReps,
                RecommendedPercentOfMax = e.RecommendedPercentOfMax,
                OrderInRoutine = e.OrderInRoutine,
                TillFailure = e.TillFailure
            }).FirstOrDefault());
        }

        [HttpPost]
        public void ExerciseRoutineUpdatePartial(ExerciseIndexPartialViewModel vm)
        {
            var exerciseRoutine = _context.Set<ExerciseRoutineDetail>().Include(c => c.ExerciseRoutine).FirstOrDefault(c => c.Id == vm.Id);
            exerciseRoutine.RecommendedNumberOfReps = vm.RecommendedNumberOfReps;
            exerciseRoutine.RecommendedPercentOfMax = vm.RecommendedPercentOfMax;
            exerciseRoutine.OrderInRoutine = vm.OrderInRoutine;
            exerciseRoutine.TillFailure = vm.TillFailure;
            _context.SaveChanges();
        }

        public IActionResult AddExerciseToRoutine(Guid exerciseId, Guid routineId)
        {
            var er = _context.Set<ExerciseRoutine>().Include(c => c.Exercise).Include(c => c.Routine).FirstOrDefault(c => c.ExerciseId == exerciseId && c.RoutineId == routineId) ?? new ExerciseRoutine
            {
                Exercise = _context.Set<Exercise>().Find(exerciseId),
                Routine = _context.Set<Routine>().Find(routineId)
            };

            if (er.Id == null || er.Id == Guid.Empty)
            {
                _context.Set<ExerciseRoutine>().Add(er);
                _context.SaveChanges();
            }

            var erd = new ExerciseRoutineDetail {
                ExerciseRoutine = er,
                OrderInRoutine = _context.Set<ExerciseRoutine>().Where(c => c.RoutineId == routineId).Sum(c => c.ExerciseRoutineDetails.Count())
            };
            _context.Set<ExerciseRoutineDetail>().Add(erd);
            _context.SaveChanges();

            return ExerciseRoutineIndexPartial(er.Routine.Id);
        }
    }
}
