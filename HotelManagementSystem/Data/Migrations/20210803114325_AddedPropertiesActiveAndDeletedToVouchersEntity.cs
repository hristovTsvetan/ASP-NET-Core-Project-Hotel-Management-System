using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelManagementSystem.Data.Migrations
{
    public partial class AddedPropertiesActiveAndDeletedToVouchersEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Vouchers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Vouchers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "Vouchers");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Vouchers");
        }
    }
}
