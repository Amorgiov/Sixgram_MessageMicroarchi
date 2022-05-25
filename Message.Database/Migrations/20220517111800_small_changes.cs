using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Message.Database.Migrations
{
    public partial class small_changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "MemberEntity",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "MemberEntity",
                type: "timestamp without time zone",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                table: "MemberEntity");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "MemberEntity");
        }
    }
}
