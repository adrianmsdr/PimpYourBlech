using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PimpYourBlech_ClassLibrary.Migrations
{
    /// <inheritdoc />
    public partial class ConfiguratorUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Colors_Cars_CarId",
                table: "Colors");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Cars_CarId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Colors_CarId",
                table: "Colors");

            migrationBuilder.DropColumn(
                name: "CarId",
                table: "Colors");

            migrationBuilder.AlterColumn<int>(
                name: "CarId",
                table: "Products",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CarId1",
                table: "Products",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_CarId1",
                table: "Products",
                column: "CarId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Cars_CarId",
                table: "Products",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Cars_CarId1",
                table: "Products",
                column: "CarId1",
                principalTable: "Cars",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Cars_CarId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Cars_CarId1",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CarId1",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CarId1",
                table: "Products");

            migrationBuilder.AlterColumn<int>(
                name: "CarId",
                table: "Products",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "CarId",
                table: "Colors",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Colors_CarId",
                table: "Colors",
                column: "CarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Colors_Cars_CarId",
                table: "Colors",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Cars_CarId",
                table: "Products",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id");
        }
    }
}
