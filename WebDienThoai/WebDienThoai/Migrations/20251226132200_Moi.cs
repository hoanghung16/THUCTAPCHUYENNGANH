using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebDienThoai.Migrations
{
    /// <inheritdoc />
    public partial class Moi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsVisible",
                table: "Category",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "id",
                keyValue: 1,
                column: "IsVisible",
                value: true);

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "id",
                keyValue: 2,
                column: "IsVisible",
                value: true);

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "id",
                keyValue: 3,
                column: "IsVisible",
                value: true);

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "id",
                keyValue: 4,
                column: "IsVisible",
                value: true);

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "id",
                keyValue: 5,
                column: "IsVisible",
                value: true);

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "id",
                keyValue: 6,
                column: "IsVisible",
                value: true);

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "id",
                keyValue: 7,
                column: "IsVisible",
                value: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVisible",
                table: "Category");
        }
    }
}
