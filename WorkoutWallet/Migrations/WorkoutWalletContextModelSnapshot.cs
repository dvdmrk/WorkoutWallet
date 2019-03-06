﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WorkoutWallet.Models;

namespace WorkoutWallet.Migrations
{
    [DbContext(typeof(WorkoutWalletContext))]
    partial class WorkoutWalletContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WorkoutWallet.Models.Exercise", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Directions")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int?>("RoutineId");

                    b.HasKey("Id");

                    b.HasIndex("RoutineId");

                    b.ToTable("Exercises");
                });

            modelBuilder.Entity("WorkoutWallet.Models.Routine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Routines");
                });

            modelBuilder.Entity("WorkoutWallet.Models.Set", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ExerciseId");

                    b.Property<int>("Reps");

                    b.Property<double>("Weight");

                    b.Property<int?>("WorkoutId");

                    b.HasKey("Id");

                    b.HasIndex("ExerciseId");

                    b.HasIndex("WorkoutId");

                    b.ToTable("Sets");
                });

            modelBuilder.Entity("WorkoutWallet.Models.Workout", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Finish");

                    b.Property<DateTime>("Start");

                    b.HasKey("Id");

                    b.ToTable("Workouts");
                });

            modelBuilder.Entity("WorkoutWallet.Models.Exercise", b =>
                {
                    b.HasOne("WorkoutWallet.Models.Routine", "Routine")
                        .WithMany("Exercises")
                        .HasForeignKey("RoutineId");
                });

            modelBuilder.Entity("WorkoutWallet.Models.Set", b =>
                {
                    b.HasOne("WorkoutWallet.Models.Exercise", "Exercise")
                        .WithMany("Sets")
                        .HasForeignKey("ExerciseId");

                    b.HasOne("WorkoutWallet.Models.Workout")
                        .WithMany("Exercises")
                        .HasForeignKey("WorkoutId");
                });
#pragma warning restore 612, 618
        }
    }
}