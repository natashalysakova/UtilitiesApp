using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations;

/// <inheritdoc />
public partial class ChangedNullability : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<DateTime>(
            name: "ModifiedAt",
            table: "UtilityTypes",
            type: "datetime(6)",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "datetime(6)");

        migrationBuilder.AlterColumn<DateTime>(
            name: "DeletedAt",
            table: "UtilityTypes",
            type: "datetime(6)",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "datetime(6)");

        migrationBuilder.AlterColumn<DateTime>(
            name: "ModifiedAt",
            table: "Tariffs",
            type: "datetime(6)",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "datetime(6)");

        migrationBuilder.AlterColumn<DateTime>(
            name: "DeletedAt",
            table: "Tariffs",
            type: "datetime(6)",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "datetime(6)");

        migrationBuilder.AlterColumn<DateTime>(
            name: "ModifiedAt",
            table: "Homes",
            type: "datetime(6)",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "datetime(6)");

        migrationBuilder.AlterColumn<DateTime>(
            name: "DeletedAt",
            table: "Homes",
            type: "datetime(6)",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "datetime(6)");

        migrationBuilder.AlterColumn<DateTime>(
            name: "ModifiedAt",
            table: "Checks",
            type: "datetime(6)",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "datetime(6)");

        migrationBuilder.AlterColumn<DateTime>(
            name: "DeletedAt",
            table: "Checks",
            type: "datetime(6)",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "datetime(6)");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<DateTime>(
            name: "ModifiedAt",
            table: "UtilityTypes",
            type: "datetime(6)",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
            oldClrType: typeof(DateTime),
            oldType: "datetime(6)",
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            name: "DeletedAt",
            table: "UtilityTypes",
            type: "datetime(6)",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
            oldClrType: typeof(DateTime),
            oldType: "datetime(6)",
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            name: "ModifiedAt",
            table: "Tariffs",
            type: "datetime(6)",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
            oldClrType: typeof(DateTime),
            oldType: "datetime(6)",
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            name: "DeletedAt",
            table: "Tariffs",
            type: "datetime(6)",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
            oldClrType: typeof(DateTime),
            oldType: "datetime(6)",
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            name: "ModifiedAt",
            table: "Homes",
            type: "datetime(6)",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
            oldClrType: typeof(DateTime),
            oldType: "datetime(6)",
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            name: "DeletedAt",
            table: "Homes",
            type: "datetime(6)",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
            oldClrType: typeof(DateTime),
            oldType: "datetime(6)",
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            name: "ModifiedAt",
            table: "Checks",
            type: "datetime(6)",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
            oldClrType: typeof(DateTime),
            oldType: "datetime(6)",
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            name: "DeletedAt",
            table: "Checks",
            type: "datetime(6)",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
            oldClrType: typeof(DateTime),
            oldType: "datetime(6)",
            oldNullable: true);
    }
}
