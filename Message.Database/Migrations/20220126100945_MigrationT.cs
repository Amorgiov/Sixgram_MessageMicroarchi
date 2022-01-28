using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Message.Database.Migrations
{
    public partial class MigrationT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Timestamp",
                table: "Messages",
                newName: "Created");

            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "Messages",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "Messages",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Chats",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Chats",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "Chats",
                type: "timestamp without time zone",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Text",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "Chats");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "Messages",
                newName: "Timestamp");
        }
    }
}
