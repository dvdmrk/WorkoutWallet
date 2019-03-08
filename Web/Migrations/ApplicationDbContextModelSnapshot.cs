﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Web.Data;

namespace Web.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<Guid>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<Guid>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<Guid>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Web.Models.Exercise", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("CreateById");

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<int>("ExerciseType");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<Guid?>("UpdateById");

                    b.Property<DateTime?>("UpdateDate");

                    b.Property<string>("VideoUrl");

                    b.HasKey("Id");

                    b.HasIndex("CreateById");

                    b.HasIndex("UpdateById");

                    b.ToTable("Exercises");
                });

            modelBuilder.Entity("Web.Models.ExerciseMuscleGroup", b =>
                {
                    b.Property<Guid>("ExerciseId");

                    b.Property<Guid>("MuscleGroupId");

                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.HasKey("ExerciseId", "MuscleGroupId");

                    b.HasAlternateKey("Id");

                    b.HasIndex("MuscleGroupId");

                    b.ToTable("ExerciseMuscleGroup");
                });

            modelBuilder.Entity("Web.Models.ExerciseRoutine", b =>
                {
                    b.Property<Guid>("ExerciseId");

                    b.Property<Guid>("RoutineId");

                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.HasKey("ExerciseId", "RoutineId");

                    b.HasAlternateKey("Id");

                    b.HasIndex("RoutineId");

                    b.ToTable("ExerciseRoutine");
                });

            modelBuilder.Entity("Web.Models.ExerciseRoutineDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("ExerciseRoutineExerciseId");

                    b.Property<Guid?>("ExerciseRoutineRoutineId");

                    b.Property<int?>("OrderInRoutine");

                    b.Property<int?>("RecommendedNumberOfReps");

                    b.Property<int?>("RecommendedPercentOfMax");

                    b.Property<bool>("TillFailure");

                    b.HasKey("Id");

                    b.HasIndex("ExerciseRoutineExerciseId", "ExerciseRoutineRoutineId");

                    b.ToTable("ExerciseRoutineDetail");
                });

            modelBuilder.Entity("Web.Models.ExerciseSet", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("CreateById");

                    b.Property<DateTime>("CreateDate");

                    b.Property<Guid?>("ExerciseId");

                    b.Property<int>("Repetitions");

                    b.Property<Guid?>("UpdateById");

                    b.Property<DateTime?>("UpdateDate");

                    b.Property<double>("Weight");

                    b.Property<Guid?>("WorkoutId");

                    b.HasKey("Id");

                    b.HasIndex("CreateById");

                    b.HasIndex("ExerciseId");

                    b.HasIndex("UpdateById");

                    b.HasIndex("WorkoutId");

                    b.ToTable("ExerciseSets");
                });

            modelBuilder.Entity("Web.Models.MuscleGroup", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("CreateById");

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<Guid?>("UpdateById");

                    b.Property<DateTime?>("UpdateDate");

                    b.HasKey("Id");

                    b.HasIndex("CreateById");

                    b.HasIndex("UpdateById");

                    b.ToTable("MuscleGroups");
                });

            modelBuilder.Entity("Web.Models.Profile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Height");

                    b.Property<string>("InstagramUrl");

                    b.Property<Guid>("UserId");

                    b.Property<string>("UserName");

                    b.Property<double>("Weight");

                    b.HasKey("Id");

                    b.ToTable("Profiles");
                });

            modelBuilder.Entity("Web.Models.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Web.Models.Routine", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("CreateById");

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<Guid?>("UpdateById");

                    b.Property<DateTime?>("UpdateDate");

                    b.Property<string>("VideoUrl");

                    b.HasKey("Id");

                    b.HasIndex("CreateById");

                    b.HasIndex("UpdateById");

                    b.ToTable("Routines");
                });

            modelBuilder.Entity("Web.Models.RoutineProfile", b =>
                {
                    b.Property<Guid>("RoutineId");

                    b.Property<Guid>("ProfileId");

                    b.HasKey("RoutineId", "ProfileId");

                    b.HasIndex("ProfileId");

                    b.ToTable("RoutineProfile");
                });

            modelBuilder.Entity("Web.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<Guid>("ProfileId");

                    b.Property<Guid?>("RoleId");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("ProfileId")
                        .IsUnique();

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Web.Models.Workout", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("CreateById");

                    b.Property<DateTime>("CreateDate");

                    b.Property<Guid?>("ProfileId");

                    b.Property<Guid?>("RoutineId");

                    b.Property<Guid?>("UpdateById");

                    b.Property<DateTime?>("UpdateDate");

                    b.HasKey("Id");

                    b.HasIndex("CreateById");

                    b.HasIndex("ProfileId");

                    b.HasIndex("RoutineId");

                    b.HasIndex("UpdateById");

                    b.ToTable("Workouts");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("Web.Models.Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("Web.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("Web.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("Web.Models.Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Web.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("Web.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Web.Models.Exercise", b =>
                {
                    b.HasOne("Web.Models.User", "CreateBy")
                        .WithMany()
                        .HasForeignKey("CreateById");

                    b.HasOne("Web.Models.User", "UpdateBy")
                        .WithMany()
                        .HasForeignKey("UpdateById");
                });

            modelBuilder.Entity("Web.Models.ExerciseMuscleGroup", b =>
                {
                    b.HasOne("Web.Models.Exercise", "Exercise")
                        .WithMany("ExerciseMuscleGroups")
                        .HasForeignKey("ExerciseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Web.Models.MuscleGroup", "MuscleGroup")
                        .WithMany("ExerciseMuscleGroups")
                        .HasForeignKey("MuscleGroupId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Web.Models.ExerciseRoutine", b =>
                {
                    b.HasOne("Web.Models.Exercise", "Exercise")
                        .WithMany("ExerciseRoutines")
                        .HasForeignKey("ExerciseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Web.Models.Routine", "Routine")
                        .WithMany("ExerciseRoutines")
                        .HasForeignKey("RoutineId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Web.Models.ExerciseRoutineDetail", b =>
                {
                    b.HasOne("Web.Models.ExerciseRoutine", "ExerciseRoutine")
                        .WithMany("ExerciseRoutineDetails")
                        .HasForeignKey("ExerciseRoutineExerciseId", "ExerciseRoutineRoutineId");
                });

            modelBuilder.Entity("Web.Models.ExerciseSet", b =>
                {
                    b.HasOne("Web.Models.User", "CreateBy")
                        .WithMany()
                        .HasForeignKey("CreateById");

                    b.HasOne("Web.Models.Exercise", "Exercise")
                        .WithMany("ExerciseSets")
                        .HasForeignKey("ExerciseId");

                    b.HasOne("Web.Models.User", "UpdateBy")
                        .WithMany()
                        .HasForeignKey("UpdateById");

                    b.HasOne("Web.Models.Workout", "Workout")
                        .WithMany("ExerciseSets")
                        .HasForeignKey("WorkoutId");
                });

            modelBuilder.Entity("Web.Models.MuscleGroup", b =>
                {
                    b.HasOne("Web.Models.User", "CreateBy")
                        .WithMany()
                        .HasForeignKey("CreateById");

                    b.HasOne("Web.Models.User", "UpdateBy")
                        .WithMany()
                        .HasForeignKey("UpdateById");
                });

            modelBuilder.Entity("Web.Models.Routine", b =>
                {
                    b.HasOne("Web.Models.User", "CreateBy")
                        .WithMany()
                        .HasForeignKey("CreateById");

                    b.HasOne("Web.Models.User", "UpdateBy")
                        .WithMany()
                        .HasForeignKey("UpdateById");
                });

            modelBuilder.Entity("Web.Models.RoutineProfile", b =>
                {
                    b.HasOne("Web.Models.Profile", "Profile")
                        .WithMany("RoutineProfiles")
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Web.Models.Routine", "Routine")
                        .WithMany("RoutineProfiles")
                        .HasForeignKey("RoutineId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Web.Models.User", b =>
                {
                    b.HasOne("Web.Models.Profile", "Profile")
                        .WithOne("User")
                        .HasForeignKey("Web.Models.User", "ProfileId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Web.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId");
                });

            modelBuilder.Entity("Web.Models.Workout", b =>
                {
                    b.HasOne("Web.Models.User", "CreateBy")
                        .WithMany()
                        .HasForeignKey("CreateById");

                    b.HasOne("Web.Models.Profile", "Profile")
                        .WithMany("Workouts")
                        .HasForeignKey("ProfileId");

                    b.HasOne("Web.Models.Routine", "Routine")
                        .WithMany("Workouts")
                        .HasForeignKey("RoutineId");

                    b.HasOne("Web.Models.User", "UpdateBy")
                        .WithMany()
                        .HasForeignKey("UpdateById");
                });
#pragma warning restore 612, 618
        }
    }
}
