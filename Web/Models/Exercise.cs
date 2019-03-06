using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Common;
using Web.Types;

namespace Web.Models
{
    public class Exercise : BaseAuditableNamedEntity
    {
        public string VideoUrl { get; set; }
        public ExerciseType ExerciseType { get; set; }
        public ICollection<ExerciseMuscleGroup> ExerciseMuscleGroups { get; set; }
        public ICollection<ExerciseRoutine> ExerciseRoutines { get; set; }
        public ICollection<ExerciseSet> ExerciseSets { get; set; }
    }
}
