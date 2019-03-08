using System.Collections.Generic;
using Web.Common;

namespace Web.Models
{
    public class ExerciseRoutineDetail : BaseEntity
    {
        public ExerciseRoutine ExerciseRoutine { get; set; }
        public int? RecommendedPercentOfMax { get; set; }
        public int? RecommendedNumberOfReps { get; set; }
        public int? OrderInRoutine { get; set; }
        public bool TillFailure { get; set; }
    }
}