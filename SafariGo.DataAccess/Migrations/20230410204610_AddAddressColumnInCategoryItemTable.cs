using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SafariGo.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddAddressColumnInCategoryItemTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryItems_Categories_CategoryId",
                table: "CategoryItems");

            migrationBuilder.AlterColumn<string>(
                name: "CategoryId",
                table: "CategoryItems",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "CategoryItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryItems_Categories_CategoryId",
                table: "CategoryItems",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryItems_Categories_CategoryId",
                table: "CategoryItems");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "CategoryItems");

            migrationBuilder.AlterColumn<string>(
                name: "CategoryId",
                table: "CategoryItems",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryItems_Categories_CategoryId",
                table: "CategoryItems",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }
    }
}
