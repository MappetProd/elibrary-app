using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EL.Repository.Migrations
{
    /// <inheritdoc />
    public partial class CreateCatalog_v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrintedBooks_Books_BookId1",
                table: "PrintedBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_PrintedBooks_Publishers_PublisherId1",
                table: "PrintedBooks");

            migrationBuilder.RenameColumn(
                name: "PublisherId1",
                table: "PrintedBooks",
                newName: "publisher_id");

            migrationBuilder.RenameColumn(
                name: "BookId1",
                table: "PrintedBooks",
                newName: "book_id");

            migrationBuilder.RenameIndex(
                name: "IX_PrintedBooks_PublisherId1",
                table: "PrintedBooks",
                newName: "IX_PrintedBooks_publisher_id");

            migrationBuilder.RenameIndex(
                name: "IX_PrintedBooks_BookId1",
                table: "PrintedBooks",
                newName: "IX_PrintedBooks_book_id");

            migrationBuilder.AddForeignKey(
                name: "FK_PrintedBooks_Books_book_id",
                table: "PrintedBooks",
                column: "book_id",
                principalTable: "Books",
                principalColumn: "book_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PrintedBooks_Publishers_publisher_id",
                table: "PrintedBooks",
                column: "publisher_id",
                principalTable: "Publishers",
                principalColumn: "publisher_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrintedBooks_Books_book_id",
                table: "PrintedBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_PrintedBooks_Publishers_publisher_id",
                table: "PrintedBooks");

            migrationBuilder.RenameColumn(
                name: "publisher_id",
                table: "PrintedBooks",
                newName: "PublisherId1");

            migrationBuilder.RenameColumn(
                name: "book_id",
                table: "PrintedBooks",
                newName: "BookId1");

            migrationBuilder.RenameIndex(
                name: "IX_PrintedBooks_publisher_id",
                table: "PrintedBooks",
                newName: "IX_PrintedBooks_PublisherId1");

            migrationBuilder.RenameIndex(
                name: "IX_PrintedBooks_book_id",
                table: "PrintedBooks",
                newName: "IX_PrintedBooks_BookId1");

            migrationBuilder.AddForeignKey(
                name: "FK_PrintedBooks_Books_BookId1",
                table: "PrintedBooks",
                column: "BookId1",
                principalTable: "Books",
                principalColumn: "book_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PrintedBooks_Publishers_PublisherId1",
                table: "PrintedBooks",
                column: "PublisherId1",
                principalTable: "Publishers",
                principalColumn: "publisher_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
