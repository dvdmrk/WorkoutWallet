using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Common;

namespace Web.Models
{
    public class ExerciseRoutine : BaseEntity
    {
        public Guid ExerciseId { get; set; }
        public Guid RoutineId { get; set; }
        public Routine Routine { get; set; }
        public Exercise Exercise { get; set; }
        public ICollection<ExerciseRoutineDetail> ExerciseRoutineDetails { get; set; }

    }
}
