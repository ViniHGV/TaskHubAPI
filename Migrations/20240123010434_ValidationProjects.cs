using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskHubAPI.Migrations
{
    public partial class ValidationProjects : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Projects",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserUserId",
                table: "Projects",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdUserCreated",
                table: "Projects",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_CreatedByUserUserId",
                table: "Projects",
                column: "CreatedByUserUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Users_CreatedByUserUserId",
                table: "Projects",
                column: "CreatedByUserUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Users_CreatedByUserUserId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_CreatedByUserUserId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "CreatedByUserUserId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "IdUserCreated",
                table: "Projects");

            migrationBuilder.AlterColumn<int>(
                name: "Title",
                table: "Projects",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
