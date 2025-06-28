using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace maulllanam_api_be.Migrations
{
    /// <inheritdoc />
    public partial class adduserIdatFiles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "files",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_files_UserId",
                table: "files",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_files_users_UserId",
                table: "files",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_files_users_UserId",
                table: "files");

            migrationBuilder.DropIndex(
                name: "IX_files_UserId",
                table: "files");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "files");
        }
    }
}
