using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Flashcard.Migrations
{
    public partial class InsertSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "user_id", "created_at", "created_by", "password", "updated_at", "updated_by", "user_name" },
                values: new object[,]
                {
                    { 1, null, null, "password1", null, null, "user1" },
                    { 2, null, null, "password2", null, null, "user2" }
                });

            migrationBuilder.InsertData(
                table: "words",
                columns: new[] { "word_id", "created_at", "created_by", "meaning", "updated_at", "updated_by", "user_id", "word" },
                values: new object[,]
                {
                    { 1, null, null, "meaning1", null, null, 1, "word1" },
                    { 2, null, null, "meaning2", null, null, 1, "word2" },
                    { 3, null, null, "meaning3", null, null, 1, "word3" },
                    { 4, null, null, "meaning4", null, null, 1, "word4" },
                    { 5, null, null, "meaning5", null, null, 1, "word5" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "user_id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "words",
                keyColumn: "word_id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "words",
                keyColumn: "word_id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "words",
                keyColumn: "word_id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "words",
                keyColumn: "word_id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "words",
                keyColumn: "word_id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "user_id",
                keyValue: 1);
        }
    }
}
