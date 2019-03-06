using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkoutWallet.Models
{
    public class Exercise : BaseNamedEntity
    {
        public Routine Routine { get; set; }
        public ICollection<Set> Sets { get; set; }
        [Required]
        public string Directions { get; set; }
    }
}
