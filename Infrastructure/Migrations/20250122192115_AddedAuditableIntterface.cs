using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations;

/// <inheritdoc />
public partial class AddedAuditableIntterface : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<DateTime>(
            name: "CreatedAt",
            table: "UtilityTypes",
            type: "datetime(6)",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.AddColumn<DateTime>(
            name: "DeletedAt",
            table: "UtilityTypes",
            type: "datetime(6)",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.AddColumn<DateTime>(
            name: "ModifiedAt",
            table: "UtilityTypes",
            type: "datetime(6)",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.AddColumn<DateTime>(
            name: "CreatedAt",
            table: "Tariffs",
            type: "datetime(6)",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.AddColumn<DateTime>(
            name: "DeletedAt",
            table: "Tariffs",
            type: "datetime(6)",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.AddColumn<DateTime>(
            name: "ModifiedAt",
            table: "Tariffs",
            type: "datetime(6)",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.AddColumn<DateTime>(
            name: "CreatedAt",
            table: "Homes",
            type: "datetime(6)",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.AddColumn<DateTime>(
            name: "DeletedAt",
            table: "Homes",
            type: "datetime(6)",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.AddColumn<DateTime>(
            name: "ModifiedAt",
            table: "Homes",
            type: "datetime(6)",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.AddColumn<Guid>(
            name: "Scope",
            table: "Homes",
            type: "char(36)",
            nullable: false,
            defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
            collation: "ascii_general_ci");

        migrationBuilder.AddColumn<DateTime>(
            name: "CreatedAt",
            table: "Checks",
            type: "datetime(6)",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.AddColumn<DateTime>(
            name: "DeletedAt",
            table: "Checks",
            type: "datetime(6)",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.AddColumn<DateTime>(
            name: "ModifiedAt",
            table: "Checks",
            type: "datetime(6)",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "CreatedAt",
            table: "UtilityTypes");

        migrationBuilder.DropColumn(
            name: "DeletedAt",
            table: "UtilityTypes");

        migrationBuilder.DropColumn(
            name: "ModifiedAt",
            table: "UtilityTypes");

        migrationBuilder.DropColumn(
            name: "CreatedAt",
            table: "Tariffs");

        migrationBuilder.DropColumn(
            name: "DeletedAt",
            table: "Tariffs");

        migrationBuilder.DropColumn(
            name: "ModifiedAt",
            table: "Tariffs");

        migrationBuilder.DropColumn(
            name: "CreatedAt",
            table: "Homes");

        migrationBuilder.DropColumn(
            name: "DeletedAt",
            table: "Homes");

        migrationBuilder.DropColumn(
            name: "ModifiedAt",
            table: "Homes");

        migrationBuilder.DropColumn(
            name: "Scope",
            table: "Homes");

        migrationBuilder.DropColumn(
            name: "CreatedAt",
            table: "Checks");

        migrationBuilder.DropColumn(
            name: "DeletedAt",
            table: "Checks");

        migrationBuilder.DropColumn(
            name: "ModifiedAt",
            table: "Checks");
    }
}
