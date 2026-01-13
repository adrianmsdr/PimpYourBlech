using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PimpYourBlech_ClassLibrary.Migrations
{
    /// <inheritdoc />
    public partial class Payment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderPositions_Products_ProductId",
                table: "OrderPositions");

            migrationBuilder.DropIndex(
                name: "IX_OrderPositions_ProductId",
                table: "OrderPositions");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "OrderPositions",
                newName: "Type");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Products",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AddColumn<int>(
                name: "PaymentValueId",
                table: "Orders",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ArticleNumber",
                table: "OrderPositions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "OrderPositions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "OrderPositions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Cars",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.CreateTable(
                name: "PaymentValues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustomerId = table.Column<int>(type: "integer", nullable: false),
                    AccountOwner = table.Column<string>(type: "text", nullable: false),
                    Iban = table.Column<string>(type: "text", nullable: false),
                    Bic = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentValues_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PaymentValueId",
                table: "Orders",
                column: "PaymentValueId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentValues_CustomerId",
                table: "PaymentValues",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_PaymentValues_PaymentValueId",
                table: "Orders",
                column: "PaymentValueId",
                principalTable: "PaymentValues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_PaymentValues_PaymentValueId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "PaymentValues");

            migrationBuilder.DropIndex(
                name: "IX_Orders_PaymentValueId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PaymentValueId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ArticleNumber",
                table: "OrderPositions");

            migrationBuilder.DropColumn(
                name: "Brand",
                table: "OrderPositions");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "OrderPositions");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "OrderPositions",
                newName: "ProductId");

            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Products",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Cars",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.CreateIndex(
                name: "IX_OrderPositions_ProductId",
                table: "OrderPositions",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderPositions_Products_ProductId",
                table: "OrderPositions",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
