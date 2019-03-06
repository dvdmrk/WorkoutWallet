using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Models;
using Web.ViewModels;

namespace Web.Controllers
{
    [Authorize]
    public class ProfileController : BaseController
    {
        private readonly ApplicationDbContext _context;


        public ProfileController(ApplicationDbContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            ViewBag.CurrentUserId = GetUserId();
            return View(_context.Profiles.Select(e => new ProfileViewModel
            {
                Height = e.Height,
                Id = e.UserId,
                InstagramUrl = e.InstagramUrl,
                UserName = e.UserName,
                Weight = e.Weight
            }).ToList());
        }

        // GET: Profile/Details/5
        public IActionResult Details(Guid? id)
        {
            ViewBag.CurrentUserId = GetUserId();

            if (id == null) return NotFound();

            var user = _context.Set<User>().Find(id);
            if (user == null) return NotFound();

            var profile = _context.Set<Profile>().Find(user.ProfileId);
            if (profile == null) return NotFound();
            
            return View(new ProfileViewModel
            {
                Id = user.Id,
                UserName = profile.UserName,
                Height = profile.Height,
                Weight = profile.Weight,
                InstagramUrl = profile.InstagramUrl
            });
        }

        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profileViewModel = _context.Set<Profile>().Where(e => e.User.Id == id).Select(e => new ProfileViewModel
            {
                Id = e.User.Id,
                UserName = e.UserName,
                Height = e.Height,
                Feet = (int)Math.Floor(e.Height / 12),
                Weight = e.Weight,
                InstagramUrl = e.InstagramUrl
            }).FirstOrDefault();
            profileViewModel.Inches = (int)((decimal)profileViewModel.Height % 12);

            if (profileViewModel == null)
            {
                return NotFound();
            }
            return View(profileViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProfileViewModel profileViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var profile = _context.Set<Profile>().FirstOrDefault(e => e.User.Id == profileViewModel.Id);
                    profile.UserName = profileViewModel.UserName;
                    profile.Height = profileViewModel.HeightInInches;
                    profile.Weight = profileViewModel.Weight;
                    profile.InstagramUrl = profileViewModel.InstagramUrl;
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersProfileExists(profileViewModel.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(profileViewModel);
        }

        private bool UsersProfileExists(Guid id)
        {
            return _context.Users.Find(id).Profile != null;
        }
    }
}
