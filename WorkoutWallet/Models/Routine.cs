using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkoutWallet.Models
{
    public class Routine : BaseNamedEntity
    {
        public ICollection<Exercise> Exercises { get; set; }
    }
}
