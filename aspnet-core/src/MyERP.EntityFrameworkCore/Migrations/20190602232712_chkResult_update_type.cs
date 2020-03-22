using Microsoft.EntityFrameworkCore.Migrations;

namespace MyERP.Migrations
{
    public partial class chkResult_update_type : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Concentration",
                table: "ChkResult",
                type: "decimal(18,3)",
                nullable: false,
                oldClrType: typeof(decimal));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Concentration",
                table: "ChkResult",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,3)");
        }
    }
}
