using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyERP.Migrations
{
    public partial class init_db : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChkResult",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    CompanyId = table.Column<int>(nullable: false),
                    ChkPointId = table.Column<int>(nullable: false),
                    PoluTypeId = table.Column<int>(nullable: false),
                    ChkDate = table.Column<DateTime>(nullable: false),
                    ChkBatch = table.Column<string>(nullable: true),
                    Concentration = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChkResult", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompanyInfo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Contact = table.Column<string>(nullable: true),
                    Tel = table.Column<string>(nullable: true),
                    LicenseFile = table.Column<string>(nullable: true),
                    WaterTypeID = table.Column<int>(nullable: false),
                    WaterAmount = table.Column<int>(nullable: false),
                    CraftFile = table.Column<string>(nullable: true),
                    CraftDes = table.Column<string>(nullable: true),
                    PipeFile = table.Column<string>(nullable: true),
                    IssSheetFile = table.Column<string>(nullable: true),
                    EnvCompany = table.Column<string>(nullable: true),
                    RiskBand = table.Column<int>(nullable: false),
                    DischargeDate = table.Column<DateTime>(nullable: false),
                    MonthResTimes = table.Column<int>(nullable: false),
                    MonthResAmount = table.Column<decimal>(nullable: false),
                    MonthTotalAmount = table.Column<decimal>(nullable: false),
                    EmissionTypeID = table.Column<int>(nullable: false),
                    CollTypeID = table.Column<int>(nullable: false),
                    CheckPointIdList = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompanyMedcineType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    CompanyId = table.Column<int>(nullable: false),
                    MedTypeId = table.Column<int>(nullable: false),
                    MonthAmmount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyMedcineType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompanyPoluType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    CompanyId = table.Column<int>(nullable: false),
                    PoluTypeId = table.Column<int>(nullable: false),
                    Unit = table.Column<string>(nullable: true),
                    EmissionStdType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyPoluType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dic",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    ParentId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Category = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    OrderId = table.Column<int>(nullable: false),
                    DicTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dic", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DicType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    TypeName = table.Column<string>(nullable: true),
                    OrderId = table.Column<int>(nullable: false),
                    ConfigJson = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DicType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MeaureMethod",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    BaseInfo = table.Column<string>(nullable: true),
                    Steps = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeaureMethod", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reagent",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Batch = table.Column<string>(nullable: true),
                    EffectiveDate = table.Column<DateTime>(nullable: false),
                    Qualitity = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reagent", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChkResult");

            migrationBuilder.DropTable(
                name: "CompanyInfo");

            migrationBuilder.DropTable(
                name: "CompanyMedcineType");

            migrationBuilder.DropTable(
                name: "CompanyPoluType");

            migrationBuilder.DropTable(
                name: "Dic");

            migrationBuilder.DropTable(
                name: "DicType");

            migrationBuilder.DropTable(
                name: "MeaureMethod");

            migrationBuilder.DropTable(
                name: "Reagent");

            migrationBuilder.CreateTable(
                name: "MedType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    MedName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedType", x => x.Id);
                });
        }
    }
}
