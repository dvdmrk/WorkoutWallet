using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class ExerciseMuscleGroup
    {
        public Guid ExerciseId { get; set; }
        public Exercise Exercise { get; set; }
        public MuscleGroup MuscleGroup { get; set; }
        public Guid MuscleGroupId { get; set; }
    }
}
