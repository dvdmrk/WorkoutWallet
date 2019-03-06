using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Web.Common;
using Web.Types;

namespace Web.ViewModels.ExerciseViewModels
{
    public class ExerciseIndexViewModel : BaseNamedEntity
    {
        [Display(Name = "Created By")]
        public string CreatedByName { get; set; }
        public Guid CreatedById { get; set; }
        [Display(Name = "Created On")]
        public DateTime CreatedDate { get; set; }
        [Display(Name = "Video Available")]
        public bool HasVideo { get; set; }
        [Display(Name = "Exercise Type")]
        public ExerciseType ExerciseType { get; set; }
        [Display(Name = "Number of Routines")]
        public int NumberOfRoutines { get; set; }
        [Display(Name = "Number of sets Completed")]
        public int NumberOfExerciseSetsCompleted { get; set; }
    }
}
