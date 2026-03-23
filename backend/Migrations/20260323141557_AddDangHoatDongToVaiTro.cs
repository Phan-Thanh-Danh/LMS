using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class AddDangHoatDongToVaiTro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "DangHoatDong",
                table: "VaiTros",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "VaiTros",
                keyColumn: "MaVaiTro",
                keyValue: 1,
                column: "DangHoatDong",
                value: true);

            migrationBuilder.UpdateData(
                table: "VaiTros",
                keyColumn: "MaVaiTro",
                keyValue: 2,
                column: "DangHoatDong",
                value: true);

            migrationBuilder.UpdateData(
                table: "VaiTros",
                keyColumn: "MaVaiTro",
                keyValue: 3,
                column: "DangHoatDong",
                value: true);

            migrationBuilder.UpdateData(
                table: "VaiTros",
                keyColumn: "MaVaiTro",
                keyValue: 4,
                column: "DangHoatDong",
                value: true);

            migrationBuilder.UpdateData(
                table: "VaiTros",
                keyColumn: "MaVaiTro",
                keyValue: 5,
                column: "DangHoatDong",
                value: true);

            migrationBuilder.UpdateData(
                table: "VaiTros",
                keyColumn: "MaVaiTro",
                keyValue: 6,
                column: "DangHoatDong",
                value: true);

            migrationBuilder.UpdateData(
                table: "VaiTros",
                keyColumn: "MaVaiTro",
                keyValue: 7,
                column: "DangHoatDong",
                value: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DangHoatDong",
                table: "VaiTros");
        }
    }
}
