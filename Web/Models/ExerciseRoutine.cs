using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class ExerciseRoutine
    {
        public Guid ExerciseId { get; set; }
        public Guid RoutineId { get; set; }
        public Routine Routine { get; set; }
        public Exercise Exercise { get; set; }
    }
}
