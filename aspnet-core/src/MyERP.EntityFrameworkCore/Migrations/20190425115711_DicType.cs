using Microsoft.EntityFrameworkCore.Migrations;

namespace MyERP.Migrations
{
    public partial class DicType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ConfigJson",
                table: "DicType",
                newName: "TypeCode");

            migrationBuilder.RenameColumn(
                name: "DicTypeId",
                table: "Dic",
                newName: "DictTypeCode");

            migrationBuilder.AddColumn<string>(
                name: "ExtensionData",
                table: "DicType",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ParentTypeCode",
                table: "DicType",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExtensionData",
                table: "DicType");

            migrationBuilder.DropColumn(
                name: "ParentTypeCode",
                table: "DicType");

            migrationBuilder.RenameColumn(
                name: "TypeCode",
                table: "DicType",
                newName: "ConfigJson");

            migrationBuilder.RenameColumn(
                name: "DictTypeCode",
                table: "Dic",
                newName: "DicTypeId");
        }
    }
}
