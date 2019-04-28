using Microsoft.EntityFrameworkCore.Migrations;

namespace MyERP.Migrations
{
    public partial class dic_rename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DictTypeCode",
                table: "Dic");

            migrationBuilder.AddColumn<string>(
                name: "TypeCode",
                table: "Dic",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TypeCode",
                table: "Dic");

            migrationBuilder.AddColumn<int>(
                name: "DictTypeCode",
                table: "Dic",
                nullable: false,
                defaultValue: 0);
        }
    }
}
