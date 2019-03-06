using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkoutWallet.Models
{
    public class Workout : BaseAuditableEntity
    {
        public ICollection<Set> Exercises { get; set; }
    }
}
