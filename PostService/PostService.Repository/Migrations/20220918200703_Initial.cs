using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PostService.Repository.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "post");

            migrationBuilder.CreateTable(
                name: "Connections",
                schema: "post",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Profile1 = table.Column<Guid>(type: "uuid", nullable: false),
                    Profile2 = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Connections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                schema: "post",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: false),
                    CommentsNumber = table.Column<int>(type: "integer", nullable: true),
                    LikesNumber = table.Column<int>(type: "integer", nullable: true),
                    DislikesNumber = table.Column<int>(type: "integer", nullable: true),
                    Likes = table.Column<List<Guid>>(type: "uuid[]", nullable: true),
                    Dislikes = table.Column<List<Guid>>(type: "uuid[]", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Profiles",
                schema: "post",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Public = table.Column<bool>(type: "boolean", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Surname = table.Column<string>(type: "text", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    ImageId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reactions",
                schema: "post",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Positive = table.Column<bool>(type: "boolean", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: false),
                    PostId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reactions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                schema: "post",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    PostId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_Posts_PostId",
                        column: x => x.PostId,
                        principalSchema: "post",
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                schema: "post",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: false),
                    PostId = table.Column<Guid>(type: "uuid", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Surname = table.Column<string>(type: "text", nullable: true),
                    Username = table.Column<string>(type: "text", nullable: true),
                    ImageId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Images_ImageId",
                        column: x => x.ImageId,
                        principalSchema: "post",
                        principalTable: "Images",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comments_Posts_PostId",
                        column: x => x.PostId,
                        principalSchema: "post",
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ImageId",
                schema: "post",
                table: "Comments",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PostId",
                schema: "post",
                table: "Comments",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_PostId",
                schema: "post",
                table: "Images",
                column: "PostId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments",
                schema: "post");

            migrationBuilder.DropTable(
                name: "Connections",
                schema: "post");

            migrationBuilder.DropTable(
                name: "Profiles",
                schema: "post");

            migrationBuilder.DropTable(
                name: "Reactions",
                schema: "post");

            migrationBuilder.DropTable(
                name: "Images",
                schema: "post");

            migrationBuilder.DropTable(
                name: "Posts",
                schema: "post");
        }
    }
}
