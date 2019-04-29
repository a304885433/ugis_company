using Microsoft.EntityFrameworkCore.Migrations;

namespace MyERP.Migrations
{
    public partial class Company_ChkPointIdList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Dic");

            migrationBuilder.RenameColumn(
                name: "CheckPointIdList",
                table: "CompanyInfo",
                newName: "ChkPointIdList");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ChkPointIdList",
                table: "CompanyInfo",
                newName: "CheckPointIdList");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Dic",
                nullable: true);
        }
    }
}
