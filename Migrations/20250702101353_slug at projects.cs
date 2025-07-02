using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace maulllanam_api_be.Migrations
{
    /// <inheritdoc />
    public partial class slugatprojects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "projects",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Slug",
                table: "projects");
        }
    }
}
