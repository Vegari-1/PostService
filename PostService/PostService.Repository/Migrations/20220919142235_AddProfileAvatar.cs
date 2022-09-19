using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PostService.Repository.Migrations
{
    public partial class AddProfileAvatar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageId",
                schema: "post",
                table: "Profiles");

            migrationBuilder.AddColumn<string>(
                name: "Avatar",
                schema: "post",
                table: "Profiles",
                type: "text",
                nullable: true,
                defaultValue: null);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Avatar",
                schema: "post",
                table: "Profiles");

            migrationBuilder.AddColumn<Guid>(
                name: "ImageId",
                schema: "post",
                table: "Profiles",
                type: "uuid",
                nullable: true);
        }
    }
}
