using Microsoft.EntityFrameworkCore.Migrations;

namespace MyERP.Migrations
{
    public partial class companyEnvWuranwu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContaminantsId",
                table: "CompanyInfo",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "EnvDeadline",
                table: "CompanyInfo",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "EnvInsuredAmount",
                table: "CompanyInfo   ",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "FactWaterAmount",
                table: "CompanyInfo",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PlanWaterAmount",
                table: "CompanyInfo",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "PurchaseFile",
                table: "CompanyInfo",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TransferTotal",
                table: "CompanyInfo",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "WaterAmountBz",
                table: "CompanyInfo",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContaminantsId",
                table: "CompanyInfo");

            migrationBuilder.DropColumn(
                name: "EnvDeadline",
                table: "CompanyInfo");

            migrationBuilder.DropColumn(
                name: "EnvInsuredAmount",
                table: "CompanyInfo");

            migrationBuilder.DropColumn(
                name: "FactWaterAmount",
                table: "CompanyInfo");

            migrationBuilder.DropColumn(
                name: "PlanWaterAmount",
                table: "CompanyInfo");

            migrationBuilder.DropColumn(
                name: "PurchaseFile",
                table: "CompanyInfo");

            migrationBuilder.DropColumn(
                name: "TransferTotal",
                table: "CompanyInfo");

            migrationBuilder.DropColumn(
                name: "WaterAmountBz",
                table: "CompanyInfo");
        }
    }
}
