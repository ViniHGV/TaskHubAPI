using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace TaskHubAPI.Migrations
{
    public partial class CreateModelProject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProjectIdProject",
                table: "Users",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProjectIdProject",
                table: "Tasks",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    IdProject = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<int>(type: "integer", nullable: false),
                    DateOfCriation = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.IdProject);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_ProjectIdProject",
                table: "Users",
                column: "ProjectIdProject");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_ProjectIdProject",
                table: "Tasks",
                column: "ProjectIdProject");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Projects_ProjectIdProject",
                table: "Tasks",
                column: "ProjectIdProject",
                principalTable: "Projects",
                principalColumn: "IdProject",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Projects_ProjectIdProject",
                table: "Users",
                column: "ProjectIdProject",
                principalTable: "Projects",
                principalColumn: "IdProject",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Projects_ProjectIdProject",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Projects_ProjectIdProject",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Users_ProjectIdProject",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_ProjectIdProject",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "ProjectIdProject",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ProjectIdProject",
                table: "Tasks");
        }
    }
}
