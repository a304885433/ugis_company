using Microsoft.EntityFrameworkCore.Migrations;

namespace MyERP.Migrations
{
    public partial class CompanyFile_UID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UId",
                table: "CompanyFile",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UId",
                table: "CompanyFile");
        }
    }
}
