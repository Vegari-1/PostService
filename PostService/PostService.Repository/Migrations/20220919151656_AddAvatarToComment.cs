using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PostService.Repository.Migrations
{
    public partial class AddAvatarToComment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Images_ImageId",
                schema: "post",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_ImageId",
                schema: "post",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "ImageId",
                schema: "post",
                table: "Comments");

            migrationBuilder.AlterColumn<string>(
                name: "Avatar",
                schema: "post",
                table: "Profiles",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                schema: "post",
                table: "Comments",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeStamp",
                schema: "post",
                table: "Comments",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Surname",
                schema: "post",
                table: "Comments",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "post",
                table: "Comments",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Avatar",
                schema: "post",
                table: "Comments",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Avatar",
                schema: "post",
                table: "Comments");

            migrationBuilder.AlterColumn<string>(
                name: "Avatar",
                schema: "post",
                table: "Profiles",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                schema: "post",
                table: "Comments",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeStamp",
                schema: "post",
                table: "Comments",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<string>(
                name: "Surname",
                schema: "post",
                table: "Comments",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "post",
                table: "Comments",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<Guid>(
                name: "ImageId",
                schema: "post",
                table: "Comments",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ImageId",
                schema: "post",
                table: "Comments",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Images_ImageId",
                schema: "post",
                table: "Comments",
                column: "ImageId",
                principalSchema: "post",
                principalTable: "Images",
                principalColumn: "Id");
        }
    }
}
