using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkoutWallet.Models;

namespace WorkoutWallet.ViewModels
{
    public class ExerciseViewModel
    {
        public int RoutineId { get; set; }
        public string Name { get; set; }
        public string Directions { get; set; }
    }
}
