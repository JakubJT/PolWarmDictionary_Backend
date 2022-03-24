using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class AddingUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

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
                        name: "FK_UserWordGroup_WordGroups_WordGroupsWordGroupId",
                        column: x => x.WordGroupsWordGroupId,
                        principalTable: "WordGroups",
                        principalColumn: "WordGroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserWordGroup_WordGroupsWordGroupId",
                table: "UserWordGroup",
                column: "WordGroupsWordGroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserWordGroup");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
