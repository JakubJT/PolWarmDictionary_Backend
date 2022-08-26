using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    AuthorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.AuthorId);
                });

            migrationBuilder.CreateTable(
                name: "PartOfSpeeches",
                columns: table => new
                {
                    PartOfSpeechId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartOfSpeeches", x => x.PartOfSpeechId);
                });

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
                name: "WordGroups",
                columns: table => new
                {
                    WordGroupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordGroups", x => x.WordGroupId);
                });

            migrationBuilder.CreateTable(
                name: "Words",
                columns: table => new
                {
                    WordId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InWarmian = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InPolish = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuthorId = table.Column<int>(type: "int", nullable: false),
                    PartOfSpeechId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Words", x => x.WordId);
                    table.ForeignKey(
                        name: "FK_Words_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "AuthorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Words_PartOfSpeeches_PartOfSpeechId",
                        column: x => x.PartOfSpeechId,
                        principalTable: "PartOfSpeeches",
                        principalColumn: "PartOfSpeechId");
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

            migrationBuilder.CreateTable(
                name: "WordWordGroup",
                columns: table => new
                {
                    WordGroupsWordGroupId = table.Column<int>(type: "int", nullable: false),
                    WordsWordId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordWordGroup", x => new { x.WordGroupsWordGroupId, x.WordsWordId });
                    table.ForeignKey(
                        name: "FK_WordWordGroup_WordGroups_WordGroupsWordGroupId",
                        column: x => x.WordGroupsWordGroupId,
                        principalTable: "WordGroups",
                        principalColumn: "WordGroupId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WordWordGroup_Words_WordsWordId",
                        column: x => x.WordsWordId,
                        principalTable: "Words",
                        principalColumn: "WordId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserWordGroup_WordGroupsWordGroupId",
                table: "UserWordGroup",
                column: "WordGroupsWordGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Words_AuthorId",
                table: "Words",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Words_PartOfSpeechId",
                table: "Words",
                column: "PartOfSpeechId");

            migrationBuilder.CreateIndex(
                name: "IX_WordWordGroup_WordsWordId",
                table: "WordWordGroup",
                column: "WordsWordId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserWordGroup");

            migrationBuilder.DropTable(
                name: "WordWordGroup");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "WordGroups");

            migrationBuilder.DropTable(
                name: "Words");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "PartOfSpeeches");
        }
    }
}
