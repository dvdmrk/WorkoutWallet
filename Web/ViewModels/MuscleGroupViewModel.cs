using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Common;

namespace Web.ViewModels
{
    public class MuscleGroupViewModel : BaseNamedEntity
    {
        public Guid ExerciseId { get; set; }
    }
}
