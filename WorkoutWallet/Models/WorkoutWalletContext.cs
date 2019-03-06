using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkoutWallet.Models
{
    public class WorkoutWalletContext : DbContext
    {
        public WorkoutWalletContext(DbContextOptions<WorkoutWalletContext> options) : base(options)
        {

        }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<Set> Sets { get; set; }
        public DbSet<Routine> Routines { get; set; }
        public DbSet<Exercise> Exercises{ get; set; }
    }
}
