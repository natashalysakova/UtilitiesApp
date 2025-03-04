using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations;

/// <inheritdoc />
public partial class CreateUtilityType : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<Guid>(
            name: "UtilityGroupId",
            table: "Tariffs",
            type: "char(36)",
            nullable: false,
            defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
            collation: "ascii_general_ci");

        migrationBuilder.CreateTable(
            name: "UtilityGroups",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                Name = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                IsArchived = table.Column<bool>(type: "tinyint(1)", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                ModifiedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_UtilityGroups", x => x.Id);
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder
            .Sql("INSERT INTO UtilityGroups (Id, Name, IsArchived, CreatedAt) SELECT UUID(), selected.Name, FALSE, NOW() FROM (SELECT DISTINCT Name FROM Tariffs) as selected");
        migrationBuilder
            .Sql("UPDATE Tariffs SET UtilityGroupId =(SELECT Id  FROM UtilityGroups    WHERE UtilityGroups.Name = Tariffs.Name Limit 1)");

        migrationBuilder.CreateIndex(
            name: "IX_Tariffs_UtilityGroupId",
            table: "Tariffs",
            column: "UtilityGroupId");

        migrationBuilder.AddForeignKey(
            name: "FK_Tariffs_UtilityGroups_UtilityGroupId",
            table: "Tariffs",
            column: "UtilityGroupId",
            principalTable: "UtilityGroups",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.DropColumn(
            name: "Name",
            table: "Tariffs");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            name: "Name",
            table: "Tariffs",
            type: "longtext",
            nullable: false)
            .Annotation("MySql:CharSet", "utf8mb4");


        migrationBuilder.Sql("UPDATE Tariffs SET Name = (SELECT Name from UtilityGroups WHERE UtilityGroups.Id = Tariffs.UtilityGroupId LIMIT 1)");


        migrationBuilder.DropForeignKey(
            name: "FK_Tariffs_UtilityGroups_UtilityGroupId",
            table: "Tariffs");

        migrationBuilder.DropTable(
            name: "UtilityGroups");

        migrationBuilder.DropIndex(
            name: "IX_Tariffs_UtilityGroupId",
            table: "Tariffs");

        migrationBuilder.DropColumn(
            name: "UtilityGroupId",
            table: "Tariffs");
    }
}
