using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dashboard.Migrations
{
    /// <inheritdoc />
    public partial class AddValidationToProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_posts_category_categoryId",
                table: "posts");

            migrationBuilder.RenameColumn(
                name: "categoryId",
                table: "posts",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_posts_categoryId",
                table: "posts",
                newName: "IX_posts_CategoryId");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "products",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_posts_category_CategoryId",
                table: "posts",
                column: "CategoryId",
                principalTable: "category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_posts_category_CategoryId",
                table: "posts");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "posts",
                newName: "categoryId");

            migrationBuilder.RenameIndex(
                name: "IX_posts_CategoryId",
                table: "posts",
                newName: "IX_posts_categoryId");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "products",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddForeignKey(
                name: "FK_posts_category_categoryId",
                table: "posts",
                column: "categoryId",
                principalTable: "category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
