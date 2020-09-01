using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.DAL.Migrations
{
    public partial class DeleteUserBehavior : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_ClientId",
                table: "Orders");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_ClientId",
                table: "Orders",
                column: "ClientId",
                principalTable: "Users",
                principalColumn: "UserName",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_ClientId",
                table: "Orders");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_ClientId",
                table: "Orders",
                column: "ClientId",
                principalTable: "Users",
                principalColumn: "UserName",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
