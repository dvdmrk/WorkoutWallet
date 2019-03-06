using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Common;

namespace Web.Models
{
    public class ExerciseSet : BaseAuditableEntity
    {
        public Exercise Exercise { get; set; }
        public Workout Workout { get; set; }
        public int Repetitions { get; set; }
        public double Weight { get; set; }
    }
}
