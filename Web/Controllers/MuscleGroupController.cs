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
using Web.ViewModels;

namespace Web.Controllers
{
    [Authorize]
    public class MuscleGroupController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public MuscleGroupController(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Set<MuscleGroup>().Select(e => new BaseNamedEntity
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description
            }).ToList());
        }

        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var muscleGroup = _context.Set<MuscleGroup>().Where(e => e.Id == id).Select(e => new MuscleGroupViewModel
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description,
            }).FirstOrDefault();
            if (muscleGroup == null)
            {
                return NotFound();
            }

            return View(muscleGroup);
        }

        public IActionResult Create(Guid id)
        {
            return View(new MuscleGroupViewModel
            {
                ExerciseId = id,
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MuscleGroupViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var muscleGroup = new MuscleGroup
                {
                    Name = vm.Name,
                    Description = vm.Description,
                    CreateBy = _context.Set<User>().Find(GetUserId()),
                    CreateDate = DateTime.Now
                };

                _context.Set<MuscleGroup>().Add(muscleGroup);

                if (vm.ExerciseId != null && vm.ExerciseId != Guid.Empty)
                    _context.Set<ExerciseMuscleGroup>().Add(new ExerciseMuscleGroup
                    {
                        MuscleGroup = muscleGroup,
                        Exercise = _context.Set<Exercise>().Find(vm.ExerciseId),
                    });

                _context.SaveChanges();
                if (vm.ExerciseId != null && vm.ExerciseId != Guid.Empty)
                    return RedirectToAction("Edit", "Exercise", new { id = vm.ExerciseId });
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

            var muscleGroup =  _context.Set<MuscleGroup>().Where(e => e.Id == id).Select(e => new MuscleGroupViewModel
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description,
            }).FirstOrDefault();
            if (muscleGroup == null)
            {
                return NotFound();
            }
            return View(muscleGroup);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(MuscleGroupViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var muscleGroup = _context.Set<MuscleGroup>().Find(vm.Id);
                    muscleGroup.Name = vm.Name;
                    muscleGroup.Description = vm.Description;
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EntityExists<MuscleGroup>(vm.Id))
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

            var muscleGroup = _context.Set<MuscleGroup>().Where(e => e.Id == id).Select(e => new MuscleGroupViewModel
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description,
            }).FirstOrDefault();
            if (muscleGroup == null)
            {
                return NotFound();
            }

            return View(muscleGroup);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Guid id)
        {
            var muscleGroup =  _context.Set<MuscleGroup>().Find(id);
            _context.Set<MuscleGroup>().Remove(muscleGroup);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
