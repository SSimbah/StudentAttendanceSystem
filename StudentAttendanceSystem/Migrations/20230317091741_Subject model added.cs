using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentAttendanceSystem.Migrations
{
    /// <inheritdoc />
    public partial class Subjectmodeladded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ClassSubject",
                table: "ClassModels",
                newName: "SubjectName");

            migrationBuilder.AddColumn<string>(
                name: "ClassName",
                table: "ClassModels",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SubjectCode",
                table: "ClassModels",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "SubjectID",
                table: "ClassModels",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    SubjectID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubjectDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.SubjectID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClassModels_SubjectID",
                table: "ClassModels",
                column: "SubjectID");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassModels_Subjects_SubjectID",
                table: "ClassModels",
                column: "SubjectID",
                principalTable: "Subjects",
                principalColumn: "SubjectID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassModels_Subjects_SubjectID",
                table: "ClassModels");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_ClassModels_SubjectID",
                table: "ClassModels");

            migrationBuilder.DropColumn(
                name: "ClassName",
                table: "ClassModels");

            migrationBuilder.DropColumn(
                name: "SubjectCode",
                table: "ClassModels");

            migrationBuilder.DropColumn(
                name: "SubjectID",
                table: "ClassModels");

            migrationBuilder.RenameColumn(
                name: "SubjectName",
                table: "ClassModels",
                newName: "ClassSubject");
        }
    }
}
