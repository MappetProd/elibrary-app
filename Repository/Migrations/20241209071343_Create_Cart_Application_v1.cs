using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EL.Repository.Migrations
{
    /// <inheritdoc />
    public partial class Create_Cart_Application_v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Application",
                columns: table => new
                {
                    application_id = table.Column<Guid>(type: "binary(16)", nullable: false),
                    issued_by_user_id = table.Column<Guid>(type: "binary(16)", nullable: false),
                    resolved_by_user_id = table.Column<Guid>(type: "binary(16)", nullable: true),
                    application_creation_dtm = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    resolve_dtm = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    application_sequence_number = table.Column<int>(type: "int", nullable: false),
                    deadline_date = table.Column<DateOnly>(type: "date", nullable: true),
                    actual_end_dtm = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IssuedById = table.Column<Guid>(type: "binary(16)", nullable: false),
                    ResolvedById = table.Column<Guid>(type: "binary(16)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Application", x => x.application_id);
                    table.ForeignKey(
                        name: "FK_Application_Users_IssuedById",
                        column: x => x.IssuedById,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Application_Users_ResolvedById",
                        column: x => x.ResolvedById,
                        principalTable: "Users",
                        principalColumn: "user_id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    cart_item_id = table.Column<Guid>(type: "binary(16)", nullable: false),
                    addition_dtm = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    printed_book_id = table.Column<Guid>(type: "binary(16)", nullable: false),
                    user_id = table.Column<Guid>(type: "binary(16)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.cart_item_id);
                    table.ForeignKey(
                        name: "FK_CartItems_PrintedBooks_printed_book_id",
                        column: x => x.printed_book_id,
                        principalTable: "PrintedBooks",
                        principalColumn: "printed_book_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItems_Users_user_id",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Applications_PrintedBooks",
                columns: table => new
                {
                    printed_book_id = table.Column<Guid>(type: "binary(16)", nullable: false),
                    application_id = table.Column<Guid>(type: "binary(16)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications_PrintedBooks", x => new { x.printed_book_id, x.application_id });
                    table.ForeignKey(
                        name: "FK_Applications_PrintedBooks_Application_application_id",
                        column: x => x.application_id,
                        principalTable: "Application",
                        principalColumn: "application_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Applications_PrintedBooks_PrintedBooks_printed_book_id",
                        column: x => x.printed_book_id,
                        principalTable: "PrintedBooks",
                        principalColumn: "printed_book_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Application_IssuedById",
                table: "Application",
                column: "IssuedById");

            migrationBuilder.CreateIndex(
                name: "IX_Application_ResolvedById",
                table: "Application",
                column: "ResolvedById");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_PrintedBooks_application_id",
                table: "Applications_PrintedBooks",
                column: "application_id");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_printed_book_id",
                table: "CartItems",
                column: "printed_book_id");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_user_id",
                table: "CartItems",
                column: "user_id");

            /*migrationBuilder.AddForeignKey(
                name: "FK_Authors_Persons_author_id",
                table: "Authors",
                column: "author_id",
                principalTable: "Persons",
                principalColumn: "person_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Persons_user_id",
                table: "Users",
                column: "user_id",
                principalTable: "Persons",
                principalColumn: "person_id",
                onDelete: ReferentialAction.Cascade);*/
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            /*migrationBuilder.DropForeignKey(
                name: "FK_Authors_Persons_author_id",
                table: "Authors");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Persons_user_id",
                table: "Users");*/

            migrationBuilder.DropTable(
                name: "Applications_PrintedBooks");

            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "Application");

            /*migrationBuilder.RenameColumn(
                name: "user_id",
                table: "Users",
                newName: "person_id");

            migrationBuilder.RenameColumn(
                name: "author_id",
                table: "Authors",
                newName: "person_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_Persons_person_id",
                table: "Authors",
                column: "person_id",
                principalTable: "Persons",
                principalColumn: "person_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Persons_person_id",
                table: "Users",
                column: "person_id",
                principalTable: "Persons",
                principalColumn: "person_id",
                onDelete: ReferentialAction.Cascade);*/
        }
    }
}
