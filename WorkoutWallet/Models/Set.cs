using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkoutWallet.Models
{
    public class Set : BaseEntity
    {
        public Exercise Exercise { get; set; }
        [Required]
        public int Reps { get; set; }
        [Required]
        public double Weight { get; set; }
    }
}
