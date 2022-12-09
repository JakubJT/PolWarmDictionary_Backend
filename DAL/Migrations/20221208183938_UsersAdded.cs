using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class UsersAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserWordGroup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "User");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.AddColumn<string>(
                name: "UserADId",
                table: "WordGroup",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserADId",
                table: "Users",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Users_UserADId",
                table: "Users",
                column: "UserADId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WordGroup_UserADId",
                table: "WordGroup",
                column: "UserADId");

            migrationBuilder.AddForeignKey(
                name: "FK_WordGroup_Users_UserADId",
                table: "WordGroup",
                column: "UserADId",
                principalTable: "Users",
                principalColumn: "UserADId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WordGroup_Users_UserADId",
                table: "WordGroup");

            migrationBuilder.DropIndex(
                name: "IX_WordGroup_UserADId",
                table: "WordGroup");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Users_UserADId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserADId",
                table: "WordGroup");

            migrationBuilder.DropColumn(
                name: "UserADId",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "UserId");

            migrationBuilder.CreateTable(
                name: "UserWordGroup",
                columns: table => new
                {
                    UsersUserId = table.Column<int>(type: "int", nullable: false),
                    WordGroupsWordGroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWordGroup", x => new { x.UsersUserId, x.WordGroupsWordGroupId });
                    table.ForeignKey(
                        name: "FK_UserWordGroup_User_UsersUserId",
                        column: x => x.UsersUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserWordGroup_WordGroup_WordGroupsWordGroupId",
                        column: x => x.WordGroupsWordGroupId,
                        principalTable: "WordGroup",
                        principalColumn: "WordGroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserWordGroup_WordGroupsWordGroupId",
                table: "UserWordGroup",
                column: "WordGroupsWordGroupId");
        }
    }
}
