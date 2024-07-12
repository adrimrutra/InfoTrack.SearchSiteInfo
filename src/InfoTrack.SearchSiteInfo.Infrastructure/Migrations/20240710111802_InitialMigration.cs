using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfoTrack.SearchSiteInfo.Infrastructure.Migrations;

/// <inheritdoc />
public partial class InitialMigration : Migration
{
  /// <inheritdoc />
  protected override void Up(MigrationBuilder migrationBuilder)
  {
    migrationBuilder.CreateTable(
        name: "SearchRequests",
        columns: table => new
        {
          Id = table.Column<int>(type: "int", nullable: false)
                .Annotation("SqlServer:Identity", "1, 1"),
          Keywords = table.Column<string>(type: "nvarchar(max)", nullable: false),
          Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
          Engine = table.Column<string>(type: "nvarchar(max)", nullable: false),
          Rank = table.Column<int>(type: "int", nullable: false),
          CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_SearchRequests", x => x.Id);
        });
  }

  /// <inheritdoc />
  protected override void Down(MigrationBuilder migrationBuilder)
  {
    migrationBuilder.DropTable(
        name: "SearchRequests");
  }
}
