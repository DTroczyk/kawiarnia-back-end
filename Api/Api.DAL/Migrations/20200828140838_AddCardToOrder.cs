using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.DAL.Migrations
{
    public partial class AddCardToOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PaymentCard",
                table: "Orders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentCard",
                table: "Orders");
        }
    }
}
