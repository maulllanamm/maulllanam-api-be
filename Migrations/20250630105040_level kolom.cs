using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace maulllanam_api_be.Migrations
{
    /// <inheritdoc />
    public partial class levelkolom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "skills",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Level",
                table: "skills");
        }
    }
}
