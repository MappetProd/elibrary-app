using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EL.Repository.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Users_user_status_id",
                table: "Users",
                column: "user_status_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Statuses_user_status_id",
                table: "Users",
                column: "user_status_id",
                principalTable: "Statuses",
                principalColumn: "status_id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Statuses_user_status_id",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_user_status_id",
                table: "Users");
        }
    }
}
