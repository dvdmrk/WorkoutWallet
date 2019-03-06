using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Common;

namespace Web.Models
{
    public class Workout : BaseAuditableEntity
    {
        public Routine Routine { get; set; }
        public Profile Profile { get; set; }
        public ICollection<ExerciseSet> ExerciseSets { get; set; }
    }
}
