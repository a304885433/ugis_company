using Microsoft.EntityFrameworkCore.Migrations;

namespace MyERP.Migrations
{
    public partial class CompanyContaminants : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContaminantsId",
                table: "CompanyInfo");

            migrationBuilder.DropColumn(
                name: "TransferTotal",
                table: "CompanyInfo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContaminantsId",
                table: "CompanyInfo",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "TransferTotal",
                table: "CompanyInfo",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
