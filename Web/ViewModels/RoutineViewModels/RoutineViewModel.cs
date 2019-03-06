using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Web.Common;

namespace Web.ViewModels.RoutineViewModels
{
    public class RoutineViewModel : BaseNamedEntity
    {
        public Guid CreatedById { get; set; }
        [Display(Name="Created By")]
        public string CreatedByName { get; set; }
        [Display(Name = "Created On")]
        public DateTime CreatedDate { get; set; }
        public string VideoUrl { get; set; }
        public List<DropdownViewModel> Exercises { get; set; }
        public List<DropdownViewModel> Profiles { get; set; }
        public List<DropdownViewModel> Workouts { get; set; }
    }
}
