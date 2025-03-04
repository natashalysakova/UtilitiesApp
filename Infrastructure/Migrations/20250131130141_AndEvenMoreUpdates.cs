using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations;

/// <inheritdoc />
public partial class AndEvenMoreUpdates : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<string>(
            name: "Street",
            table: "Homes",
            type: "longtext",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "varchar(96)",
            oldMaxLength: 96)
            .Annotation("MySql:CharSet", "utf8mb4")
            .OldAnnotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.AlterColumn<string>(
            name: "Region",
            table: "Homes",
            type: "longtext",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "varchar(96)",
            oldMaxLength: 96)
            .Annotation("MySql:CharSet", "utf8mb4")
            .OldAnnotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.AlterColumn<string>(
            name: "Name",
            table: "Homes",
            type: "longtext",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "varchar(32)",
            oldMaxLength: 32)
            .Annotation("MySql:CharSet", "utf8mb4")
            .OldAnnotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.AlterColumn<string>(
            name: "Country",
            table: "Homes",
            type: "longtext",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "varchar(96)",
            oldMaxLength: 96)
            .Annotation("MySql:CharSet", "utf8mb4")
            .OldAnnotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.AlterColumn<string>(
            name: "City",
            table: "Homes",
            type: "longtext",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "varchar(96)",
            oldMaxLength: 96)
            .Annotation("MySql:CharSet", "utf8mb4")
            .OldAnnotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.AlterColumn<string>(
            name: "Building",
            table: "Homes",
            type: "longtext",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "varchar(96)",
            oldMaxLength: 96)
            .Annotation("MySql:CharSet", "utf8mb4")
            .OldAnnotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.AlterColumn<string>(
            name: "Apartment",
            table: "Homes",
            type: "longtext",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "varchar(32)",
            oldMaxLength: 32,
            oldNullable: true)
            .Annotation("MySql:CharSet", "utf8mb4")
            .OldAnnotation("MySql:CharSet", "utf8mb4");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<string>(
            name: "Street",
            table: "Homes",
            type: "varchar(96)",
            maxLength: 96,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "longtext")
            .Annotation("MySql:CharSet", "utf8mb4")
            .OldAnnotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.AlterColumn<string>(
            name: "Region",
            table: "Homes",
            type: "varchar(96)",
            maxLength: 96,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "longtext")
            .Annotation("MySql:CharSet", "utf8mb4")
            .OldAnnotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.AlterColumn<string>(
            name: "Name",
            table: "Homes",
            type: "varchar(32)",
            maxLength: 32,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "longtext")
            .Annotation("MySql:CharSet", "utf8mb4")
            .OldAnnotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.AlterColumn<string>(
            name: "Country",
            table: "Homes",
            type: "varchar(96)",
            maxLength: 96,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "longtext")
            .Annotation("MySql:CharSet", "utf8mb4")
            .OldAnnotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.AlterColumn<string>(
            name: "City",
            table: "Homes",
            type: "varchar(96)",
            maxLength: 96,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "longtext")
            .Annotation("MySql:CharSet", "utf8mb4")
            .OldAnnotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.AlterColumn<string>(
            name: "Building",
            table: "Homes",
            type: "varchar(96)",
            maxLength: 96,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "longtext")
            .Annotation("MySql:CharSet", "utf8mb4")
            .OldAnnotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.AlterColumn<string>(
            name: "Apartment",
            table: "Homes",
            type: "varchar(32)",
            maxLength: 32,
            nullable: true,
            oldClrType: typeof(string),
            oldType: "longtext",
            oldNullable: true)
            .Annotation("MySql:CharSet", "utf8mb4")
            .OldAnnotation("MySql:CharSet", "utf8mb4");
    }
}
