using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Flashcard.Migrations
{
    public partial class FixHistories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "stydy_count",
                table: "histories",
                newName: "study_count");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "study_count",
                table: "histories",
                newName: "stydy_count");
        }
    }
}
