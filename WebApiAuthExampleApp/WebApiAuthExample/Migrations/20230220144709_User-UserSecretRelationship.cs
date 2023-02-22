using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiAuthExample.Migrations
{
    /// <inheritdoc />
    public partial class UserUserSecretRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSecretModel_Users_UserId",
                table: "UserSecretModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserSecretModel",
                table: "UserSecretModel");

            migrationBuilder.RenameTable(
                name: "UserSecretModel",
                newName: "UserSercrets");

            migrationBuilder.RenameIndex(
                name: "IX_UserSecretModel_UserId",
                table: "UserSercrets",
                newName: "IX_UserSercrets_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserSercrets",
                table: "UserSercrets",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSercrets_Users_UserId",
                table: "UserSercrets",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSercrets_Users_UserId",
                table: "UserSercrets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserSercrets",
                table: "UserSercrets");

            migrationBuilder.RenameTable(
                name: "UserSercrets",
                newName: "UserSecretModel");

            migrationBuilder.RenameIndex(
                name: "IX_UserSercrets_UserId",
                table: "UserSecretModel",
                newName: "IX_UserSecretModel_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserSecretModel",
                table: "UserSecretModel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSecretModel_Users_UserId",
                table: "UserSecretModel",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
