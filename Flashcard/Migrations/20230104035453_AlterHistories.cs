using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Flashcard.Migrations
{
    public partial class AlterHistories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "histories",
                keyColumn: "history_id",
                keyValue: 6,
                column: "stydy_count",
                value: 5);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "histories",
                keyColumn: "history_id",
                keyValue: 6,
                column: "stydy_count",
                value: 10);
        }
    }
}
