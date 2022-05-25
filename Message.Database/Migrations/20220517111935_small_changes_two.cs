using Microsoft.EntityFrameworkCore.Migrations;

namespace Message.Database.Migrations
{
    public partial class small_changes_two : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MemberEntity_Chats_ChatId",
                table: "MemberEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MemberEntity",
                table: "MemberEntity");

            migrationBuilder.RenameTable(
                name: "MemberEntity",
                newName: "Members");

            migrationBuilder.RenameIndex(
                name: "IX_MemberEntity_ChatId",
                table: "Members",
                newName: "IX_Members_ChatId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Members",
                table: "Members",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Chats_ChatId",
                table: "Members",
                column: "ChatId",
                principalTable: "Chats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_Chats_ChatId",
                table: "Members");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Members",
                table: "Members");

            migrationBuilder.RenameTable(
                name: "Members",
                newName: "MemberEntity");

            migrationBuilder.RenameIndex(
                name: "IX_Members_ChatId",
                table: "MemberEntity",
                newName: "IX_MemberEntity_ChatId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MemberEntity",
                table: "MemberEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MemberEntity_Chats_ChatId",
                table: "MemberEntity",
                column: "ChatId",
                principalTable: "Chats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
