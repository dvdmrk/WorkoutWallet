using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Common;

namespace Web.ViewModels.RoutineViewModels
{
    public class ExerciseIndexPartialViewModel : BaseNamedEntity
    {
        public int? RecommendedPercentOfMax { get; set; }
        public int? RecommendedNumberOfReps { get; set; }
        public int? OrderInRoutine { get; set; }
        public bool TillFailure { get; set; }
    }
}
