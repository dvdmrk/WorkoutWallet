using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Common;

namespace Web.Models
{
    public class ExerciseMuscleGroup : BaseEntity
    {
        public Guid ExerciseId { get; set; }
        public Exercise Exercise { get; set; }
        public MuscleGroup MuscleGroup { get; set; }
        public Guid MuscleGroupId { get; set; }
    }
}
