using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Common;

namespace Web.Models
{
    public class Routine : BaseAuditableNamedEntity
    {
        public ICollection<ExerciseRoutine> ExerciseRoutines { get; set; }
        public ICollection<RoutineProfile> RoutineProfiles { get; set; }
        public ICollection<Workout> Workouts { get; set; }
        public string VideoUrl { get; set; }
    }
}
