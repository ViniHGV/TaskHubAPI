﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskHubAPI.Migrations
{
    public partial class FixingRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectUser");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Projects",
                newName: "IdUser");

            migrationBuilder.AddColumn<int>(
                name: "IdProject",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProjectsIdProject",
                table: "Users",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_ProjectsIdProject",
                table: "Users",
                column: "ProjectsIdProject");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Projects_ProjectsIdProject",
                table: "Users",
                column: "ProjectsIdProject",
                principalTable: "Projects",
                principalColumn: "IdProject",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Projects_ProjectsIdProject",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ProjectsIdProject",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IdProject",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ProjectsIdProject",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "IdUser",
                table: "Projects",
                newName: "UserId");

            migrationBuilder.CreateTable(
                name: "ProjectUser",
                columns: table => new
                {
                    ProjectsIdProject = table.Column<int>(type: "integer", nullable: false),
                    UsersUserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectUser", x => new { x.ProjectsIdProject, x.UsersUserId });
                    table.ForeignKey(
                        name: "FK_ProjectUser_Projects_ProjectsIdProject",
                        column: x => x.ProjectsIdProject,
                        principalTable: "Projects",
                        principalColumn: "IdProject",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectUser_Users_UsersUserId",
                        column: x => x.UsersUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectUser_UsersUserId",
                table: "ProjectUser",
                column: "UsersUserId");
        }
    }
}
