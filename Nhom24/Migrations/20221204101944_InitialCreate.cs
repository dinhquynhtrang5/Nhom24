using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nhom24.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DanhMucSanPham",
                columns: table => new
                {
                    DanhMucSanPhamID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DanhMucSanPhamName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanhMucSanPham", x => x.DanhMucSanPhamID);
                });

            migrationBuilder.CreateTable(
                name: "NguoiDung",
                columns: table => new
                {
                    NguoiDungID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NguoiDungName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailNguoiDung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SDTNguoiDung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MatKhauNguoiDung = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NguoiDung", x => x.NguoiDungID);
                });

            migrationBuilder.CreateTable(
                name: "NganhHang",
                columns: table => new
                {
                    NganhHangID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NganhHangName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DanhMucSanPhamID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NganhHang", x => x.NganhHangID);
                    table.ForeignKey(
                        name: "FK_NganhHang_DanhMucSanPham_DanhMucSanPhamID",
                        column: x => x.DanhMucSanPhamID,
                        principalTable: "DanhMucSanPham",
                        principalColumn: "DanhMucSanPhamID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DonHang",
                columns: table => new
                {
                    DonHangID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DiaChiDonHang = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThanhTien = table.Column<double>(type: "float", nullable: false),
                    NguoiDungID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonHang", x => x.DonHangID);
                    table.ForeignKey(
                        name: "FK_DonHang_NguoiDung_NguoiDungID",
                        column: x => x.NguoiDungID,
                        principalTable: "NguoiDung",
                        principalColumn: "NguoiDungID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SanPham",
                columns: table => new
                {
                    SanPhamID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SanPhamName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnhSanPham = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GiaSanPham = table.Column<double>(type: "float", nullable: false),
                    SoLuongSanPham = table.Column<int>(type: "int", nullable: false),
                    MoTaSanPham = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NganhHangID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SanPham", x => x.SanPhamID);
                    table.ForeignKey(
                        name: "FK_SanPham_NganhHang_NganhHangID",
                        column: x => x.NganhHangID,
                        principalTable: "NganhHang",
                        principalColumn: "NganhHangID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietDonHang",
                columns: table => new
                {
                    DonHangID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SanPhamID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SoLuongSanPham = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietDonHang", x => x.DonHangID);
                    table.ForeignKey(
                        name: "FK_ChiTietDonHang_DonHang_DonHangID",
                        column: x => x.DonHangID,
                        principalTable: "DonHang",
                        principalColumn: "DonHangID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChiTietDonHang_SanPham_SanPhamID",
                        column: x => x.SanPhamID,
                        principalTable: "SanPham",
                        principalColumn: "SanPhamID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GioHang",
                columns: table => new
                {
                    NguoiDungID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SanPhamID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SoLuongSanPham = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GioHang", x => x.NguoiDungID);
                    table.ForeignKey(
                        name: "FK_GioHang_NguoiDung_NguoiDungID",
                        column: x => x.NguoiDungID,
                        principalTable: "NguoiDung",
                        principalColumn: "NguoiDungID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GioHang_SanPham_SanPhamID",
                        column: x => x.SanPhamID,
                        principalTable: "SanPham",
                        principalColumn: "SanPhamID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietDonHang_SanPhamID",
                table: "ChiTietDonHang",
                column: "SanPhamID");

            migrationBuilder.CreateIndex(
                name: "IX_DonHang_NguoiDungID",
                table: "DonHang",
                column: "NguoiDungID");

            migrationBuilder.CreateIndex(
                name: "IX_GioHang_SanPhamID",
                table: "GioHang",
                column: "SanPhamID");

            migrationBuilder.CreateIndex(
                name: "IX_NganhHang_DanhMucSanPhamID",
                table: "NganhHang",
                column: "DanhMucSanPhamID");

            migrationBuilder.CreateIndex(
                name: "IX_SanPham_NganhHangID",
                table: "SanPham",
                column: "NganhHangID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChiTietDonHang");

            migrationBuilder.DropTable(
                name: "GioHang");

            migrationBuilder.DropTable(
                name: "DonHang");

            migrationBuilder.DropTable(
                name: "SanPham");

            migrationBuilder.DropTable(
                name: "NguoiDung");

            migrationBuilder.DropTable(
                name: "NganhHang");

            migrationBuilder.DropTable(
                name: "DanhMucSanPham");
        }
    }
}
