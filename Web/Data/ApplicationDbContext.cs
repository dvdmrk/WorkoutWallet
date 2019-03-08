using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Web.Models;
using Web.ViewModels;
using Web.ViewModels.RoutineViewModels;
using Web.ViewModels.ExerciseViewModels;
using Web.Common;

namespace Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //Generate Ids on Add
            builder.Entity<User>(b => { b.Property(u => u.Id).ValueGeneratedOnAdd(); });
            builder.Entity<Role>(b => { b.Property(u => u.Id).ValueGeneratedOnAdd(); });
            builder.Entity<Exercise>(b => { b.Property(u => u.Id).ValueGeneratedOnAdd(); });
            builder.Entity<ExerciseRoutine>(b => { b.Property(u => u.Id).ValueGeneratedOnAdd(); });
            builder.Entity<ExerciseSet>(b => { b.Property(u => u.Id).ValueGeneratedOnAdd(); });
            builder.Entity<MuscleGroup>(b => { b.Property(u => u.Id).ValueGeneratedOnAdd(); });
            builder.Entity<ExerciseMuscleGroup>(b => { b.Property(u => u.Id).ValueGeneratedOnAdd(); });
            builder.Entity<Profile>(b => { b.Property(u => u.Id).ValueGeneratedOnAdd(); });
            builder.Entity<Routine>(b => { b.Property(u => u.Id).ValueGeneratedOnAdd(); });
            builder.Entity<Workout>(b => { b.Property(u => u.Id).ValueGeneratedOnAdd(); });
            builder.Entity<ExerciseRoutineDetail>(b => { b.Property(u => u.Id).ValueGeneratedOnAdd(); });

            //Many to Many
            builder.Entity<ExerciseMuscleGroup>().HasKey(emg => new { emg.ExerciseId, emg.MuscleGroupId });
            builder.Entity<ExerciseMuscleGroup>().HasOne(emg => emg.Exercise).WithMany(e => e.ExerciseMuscleGroups).HasForeignKey(emg => emg.ExerciseId);
            builder.Entity<ExerciseMuscleGroup>().HasOne(mg => mg.MuscleGroup).WithMany(emg => emg.ExerciseMuscleGroups).HasForeignKey(mg => mg.MuscleGroupId);
            builder.Entity<RoutineProfile>().HasKey(rp => new { rp.RoutineId, rp.ProfileId });
            builder.Entity<RoutineProfile>().HasOne(rp => rp.Routine).WithMany(r => r.RoutineProfiles).HasForeignKey(rp => rp.RoutineId);
            builder.Entity<RoutineProfile>().HasOne(rp => rp.Profile).WithMany(p => p.RoutineProfiles).HasForeignKey(rp => rp.ProfileId);
            builder.Entity<ExerciseMuscleGroup>().HasKey(emg => new { emg.ExerciseId, emg.MuscleGroupId });
            builder.Entity<ExerciseMuscleGroup>().HasOne(e => e.Exercise).WithMany(m => m.ExerciseMuscleGroups).HasForeignKey(emg => emg.ExerciseId);
            builder.Entity<ExerciseMuscleGroup>().HasOne(bc => bc.MuscleGroup).WithMany(c => c.ExerciseMuscleGroups).HasForeignKey(bc => bc.MuscleGroupId);
            builder.Entity<ExerciseRoutine>().HasKey(er => new { er.ExerciseId, er.RoutineId });
            builder.Entity<ExerciseRoutine>().HasOne(er => er.Exercise).WithMany(e => e.ExerciseRoutines).HasForeignKey(er => er.ExerciseId);
            builder.Entity<ExerciseRoutine>().HasOne(er => er.Routine).WithMany(r => r.ExerciseRoutines).HasForeignKey(er => er.RoutineId);
            //One to Many
            builder.Entity<ExerciseRoutine>().HasMany(c => c.ExerciseRoutineDetails).WithOne(e => e.ExerciseRoutine);
            builder.Entity<Exercise>().HasMany(c => c.ExerciseSets).WithOne(e => e.Exercise);
            builder.Entity<Profile>().HasMany(c => c.Workouts).WithOne(e => e.Profile);
            builder.Entity<Workout>().HasMany(c => c.ExerciseSets).WithOne(e => e.Workout);
            builder.Entity<Routine>().HasMany(c => c.Workouts).WithOne(e => e.Routine);
            //One to One
            builder.Entity<Profile>().HasOne(a => a.User).WithOne(b => b.Profile).HasForeignKey<User>(b => b.ProfileId);
        }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<ExerciseSet> ExerciseSets { get; set; }
        public DbSet<MuscleGroup> MuscleGroups { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Routine> Routines { get; set; }
        public DbSet<Workout> Workouts { get; set; }
    }
}
