using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace maulllanam_api_be.Migrations
{
    /// <inheritdoc />
    public partial class addgithubUrlandfeatureatprojects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Features",
                table: "projects",
                type: "jsonb",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Github",
                table: "projects",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Features",
                table: "projects");

            migrationBuilder.DropColumn(
                name: "Github",
                table: "projects");
        }
    }
}
