using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Common;

namespace Web.Models
{
    public class MuscleGroup : BaseAuditableNamedEntity
    {
        public ICollection<ExerciseMuscleGroup> ExerciseMuscleGroups { get; set; }
    }
}
