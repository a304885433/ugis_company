using Microsoft.EntityFrameworkCore.Migrations;

namespace MyERP.Migrations
{
    public partial class CompanyFileAddCompangId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "CompanyFile",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "CompanyFile");
        }
    }
}
