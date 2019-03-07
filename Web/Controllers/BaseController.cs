using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Common;
using Web.Data;

namespace Web.Controllers
{
    public class BaseController : Controller
    {
        private readonly ApplicationDbContext _context;
        public BaseController(ApplicationDbContext context)
        {
            _context = context;
        }
        public Guid GetUserId() => Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? Guid.Empty.ToString());
        public List<SelectListItem> GetDropdownViewModels<T>() where T : BaseAuditableNamedEntity
        {
            return _context.Set<T>().Select(e => new SelectListItem
            {
                Value = e.Id.ToString(),
                Text = e.Name
            }).ToList();
        }
        public void AddRelationship<T>(T obj) where T : class
        {
            _context.Set<T>().Add(obj);
            _context.SaveChanges();
        }
    }
}