using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OneHandTraining.Data
{
    public partial class InitialCreatt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "UserOldDBs",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    slug = table.Column<string>(type: "TEXT", nullable: true),
                    title = table.Column<string>(type: "TEXT", nullable: false),
                    description = table.Column<string>(type: "TEXT", nullable: false),
                    body = table.Column<string>(type: "TEXT", nullable: false),
                    createdAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    favorited = table.Column<bool>(type: "INTEGER", nullable: false),
                    favoritesCount = table.Column<int>(type: "INTEGER", nullable: false),
                    authorId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Articles_UserOldDBs_authorId",
                        column: x => x.authorId,
                        principalTable: "UserOldDBs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserOldUserOld",
                columns: table => new
                {
                    followersId = table.Column<int>(type: "INTEGER", nullable: false),
                    followingId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserOldUserOld", x => new { x.followersId, x.followingId });
                    table.ForeignKey(
                        name: "FK_UserOldUserOld_UserOldDBs_followersId",
                        column: x => x.followersId,
                        principalTable: "UserOldDBs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserOldUserOld_UserOldDBs_followingId",
                        column: x => x.followingId,
                        principalTable: "UserOldDBs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    createdAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    body = table.Column<string>(type: "TEXT", nullable: false),
                    authorId = table.Column<int>(type: "INTEGER", nullable: false),
                    ArticleId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comments_UserOldDBs_authorId",
                        column: x => x.authorId,
                        principalTable: "UserOldDBs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Articles_authorId",
                table: "Articles",
                column: "authorId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ArticleId",
                table: "Comments",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_authorId",
                table: "Comments",
                column: "authorId");

            migrationBuilder.CreateIndex(
                name: "IX_UserOldUserOld_followingId",
                table: "UserOldUserOld",
                column: "followingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "UserOldUserOld");

            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropColumn(
                name: "Token",
                table: "UserOldDBs");
        }
    }
}
