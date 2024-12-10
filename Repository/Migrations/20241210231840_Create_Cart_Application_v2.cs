using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EL.Repository.Migrations
{
    /// <inheritdoc />
    public partial class Create_Cart_Application_v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ClosedById",
                table: "Application",
                type: "binary(16)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "closed_by_user_id",
                table: "Application",
                type: "binary(16)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Application_ClosedById",
                table: "Application",
                column: "ClosedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Application_Users_ClosedById",
                table: "Application",
                column: "ClosedById",
                principalTable: "Users",
                principalColumn: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Application_Users_ClosedById",
                table: "Application");

            migrationBuilder.DropIndex(
                name: "IX_Application_ClosedById",
                table: "Application");

            migrationBuilder.DropColumn(
                name: "ClosedById",
                table: "Application");

            migrationBuilder.DropColumn(
                name: "closed_by_user_id",
                table: "Application");
        }
    }
}
