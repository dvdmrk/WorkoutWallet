using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Migrations
{
    public partial class AddedExerciseRoutineDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "ExerciseRoutine",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "ExerciseMuscleGroup",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddUniqueConstraint(
                name: "AK_ExerciseRoutine_Id",
                table: "ExerciseRoutine",
                column: "Id");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_ExerciseMuscleGroup_Id",
                table: "ExerciseMuscleGroup",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ExerciseRoutineDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExerciseRoutineExerciseId = table.Column<Guid>(nullable: true),
                    ExerciseRoutineRoutineId = table.Column<Guid>(nullable: true),
                    RecommendedPercentOfMax = table.Column<int>(nullable: true),
                    RecommendedNumberOfReps = table.Column<int>(nullable: true),
                    OrderInRoutine = table.Column<int>(nullable: true),
                    TillFailure = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseRoutineDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExerciseRoutineDetail_ExerciseRoutine_ExerciseRoutineExerciseId_ExerciseRoutineRoutineId",
                        columns: x => new { x.ExerciseRoutineExerciseId, x.ExerciseRoutineRoutineId },
                        principalTable: "ExerciseRoutine",
                        principalColumns: new[] { "ExerciseId", "RoutineId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseRoutineDetail_ExerciseRoutineExerciseId_ExerciseRoutineRoutineId",
                table: "ExerciseRoutineDetail",
                columns: new[] { "ExerciseRoutineExerciseId", "ExerciseRoutineRoutineId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExerciseRoutineDetail");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_ExerciseRoutine_Id",
                table: "ExerciseRoutine");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_ExerciseMuscleGroup_Id",
                table: "ExerciseMuscleGroup");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ExerciseRoutine");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ExerciseMuscleGroup");
        }
    }
}
