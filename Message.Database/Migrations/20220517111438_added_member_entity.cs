using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Message.Database.Migrations
{
    public partial class added_member_entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Members",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "Messages",
                table: "Chats");

            migrationBuilder.AlterColumn<Guid>(
                name: "SenderId",
                table: "Messages",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<Guid>(
                name: "FileId",
                table: "Messages",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MemberEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ChatId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MemberEntity_Chats_ChatId",
                        column: x => x.ChatId,
                        principalTable: "Chats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MemberEntity_ChatId",
                table: "MemberEntity",
                column: "ChatId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MemberEntity");

            migrationBuilder.DropColumn(
                name: "FileId",
                table: "Messages");

            migrationBuilder.AlterColumn<Guid>(
                name: "SenderId",
                table: "Messages",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<List<Guid>>(
                name: "Members",
                table: "Chats",
                type: "uuid[]",
                nullable: true);

            migrationBuilder.AddColumn<List<Guid>>(
                name: "Messages",
                table: "Chats",
                type: "uuid[]",
                nullable: true);
        }
    }
}
