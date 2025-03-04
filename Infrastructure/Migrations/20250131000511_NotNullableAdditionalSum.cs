using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations;

/// <inheritdoc />
public partial class NotNullableAdditionalSum : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<decimal>(
            name: "AdditionalSum",
            table: "Records",
            type: "decimal(65,30)",
            nullable: false,
            defaultValue: 0m,
            oldClrType: typeof(decimal),
            oldType: "decimal(65,30)",
            oldNullable: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<decimal>(
            name: "AdditionalSum",
            table: "Records",
            type: "decimal(65,30)",
            nullable: true,
            oldClrType: typeof(decimal),
            oldType: "decimal(65,30)");
    }
}
