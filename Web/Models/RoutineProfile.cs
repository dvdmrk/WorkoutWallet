using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class RoutineProfile
    {
        public Guid ProfileId { get; set; }
        public Guid RoutineId { get; set; }
        public Routine Routine { get; set; }
        public Profile Profile { get; set; }
    }
}
