using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations;

/// <inheritdoc />
public partial class RemovedUtilityType : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Tariffs_UtilityTypes_TypeId",
            table: "Tariffs");

        migrationBuilder.DropTable(
            name: "UtilityTypes");

        migrationBuilder.DropIndex(
            name: "IX_Tariffs_TypeId",
            table: "Tariffs");

        migrationBuilder.DropColumn(
            name: "TypeId",
            table: "Tariffs");

        migrationBuilder.AddColumn<string>(
            name: "Name",
            table: "Tariffs",
            type: "longtext",
            nullable: false)
            .Annotation("MySql:CharSet", "utf8mb4");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "Name",
            table: "Tariffs");

        migrationBuilder.AddColumn<Guid>(
            name: "TypeId",
            table: "Tariffs",
            type: "char(36)",
            nullable: false,
            defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
            collation: "ascii_general_ci");

        migrationBuilder.CreateTable(
            name: "UtilityTypes",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                IsArchived = table.Column<bool>(type: "tinyint(1)", nullable: false),
                ModifiedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                Name = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4")
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_UtilityTypes", x => x.Id);
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateIndex(
            name: "IX_Tariffs_TypeId",
            table: "Tariffs",
            column: "TypeId");

        migrationBuilder.AddForeignKey(
            name: "FK_Tariffs_UtilityTypes_TypeId",
            table: "Tariffs",
            column: "TypeId",
            principalTable: "UtilityTypes",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }
}
