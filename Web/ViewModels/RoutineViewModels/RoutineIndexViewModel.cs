using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Web.Common;

namespace Web.ViewModels.RoutineViewModels
{
    public class RoutineIndexViewModel : BaseNamedEntity
    {
        [Display(Name = "Created By")]
        public string CreatedByName { get; set; }
        public Guid CreatedById { get; set; }
        [Display(Name = "Created On")]
        public DateTime CreatedDate { get; set; }
        public bool HasVideo { get; set; }
        [Display(Name="Number of Exercises")]
        public int NumberOfExercises { get; set; }
        [Display(Name = "Number of Users")]
        public int NumberOfUsers { get; set; }
        [Display(Name = "Number of Workouts")]
        public int NumberOfWorkouts { get; set; }
    }
}
