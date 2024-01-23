using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskHubAPI.Migrations
{
    public partial class AddAttributesAndRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Users_CreatedByUserUserId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Projects_ProjectIdProject",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Projects_ProjectIdProject",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ProjectIdProject",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_ProjectIdProject",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Projects_CreatedByUserUserId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ProjectIdProject",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ProjectIdProject",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "CreatedByUserUserId",
                table: "Projects");

            migrationBuilder.RenameColumn(
                name: "IdUserCreated",
                table: "Projects",
                newName: "UserId");

            migrationBuilder.AddColumn<int>(
                name: "IdProject",
                table: "Tasks",
                type: "integer",
                nullable: false,
                defaultValue: 0);

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
                name: "IX_Tasks_IdProject",
                table: "Tasks",
                column: "IdProject");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectUser_UsersUserId",
                table: "ProjectUser",
                column: "UsersUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Projects_IdProject",
                table: "Tasks",
                column: "IdProject",
                principalTable: "Projects",
                principalColumn: "IdProject",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Projects_IdProject",
                table: "Tasks");

            migrationBuilder.DropTable(
                name: "ProjectUser");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_IdProject",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "IdProject",
                table: "Tasks");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Projects",
                newName: "IdUserCreated");

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

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserUserId",
                table: "Projects",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_ProjectIdProject",
                table: "Users",
                column: "ProjectIdProject");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_ProjectIdProject",
                table: "Tasks",
                column: "ProjectIdProject");

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
    }
}
