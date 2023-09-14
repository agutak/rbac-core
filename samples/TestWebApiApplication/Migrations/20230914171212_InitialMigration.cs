using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AHutak.TestWebApiApplication.Migrations;

/// <inheritdoc />
public partial class InitialMigration : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "rbac");

        migrationBuilder.CreateTable(
            name: "Permissions",
            schema: "rbac",
            columns: table => new
            {
                _id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Permissions", x => x._id);
            });

        migrationBuilder.CreateTable(
            name: "Roles",
            schema: "rbac",
            columns: table => new
            {
                _id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Roles", x => x._id);
                table.UniqueConstraint("AK_Roles_Id", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "PermissionAssignments",
            schema: "rbac",
            columns: table => new
            {
                _id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                PermissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PermissionAssignments", x => x._id);
                table.ForeignKey(
                    name: "FK_PermissionAssignments_Roles_RoleId",
                    column: x => x.RoleId,
                    principalSchema: "rbac",
                    principalTable: "Roles",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "UserAssignments",
            schema: "rbac",
            columns: table => new
            {
                _id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_UserAssignments", x => x._id);
                table.ForeignKey(
                    name: "FK_UserAssignments_Roles_RoleId",
                    column: x => x.RoleId,
                    principalSchema: "rbac",
                    principalTable: "Roles",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_PermissionAssignments_RoleId",
            schema: "rbac",
            table: "PermissionAssignments",
            column: "RoleId");

        migrationBuilder.CreateIndex(
            name: "IX_Permissions_Id",
            schema: "rbac",
            table: "Permissions",
            column: "Id",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_Roles_Id",
            schema: "rbac",
            table: "Roles",
            column: "Id",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_UserAssignments_RoleId",
            schema: "rbac",
            table: "UserAssignments",
            column: "RoleId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "PermissionAssignments",
            schema: "rbac");

        migrationBuilder.DropTable(
            name: "Permissions",
            schema: "rbac");

        migrationBuilder.DropTable(
            name: "UserAssignments",
            schema: "rbac");

        migrationBuilder.DropTable(
            name: "Roles",
            schema: "rbac");
    }
}
