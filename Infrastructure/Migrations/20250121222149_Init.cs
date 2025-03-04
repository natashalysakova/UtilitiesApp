using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations;

// <auto=generated />
[SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1413:UseTrailingCommasInMultiLineInitializers", Justification = "Reviewed.")]

/// <inheritdoc />
public partial class Init : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterDatabase()
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
            name: "Homes",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                Name = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                Country = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                City = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                Region = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                Street = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                Building = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                Apartment = table.Column<string>(type: "longtext", nullable: true)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                Area = table.Column<double>(type: "double", nullable: false),
                IsDefault = table.Column<bool>(type: "tinyint(1)", nullable: false),
                IsArchived = table.Column<bool>(type: "tinyint(1)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Homes", x => x.Id);
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
            name: "UtilityTypes",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                Name = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                IsArchived = table.Column<bool>(type: "tinyint(1)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_UtilityTypes", x => x.Id);
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
            name: "Checks",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                Amount = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                HomeId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                IsArchived = table.Column<bool>(type: "tinyint(1)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Checks", x => x.Id);
                table.ForeignKey(
                    name: "FK_Checks_Homes_HomeId",
                    column: x => x.HomeId,
                    principalTable: "Homes",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
            name: "Tariffs",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                StartDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                EndDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                Order = table.Column<int>(type: "int", nullable: false),
                Cost = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                TariffType = table.Column<int>(type: "int", nullable: false),
                Units = table.Column<string>(type: "longtext", nullable: true)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                UseLimits = table.Column<bool>(type: "tinyint(1)", nullable: false),
                UseAdditionalFee = table.Column<bool>(type: "tinyint(1)", nullable: false),
                AdditionalFeeName = table.Column<string>(type: "longtext", nullable: true)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                AdditionalFeeCost = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                UseFixedPay = table.Column<bool>(type: "tinyint(1)", nullable: false),
                FixedPayName = table.Column<string>(type: "longtext", nullable: true)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                FixedPay = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                HomeId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                TypeId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                IsArchived = table.Column<bool>(type: "tinyint(1)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Tariffs", x => x.Id);
                table.ForeignKey(
                    name: "FK_Tariffs_Homes_HomeId",
                    column: x => x.HomeId,
                    principalTable: "Homes",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Tariffs_UtilityTypes_TypeId",
                    column: x => x.TypeId,
                    principalTable: "UtilityTypes",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
            name: "Limits",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                TariffId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                Limit = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                CostAfterLimit = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Limits", x => x.Id);
                table.ForeignKey(
                    name: "FK_Limits_Tariffs_TariffId",
                    column: x => x.TariffId,
                    principalTable: "Tariffs",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
            name: "Records",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                Measure = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                PreviousMeasure = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                Cost = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                CheckId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                TariffId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Records", x => x.Id);
                table.ForeignKey(
                    name: "FK_Records_Checks_CheckId",
                    column: x => x.CheckId,
                    principalTable: "Checks",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Records_Tariffs_TariffId",
                    column: x => x.TariffId,
                    principalTable: "Tariffs",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateIndex(
            name: "IX_Checks_HomeId",
            table: "Checks",
            column: "HomeId");

        migrationBuilder.CreateIndex(
            name: "IX_Limits_TariffId_Limit",
            table: "Limits",
            columns: ["TariffId", "Limit"],
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_Records_CheckId",
            table: "Records",
            column: "CheckId");

        migrationBuilder.CreateIndex(
            name: "IX_Records_TariffId",
            table: "Records",
            column: "TariffId");

        migrationBuilder.CreateIndex(
            name: "IX_Tariffs_HomeId",
            table: "Tariffs",
            column: "HomeId");

        migrationBuilder.CreateIndex(
            name: "IX_Tariffs_TypeId",
            table: "Tariffs",
            column: "TypeId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Limits");

        migrationBuilder.DropTable(
            name: "Records");

        migrationBuilder.DropTable(
            name: "Checks");

        migrationBuilder.DropTable(
            name: "Tariffs");

        migrationBuilder.DropTable(
            name: "Homes");

        migrationBuilder.DropTable(
            name: "UtilityTypes");
    }
}
