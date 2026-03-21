using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CauHinhHeThongs",
                columns: table => new
                {
                    KhoaCauHinh = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    GiaTriCauHinh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KieuDuLieu = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CapNhatBoi = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CauHinhHeThongs", x => x.KhoaCauHinh);
                });

            migrationBuilder.CreateTable(
                name: "DanhMucs",
                columns: table => new
                {
                    MaDanhMuc = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaDanhMucCha = table.Column<int>(type: "int", nullable: true),
                    TenDanhMuc = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DuongDanURL = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ThuTuHienThi = table.Column<int>(type: "int", nullable: false),
                    DangHienThi = table.Column<bool>(type: "bit", nullable: false),
                    DuongDanIcon = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanhMucs", x => x.MaDanhMuc);
                    table.ForeignKey(
                        name: "FK_DanhMucs_DanhMucs_MaDanhMucCha",
                        column: x => x.MaDanhMucCha,
                        principalTable: "DanhMucs",
                        principalColumn: "MaDanhMuc");
                });

            migrationBuilder.CreateTable(
                name: "LanDangNhaps",
                columns: table => new
                {
                    MaLanLam = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DiaChiIP = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TrinhDuyet = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DangNhapThanhCong = table.Column<bool>(type: "bit", nullable: false),
                    LyDoThatBai = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    BatDauLamLuc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanDangNhaps", x => x.MaLanLam);
                });

            migrationBuilder.CreateTable(
                name: "NguoiDungs",
                columns: table => new
                {
                    MaNguoiDung = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    MatKhauBam = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    MuoiMatKhau = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DuongDanAnhDaiDien = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TieuSu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DangHoatDong = table.Column<bool>(type: "bit", nullable: false),
                    DaXoa = table.Column<bool>(type: "bit", nullable: false),
                    EmailDaXacThuc = table.Column<bool>(type: "bit", nullable: false),
                    LaNhanVien = table.Column<bool>(type: "bit", nullable: false),
                    LanDangNhapCuoi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NguoiDungs", x => x.MaNguoiDung);
                });

            migrationBuilder.CreateTable(
                name: "NhanTuKhoas",
                columns: table => new
                {
                    MaNhan = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenNhan = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DuongDanURL = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SoLuotDung = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhanTuKhoas", x => x.MaNhan);
                });

            migrationBuilder.CreateTable(
                name: "NhatKyKiemToans",
                columns: table => new
                {
                    MaLog = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaNguoiDung = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    HanhDong = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    LoaiDoiTuong = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    MaDoiTuong = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    GiaTriCu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GiaTriMoi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiaChiIP = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TrinhDuyet = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ThoiDiem = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhatKyKiemToans", x => x.MaLog);
                });

            migrationBuilder.CreateTable(
                name: "VaiTros",
                columns: table => new
                {
                    MaVaiTro = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenVaiTro = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VaiTros", x => x.MaVaiTro);
                });

            migrationBuilder.CreateTable(
                name: "ChienDichs",
                columns: table => new
                {
                    MaChienDich = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenChienDich = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayBatDau = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayKetThuc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DuongDanBanner = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    DangHoatDong = table.Column<bool>(type: "bit", nullable: false),
                    TaoBoi = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChienDichs", x => x.MaChienDich);
                    table.ForeignKey(
                        name: "FK_ChienDichs_NguoiDungs_TaoBoi",
                        column: x => x.TaoBoi,
                        principalTable: "NguoiDungs",
                        principalColumn: "MaNguoiDung",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChuoiHocLienTieps",
                columns: table => new
                {
                    MaChuoi = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaHocVien = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChuoiHienTai = table.Column<int>(type: "int", nullable: false),
                    ChuoiDaiNhat = table.Column<int>(type: "int", nullable: false),
                    NgayHoatDongCuoi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TheDoangBang = table.Column<int>(type: "int", nullable: false),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChuoiHocLienTieps", x => x.MaChuoi);
                    table.ForeignKey(
                        name: "FK_ChuoiHocLienTieps_NguoiDungs_MaHocVien",
                        column: x => x.MaHocVien,
                        principalTable: "NguoiDungs",
                        principalColumn: "MaNguoiDung",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GioHangs",
                columns: table => new
                {
                    MaGioHang = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaHocVien = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GioHangs", x => x.MaGioHang);
                    table.ForeignKey(
                        name: "FK_GioHangs_NguoiDungs_MaHocVien",
                        column: x => x.MaHocVien,
                        principalTable: "NguoiDungs",
                        principalColumn: "MaNguoiDung",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HoSoGiangViens",
                columns: table => new
                {
                    MaGiangVien = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DanhHieu = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    TieuSuChiTiet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DuongDanWebsite = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DuongDanLinkedIn = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ThongTinNganHangMaHoa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaSoThueMaHoa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TyLeDoanhThu = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TrangThaiKYC = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    KyKetDieuKhoanLuc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoSoGiangViens", x => x.MaGiangVien);
                    table.ForeignKey(
                        name: "FK_HoSoGiangViens_NguoiDungs_MaGiangVien",
                        column: x => x.MaGiangVien,
                        principalTable: "NguoiDungs",
                        principalColumn: "MaNguoiDung",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HoSoNhanViens",
                columns: table => new
                {
                    MaNhanVien = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaNhanVienNoiBo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PhongBan = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ChucDanh = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SoDienThoaiNoiBo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    QuyenBoSung = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayVaoLam = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DangHoatDong = table.Column<bool>(type: "bit", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoSoNhanViens", x => x.MaNhanVien);
                    table.ForeignKey(
                        name: "FK_HoSoNhanViens_NguoiDungs_MaNhanVien",
                        column: x => x.MaNhanVien,
                        principalTable: "NguoiDungs",
                        principalColumn: "MaNguoiDung",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MaOtps",
                columns: table => new
                {
                    MaToken = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaNguoiDung = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GiaTriToken = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    LoaiToken = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    HetHanLuc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DaSuDung = table.Column<bool>(type: "bit", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaOtps", x => x.MaToken);
                    table.ForeignKey(
                        name: "FK_MaOtps_NguoiDungs_MaNguoiDung",
                        column: x => x.MaNguoiDung,
                        principalTable: "NguoiDungs",
                        principalColumn: "MaNguoiDung",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PhienLamViecs",
                columns: table => new
                {
                    MaPhien = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaNguoiDung = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TokenLamMoi = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    DiaChiIP = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ThongTinThietBi = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ViTri = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    HetHanLuc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DaThuHoi = table.Column<bool>(type: "bit", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhienLamViecs", x => x.MaPhien);
                    table.ForeignKey(
                        name: "FK_PhienLamViecs_NguoiDungs_MaNguoiDung",
                        column: x => x.MaNguoiDung,
                        principalTable: "NguoiDungs",
                        principalColumn: "MaNguoiDung",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PhieuHoTros",
                columns: table => new
                {
                    MaPhieu = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MaNguoiGuiYeuCau = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PhanCongCho = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PhanLoai = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TieuDeThongBao = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    MucUuTien = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MaDonHangLienQuan = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DuLieuBoiCanh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiemHaiLong = table.Column<int>(type: "int", nullable: true),
                    GiaiQuyetLuc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DongPhieuLuc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhieuHoTros", x => x.MaPhieu);
                    table.ForeignKey(
                        name: "FK_PhieuHoTros_NguoiDungs_MaNguoiGuiYeuCau",
                        column: x => x.MaNguoiGuiYeuCau,
                        principalTable: "NguoiDungs",
                        principalColumn: "MaNguoiDung",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PhieuHoTros_NguoiDungs_PhanCongCho",
                        column: x => x.PhanCongCho,
                        principalTable: "NguoiDungs",
                        principalColumn: "MaNguoiDung");
                });

            migrationBuilder.CreateTable(
                name: "TaiNguyenSos",
                columns: table => new
                {
                    MaTaiNguyen = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaiLenBoi = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenTep = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    LoaiTep = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    KieuMIME = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DungLuongByte = table.Column<long>(type: "bigint", nullable: false),
                    DuongDanLuuTru = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ThoiLuong = table.Column<int>(type: "int", nullable: true),
                    TrangThai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaiNguyenSos", x => x.MaTaiNguyen);
                    table.ForeignKey(
                        name: "FK_TaiNguyenSos_NguoiDungs_TaiLenBoi",
                        column: x => x.TaiLenBoi,
                        principalTable: "NguoiDungs",
                        principalColumn: "MaNguoiDung",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TuongTacNguoiDungs",
                columns: table => new
                {
                    MaTuongTac = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaNguoiDung = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaKhoaHoc = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoaiTuongTac = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ThoiDiem = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaPhien = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TuongTacNguoiDungs", x => x.MaTuongTac);
                    table.ForeignKey(
                        name: "FK_TuongTacNguoiDungs_NguoiDungs_MaNguoiDung",
                        column: x => x.MaNguoiDung,
                        principalTable: "NguoiDungs",
                        principalColumn: "MaNguoiDung",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "YeuCauRutTiens",
                columns: table => new
                {
                    MaYeuCau = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaGiangVien = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SoTienYeuCau = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SnapshotNganHang = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DuyetBoi = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GhiChuDuyet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThanhToanLuc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YeuCauRutTiens", x => x.MaYeuCau);
                    table.ForeignKey(
                        name: "FK_YeuCauRutTiens_NguoiDungs_DuyetBoi",
                        column: x => x.DuyetBoi,
                        principalTable: "NguoiDungs",
                        principalColumn: "MaNguoiDung");
                    table.ForeignKey(
                        name: "FK_YeuCauRutTiens_NguoiDungs_MaGiangVien",
                        column: x => x.MaGiangVien,
                        principalTable: "NguoiDungs",
                        principalColumn: "MaNguoiDung",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VaiTroNguoiDungs",
                columns: table => new
                {
                    MaVaiTroNguoiDung = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaNguoiDung = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaVaiTro = table.Column<int>(type: "int", nullable: false),
                    NgayGanVaiTro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GanBoi = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VaiTroNguoiDungs", x => x.MaVaiTroNguoiDung);
                    table.ForeignKey(
                        name: "FK_VaiTroNguoiDungs_NguoiDungs_MaNguoiDung",
                        column: x => x.MaNguoiDung,
                        principalTable: "NguoiDungs",
                        principalColumn: "MaNguoiDung",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VaiTroNguoiDungs_VaiTros_MaVaiTro",
                        column: x => x.MaVaiTro,
                        principalTable: "VaiTros",
                        principalColumn: "MaVaiTro",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MaGiamGia",
                columns: table => new
                {
                    MaGiamGia = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MaChienDich = table.Column<int>(type: "int", nullable: true),
                    MaGiangVien = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LoaiGiamGia = table.Column<int>(type: "int", nullable: false),
                    GiaTriGiam = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GiamToiDa = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DonHangToiThieu = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GioiHanLuotDung = table.Column<int>(type: "int", nullable: true),
                    SoLuotDaDung = table.Column<int>(type: "int", nullable: false),
                    NgayBatDau = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayKetThuc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DangHoatDong = table.Column<bool>(type: "bit", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaGiamGia", x => x.MaGiamGia);
                    table.ForeignKey(
                        name: "FK_MaGiamGia_ChienDichs_MaChienDich",
                        column: x => x.MaChienDich,
                        principalTable: "ChienDichs",
                        principalColumn: "MaChienDich");
                    table.ForeignKey(
                        name: "FK_MaGiamGia_NguoiDungs_MaGiangVien",
                        column: x => x.MaGiangVien,
                        principalTable: "NguoiDungs",
                        principalColumn: "MaNguoiDung");
                });

            migrationBuilder.CreateTable(
                name: "PhanHoiPhieus",
                columns: table => new
                {
                    MaTraLoi = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaPhieu = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MaNguoiGui = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NoiDungTinNhan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TepDinhKem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GhiChuNoiBo = table.Column<bool>(type: "bit", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhanHoiPhieus", x => x.MaTraLoi);
                    table.ForeignKey(
                        name: "FK_PhanHoiPhieus_NguoiDungs_MaNguoiGui",
                        column: x => x.MaNguoiGui,
                        principalTable: "NguoiDungs",
                        principalColumn: "MaNguoiDung",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PhanHoiPhieus_PhieuHoTros_MaPhieu",
                        column: x => x.MaPhieu,
                        principalTable: "PhieuHoTros",
                        principalColumn: "MaPhieu",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KhoaHocs",
                columns: table => new
                {
                    MaKhoaHoc = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaGiangVien = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaDanhMuc = table.Column<int>(type: "int", nullable: false),
                    TieuDe = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DuongDanURL = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    TieuDeNho = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MucTieuDauRa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YeuCauDieuKien = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrinhDo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NgonNgu = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Gia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DuongDanAnhDaiDien = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    MaVideoGioiThieu = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TongThoiLuong = table.Column<int>(type: "int", nullable: false),
                    TongBaiGiang = table.Column<int>(type: "int", nullable: false),
                    DiemDanhGiaTrungBinh = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TongDanhGia = table.Column<int>(type: "int", nullable: false),
                    TongGhiDanh = table.Column<int>(type: "int", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false),
                    GhiChuTuChoi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    XuatBanLuc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LuuTruLuc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DaXoa = table.Column<bool>(type: "bit", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhoaHocs", x => x.MaKhoaHoc);
                    table.ForeignKey(
                        name: "FK_KhoaHocs_DanhMucs_MaDanhMuc",
                        column: x => x.MaDanhMuc,
                        principalTable: "DanhMucs",
                        principalColumn: "MaDanhMuc",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KhoaHocs_NguoiDungs_MaGiangVien",
                        column: x => x.MaGiangVien,
                        principalTable: "NguoiDungs",
                        principalColumn: "MaNguoiDung",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KhoaHocs_TaiNguyenSos_MaVideoGioiThieu",
                        column: x => x.MaVideoGioiThieu,
                        principalTable: "TaiNguyenSos",
                        principalColumn: "MaTaiNguyen");
                });

            migrationBuilder.CreateTable(
                name: "MaHoaVideos",
                columns: table => new
                {
                    MaMaHoa = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaTaiNguyen = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DoPhanGiai = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DuongDanHLS = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    TocDoBitKbps = table.Column<int>(type: "int", nullable: true),
                    MaHoaLuc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaHoaVideos", x => x.MaMaHoa);
                    table.ForeignKey(
                        name: "FK_MaHoaVideos_TaiNguyenSos_MaTaiNguyen",
                        column: x => x.MaTaiNguyen,
                        principalTable: "TaiNguyenSos",
                        principalColumn: "MaTaiNguyen",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DonHangs",
                columns: table => new
                {
                    MaDonHang = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MaHocVien = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaGiamGia = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SoTienGoc = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SoTienGiam = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TongTien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DonViTienTe = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CongThanhToan = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MaGiaoDichCong = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    TrangThaiThanhToan = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ThanhToanLuc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DaXoa = table.Column<bool>(type: "bit", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonHangs", x => x.MaDonHang);
                    table.ForeignKey(
                        name: "FK_DonHangs_MaGiamGia_MaGiamGia",
                        column: x => x.MaGiamGia,
                        principalTable: "MaGiamGia",
                        principalColumn: "MaGiamGia");
                    table.ForeignKey(
                        name: "FK_DonHangs_NguoiDungs_MaHocVien",
                        column: x => x.MaHocVien,
                        principalTable: "NguoiDungs",
                        principalColumn: "MaNguoiDung",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChungChis",
                columns: table => new
                {
                    MaChungChi = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaHocVien = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaKhoaHoc = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NgayCapPhat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaXacThuc = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DuongDanPDFChungChi = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChungChis", x => x.MaChungChi);
                    table.ForeignKey(
                        name: "FK_ChungChis_KhoaHocs_MaKhoaHoc",
                        column: x => x.MaKhoaHoc,
                        principalTable: "KhoaHocs",
                        principalColumn: "MaKhoaHoc",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChungChis_NguoiDungs_MaHocVien",
                        column: x => x.MaHocVien,
                        principalTable: "NguoiDungs",
                        principalColumn: "MaNguoiDung",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Chuongs",
                columns: table => new
                {
                    MaChuong = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaKhoaHoc = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TieuDe = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThuTu = table.Column<int>(type: "int", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chuongs", x => x.MaChuong);
                    table.ForeignKey(
                        name: "FK_Chuongs_KhoaHocs_MaKhoaHoc",
                        column: x => x.MaKhoaHoc,
                        principalTable: "KhoaHocs",
                        principalColumn: "MaKhoaHoc",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DanhGias",
                columns: table => new
                {
                    MaDanhGia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaKhoaHoc = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaHocVien = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DiemSao = table.Column<int>(type: "int", nullable: false),
                    NhanXet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DaMuaKhoaHoc = table.Column<bool>(type: "bit", nullable: false),
                    BiBaoCao = table.Column<bool>(type: "bit", nullable: false),
                    DaXoa = table.Column<bool>(type: "bit", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanhGias", x => x.MaDanhGia);
                    table.ForeignKey(
                        name: "FK_DanhGias_KhoaHocs_MaKhoaHoc",
                        column: x => x.MaKhoaHoc,
                        principalTable: "KhoaHocs",
                        principalColumn: "MaKhoaHoc",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DanhGias_NguoiDungs_MaHocVien",
                        column: x => x.MaHocVien,
                        principalTable: "NguoiDungs",
                        principalColumn: "MaNguoiDung",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LichHocs",
                columns: table => new
                {
                    MaLichHoc = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaHocVien = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaKhoaHoc = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NgayTrongTuan = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    GioBatDau = table.Column<TimeSpan>(type: "time", nullable: false),
                    GioKetThuc = table.Column<TimeSpan>(type: "time", nullable: false),
                    ThoiLuongToiThieuPhut = table.Column<int>(type: "int", nullable: false),
                    DangHoatDong = table.Column<bool>(type: "bit", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LichHocs", x => x.MaLichHoc);
                    table.ForeignKey(
                        name: "FK_LichHocs_KhoaHocs_MaKhoaHoc",
                        column: x => x.MaKhoaHoc,
                        principalTable: "KhoaHocs",
                        principalColumn: "MaKhoaHoc",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LichHocs_NguoiDungs_MaHocVien",
                        column: x => x.MaHocVien,
                        principalTable: "NguoiDungs",
                        principalColumn: "MaNguoiDung",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MatHangGioHangs",
                columns: table => new
                {
                    MaMatHang = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaGioHang = table.Column<int>(type: "int", nullable: false),
                    MaKhoaHoc = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ThemVaoLuc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatHangGioHangs", x => x.MaMatHang);
                    table.ForeignKey(
                        name: "FK_MatHangGioHangs_GioHangs_MaGioHang",
                        column: x => x.MaGioHang,
                        principalTable: "GioHangs",
                        principalColumn: "MaGioHang",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MatHangGioHangs_KhoaHocs_MaKhoaHoc",
                        column: x => x.MaKhoaHoc,
                        principalTable: "KhoaHocs",
                        principalColumn: "MaKhoaHoc",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NhanKhoaHocs",
                columns: table => new
                {
                    MaKhoaHoc = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaNhan = table.Column<int>(type: "int", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhanKhoaHocs", x => new { x.MaKhoaHoc, x.MaNhan });
                    table.ForeignKey(
                        name: "FK_NhanKhoaHocs_KhoaHocs_MaKhoaHoc",
                        column: x => x.MaKhoaHoc,
                        principalTable: "KhoaHocs",
                        principalColumn: "MaKhoaHoc",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NhanKhoaHocs_NhanTuKhoas_MaNhan",
                        column: x => x.MaNhan,
                        principalTable: "NhanTuKhoas",
                        principalColumn: "MaNhan",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ThongBaoKhoaHocs",
                columns: table => new
                {
                    MaThongBao = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaKhoaHoc = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaGiangVien = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TieuDeThongBao = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GuiLuc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TongNguoiNhan = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThongBaoKhoaHocs", x => x.MaThongBao);
                    table.ForeignKey(
                        name: "FK_ThongBaoKhoaHocs_KhoaHocs_MaKhoaHoc",
                        column: x => x.MaKhoaHoc,
                        principalTable: "KhoaHocs",
                        principalColumn: "MaKhoaHoc",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ThongBaoKhoaHocs_NguoiDungs_MaGiangVien",
                        column: x => x.MaGiangVien,
                        principalTable: "NguoiDungs",
                        principalColumn: "MaNguoiDung",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TongHopDoanhThuNgays",
                columns: table => new
                {
                    NgayTongHop = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaKhoaHoc = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TongDonHang = table.Column<int>(type: "int", nullable: false),
                    TongDoanhThu = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TongHoanTien = table.Column<int>(type: "int", nullable: false),
                    SoTienHoan = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TongHopDoanhThuNgays", x => new { x.NgayTongHop, x.MaKhoaHoc });
                    table.ForeignKey(
                        name: "FK_TongHopDoanhThuNgays_KhoaHocs_MaKhoaHoc",
                        column: x => x.MaKhoaHoc,
                        principalTable: "KhoaHocs",
                        principalColumn: "MaKhoaHoc",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietDonHangs",
                columns: table => new
                {
                    MaChiTietDonHang = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaDonHang = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MaKhoaHoc = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GiaTaiThoiDiemMua = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GiamGiaApDung = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietDonHangs", x => x.MaChiTietDonHang);
                    table.ForeignKey(
                        name: "FK_ChiTietDonHangs_DonHangs_MaDonHang",
                        column: x => x.MaDonHang,
                        principalTable: "DonHangs",
                        principalColumn: "MaDonHang",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChiTietDonHangs_KhoaHocs_MaKhoaHoc",
                        column: x => x.MaKhoaHoc,
                        principalTable: "KhoaHocs",
                        principalColumn: "MaKhoaHoc",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GhiDanhs",
                columns: table => new
                {
                    MaGhiDanh = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaHocVien = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaKhoaHoc = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaDonHang = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    GhiDanhLuc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TrangThaiTruyCap = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ThuHoiLuc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GhiDanhs", x => x.MaGhiDanh);
                    table.ForeignKey(
                        name: "FK_GhiDanhs_DonHangs_MaDonHang",
                        column: x => x.MaDonHang,
                        principalTable: "DonHangs",
                        principalColumn: "MaDonHang");
                    table.ForeignKey(
                        name: "FK_GhiDanhs_KhoaHocs_MaKhoaHoc",
                        column: x => x.MaKhoaHoc,
                        principalTable: "KhoaHocs",
                        principalColumn: "MaKhoaHoc",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GhiDanhs_NguoiDungs_MaHocVien",
                        column: x => x.MaHocVien,
                        principalTable: "NguoiDungs",
                        principalColumn: "MaNguoiDung",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HoanTiens",
                columns: table => new
                {
                    MaHoanTien = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaDonHang = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MaHocVien = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LyDo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoTienHoan = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    XuLyBoi = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    XuLyLuc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoanTiens", x => x.MaHoanTien);
                    table.ForeignKey(
                        name: "FK_HoanTiens_DonHangs_MaDonHang",
                        column: x => x.MaDonHang,
                        principalTable: "DonHangs",
                        principalColumn: "MaDonHang",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HoanTiens_NguoiDungs_MaHocVien",
                        column: x => x.MaHocVien,
                        principalTable: "NguoiDungs",
                        principalColumn: "MaNguoiDung",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HoanTiens_NguoiDungs_XuLyBoi",
                        column: x => x.XuLyBoi,
                        principalTable: "NguoiDungs",
                        principalColumn: "MaNguoiDung");
                });

            migrationBuilder.CreateTable(
                name: "SoCaiKeToans",
                columns: table => new
                {
                    MaBuToan = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaDonHang = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MaGiangVien = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaKhoaHoc = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DoanhThuGop = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PhiNenTang = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ThuNhapGiangVien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NgayGiaiPhong = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DaGiaiPhong = table.Column<bool>(type: "bit", nullable: false),
                    GiaiPhongLuc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoCaiKeToans", x => x.MaBuToan);
                    table.ForeignKey(
                        name: "FK_SoCaiKeToans_DonHangs_MaDonHang",
                        column: x => x.MaDonHang,
                        principalTable: "DonHangs",
                        principalColumn: "MaDonHang",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SoCaiKeToans_KhoaHocs_MaKhoaHoc",
                        column: x => x.MaKhoaHoc,
                        principalTable: "KhoaHocs",
                        principalColumn: "MaKhoaHoc",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SoCaiKeToans_NguoiDungs_MaGiangVien",
                        column: x => x.MaGiangVien,
                        principalTable: "NguoiDungs",
                        principalColumn: "MaNguoiDung",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BaiGiangs",
                columns: table => new
                {
                    MaBaiGiang = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaChuong = table.Column<int>(type: "int", nullable: false),
                    TieuDe = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    LoaiBaiGiang = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MaTaiNguyen = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThoiLuong = table.Column<int>(type: "int", nullable: true),
                    ThuTu = table.Column<int>(type: "int", nullable: false),
                    XemMienPhi = table.Column<bool>(type: "bit", nullable: false),
                    TyLeXemToiThieu = table.Column<int>(type: "int", nullable: false),
                    DangKhoa = table.Column<bool>(type: "bit", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaiGiangs", x => x.MaBaiGiang);
                    table.ForeignKey(
                        name: "FK_BaiGiangs_Chuongs_MaChuong",
                        column: x => x.MaChuong,
                        principalTable: "Chuongs",
                        principalColumn: "MaChuong",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BaiGiangs_TaiNguyenSos_MaTaiNguyen",
                        column: x => x.MaTaiNguyen,
                        principalTable: "TaiNguyenSos",
                        principalColumn: "MaTaiNguyen");
                });

            migrationBuilder.CreateTable(
                name: "DiemDanhs",
                columns: table => new
                {
                    MaDiemDanh = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaLichHoc = table.Column<int>(type: "int", nullable: false),
                    MaHocVien = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaKhoaHoc = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NgayLichHoc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DiemDanhLuc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ThoiGianXemThucTeGiay = table.Column<int>(type: "int", nullable: false),
                    HopLe = table.Column<bool>(type: "bit", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiemDanhs", x => x.MaDiemDanh);
                    table.ForeignKey(
                        name: "FK_DiemDanhs_KhoaHocs_MaKhoaHoc",
                        column: x => x.MaKhoaHoc,
                        principalTable: "KhoaHocs",
                        principalColumn: "MaKhoaHoc",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DiemDanhs_LichHocs_MaLichHoc",
                        column: x => x.MaLichHoc,
                        principalTable: "LichHocs",
                        principalColumn: "MaLichHoc",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DiemDanhs_NguoiDungs_MaHocVien",
                        column: x => x.MaHocVien,
                        principalTable: "NguoiDungs",
                        principalColumn: "MaNguoiDung",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BaiKiemTras",
                columns: table => new
                {
                    MaBaiKiemTra = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaBaiGiang = table.Column<int>(type: "int", nullable: false),
                    TieuDe = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DiemDat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GioiHanThoiGianPhut = table.Column<int>(type: "int", nullable: true),
                    SoLanToiDa = table.Column<int>(type: "int", nullable: true),
                    XaoTronNgauNhien = table.Column<bool>(type: "bit", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaiKiemTras", x => x.MaBaiKiemTra);
                    table.ForeignKey(
                        name: "FK_BaiKiemTras_BaiGiangs_MaBaiGiang",
                        column: x => x.MaBaiGiang,
                        principalTable: "BaiGiangs",
                        principalColumn: "MaBaiGiang",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChuDeHoiDaps",
                columns: table => new
                {
                    MaChuDe = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaBaiGiang = table.Column<int>(type: "int", nullable: false),
                    MaHocVien = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TieuDe = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    NoiDungChiTiet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoLuotUpvote = table.Column<int>(type: "int", nullable: false),
                    DaGiaiDap = table.Column<bool>(type: "bit", nullable: false),
                    DaXoa = table.Column<bool>(type: "bit", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChuDeHoiDaps", x => x.MaChuDe);
                    table.ForeignKey(
                        name: "FK_ChuDeHoiDaps_BaiGiangs_MaBaiGiang",
                        column: x => x.MaBaiGiang,
                        principalTable: "BaiGiangs",
                        principalColumn: "MaBaiGiang",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChuDeHoiDaps_NguoiDungs_MaHocVien",
                        column: x => x.MaHocVien,
                        principalTable: "NguoiDungs",
                        principalColumn: "MaNguoiDung",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GhiChus",
                columns: table => new
                {
                    MaGhiChu = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaHocVien = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaBaiGiang = table.Column<int>(type: "int", nullable: false),
                    ThoiDiem = table.Column<int>(type: "int", nullable: false),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GhiChus", x => x.MaGhiChu);
                    table.ForeignKey(
                        name: "FK_GhiChus_BaiGiangs_MaBaiGiang",
                        column: x => x.MaBaiGiang,
                        principalTable: "BaiGiangs",
                        principalColumn: "MaBaiGiang",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GhiChus_NguoiDungs_MaHocVien",
                        column: x => x.MaHocVien,
                        principalTable: "NguoiDungs",
                        principalColumn: "MaNguoiDung",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ThongKeThoatBaiHocs",
                columns: table => new
                {
                    MaBaiGiang = table.Column<int>(type: "int", nullable: false),
                    SoLuotXem = table.Column<int>(type: "int", nullable: false),
                    SoLuotHoanThanh = table.Column<int>(type: "int", nullable: false),
                    ThoiGianXemTrungBinhGiay = table.Column<int>(type: "int", nullable: false),
                    TyLeThoat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CapNhatLanCuoi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThongKeThoatBaiHocs", x => x.MaBaiGiang);
                    table.ForeignKey(
                        name: "FK_ThongKeThoatBaiHocs_BaiGiangs_MaBaiGiang",
                        column: x => x.MaBaiGiang,
                        principalTable: "BaiGiangs",
                        principalColumn: "MaBaiGiang",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TienDoHocTaps",
                columns: table => new
                {
                    MaTienDo = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaGhiDanh = table.Column<int>(type: "int", nullable: false),
                    MaBaiGiang = table.Column<int>(type: "int", nullable: false),
                    ThoiGianXemGiay = table.Column<int>(type: "int", nullable: false),
                    ViTriXemCuoi = table.Column<int>(type: "int", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    HoanThanhLuc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TienDoHocTaps", x => x.MaTienDo);
                    table.ForeignKey(
                        name: "FK_TienDoHocTaps_BaiGiangs_MaBaiGiang",
                        column: x => x.MaBaiGiang,
                        principalTable: "BaiGiangs",
                        principalColumn: "MaBaiGiang",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TienDoHocTaps_GhiDanhs_MaGhiDanh",
                        column: x => x.MaGhiDanh,
                        principalTable: "GhiDanhs",
                        principalColumn: "MaGhiDanh",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CauHois",
                columns: table => new
                {
                    MaCauHoi = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaBaiKiemTra = table.Column<int>(type: "int", nullable: false),
                    NoiDungCauHoi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LoaiCauHoi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    GiaiThich = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThuTu = table.Column<int>(type: "int", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CauHois", x => x.MaCauHoi);
                    table.ForeignKey(
                        name: "FK_CauHois_BaiKiemTras_MaBaiKiemTra",
                        column: x => x.MaBaiKiemTra,
                        principalTable: "BaiKiemTras",
                        principalColumn: "MaBaiKiemTra",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LanLamBais",
                columns: table => new
                {
                    MaLanLam = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaHocVien = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaBaiKiemTra = table.Column<int>(type: "int", nullable: false),
                    DiemSo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TongCauHoi = table.Column<int>(type: "int", nullable: false),
                    SoCauDung = table.Column<int>(type: "int", nullable: false),
                    DaDat = table.Column<bool>(type: "bit", nullable: false),
                    ThoiGianLamGiay = table.Column<int>(type: "int", nullable: true),
                    BatDauLamLuc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NopBaiLuc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanLamBais", x => x.MaLanLam);
                    table.ForeignKey(
                        name: "FK_LanLamBais_BaiKiemTras_MaBaiKiemTra",
                        column: x => x.MaBaiKiemTra,
                        principalTable: "BaiKiemTras",
                        principalColumn: "MaBaiKiemTra",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LanLamBais_NguoiDungs_MaHocVien",
                        column: x => x.MaHocVien,
                        principalTable: "NguoiDungs",
                        principalColumn: "MaNguoiDung",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TraLoiHoiDaps",
                columns: table => new
                {
                    MaTraLoi = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaChuDe = table.Column<int>(type: "int", nullable: false),
                    MaNguoiDung = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NoiDungChiTiet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DuocGhimBoi = table.Column<bool>(type: "bit", nullable: false),
                    SoLuotUpvote = table.Column<int>(type: "int", nullable: false),
                    DaXoa = table.Column<bool>(type: "bit", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TraLoiHoiDaps", x => x.MaTraLoi);
                    table.ForeignKey(
                        name: "FK_TraLoiHoiDaps_ChuDeHoiDaps_MaChuDe",
                        column: x => x.MaChuDe,
                        principalTable: "ChuDeHoiDaps",
                        principalColumn: "MaChuDe",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TraLoiHoiDaps_NguoiDungs_MaNguoiDung",
                        column: x => x.MaNguoiDung,
                        principalTable: "NguoiDungs",
                        principalColumn: "MaNguoiDung",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LuaChonDapAns",
                columns: table => new
                {
                    MaLuaChon = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaCauHoi = table.Column<int>(type: "int", nullable: false),
                    NoiDungLuaChon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LaDapAnDung = table.Column<bool>(type: "bit", nullable: false),
                    ThuTu = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LuaChonDapAns", x => x.MaLuaChon);
                    table.ForeignKey(
                        name: "FK_LuaChonDapAns_CauHois_MaCauHoi",
                        column: x => x.MaCauHoi,
                        principalTable: "CauHois",
                        principalColumn: "MaCauHoi",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DapAnHocViens",
                columns: table => new
                {
                    MaDapAnChon = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaLanLam = table.Column<int>(type: "int", nullable: false),
                    MaCauHoi = table.Column<int>(type: "int", nullable: false),
                    MaLuaChonDaChon = table.Column<int>(type: "int", nullable: true),
                    LaDapAnDung = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DapAnHocViens", x => x.MaDapAnChon);
                    table.ForeignKey(
                        name: "FK_DapAnHocViens_CauHois_MaCauHoi",
                        column: x => x.MaCauHoi,
                        principalTable: "CauHois",
                        principalColumn: "MaCauHoi",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DapAnHocViens_LanLamBais_MaLanLam",
                        column: x => x.MaLanLam,
                        principalTable: "LanLamBais",
                        principalColumn: "MaLanLam",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DapAnHocViens_LuaChonDapAns_MaLuaChonDaChon",
                        column: x => x.MaLuaChonDaChon,
                        principalTable: "LuaChonDapAns",
                        principalColumn: "MaLuaChon");
                });

            migrationBuilder.InsertData(
                table: "CauHinhHeThongs",
                columns: new[] { "KhoaCauHinh", "CapNhatBoi", "GiaTriCauHinh", "KieuDuLieu", "MoTa", "NgayCapNhat" },
                values: new object[,]
                {
                    { "DiemToiThieuRutTien", null, "100000", "Decimal", "Số dư tối thiểu (VNĐ) để giảng viên có thể rút", null },
                    { "MaxUploadSizeMB", null, "2048", "Int", "Giới hạn dung lượng tệp tải lên (MB)", null },
                    { "PhiNenTang", null, "30", "Decimal", "Tỷ lệ % phí nền tảng giữ lại từ mỗi đơn hàng", null },
                    { "ThoiGianGiuTienNgay", null, "30", "Int", "Số ngày giữ tiền trước khi giải phóng cho giảng viên", null }
                });

            migrationBuilder.InsertData(
                table: "VaiTros",
                columns: new[] { "MaVaiTro", "MoTa", "NgayTao", "TenVaiTro" },
                values: new object[,]
                {
                    { 1, "Học viên – quyền truy cập khóa học đã mua", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Student" },
                    { 2, "Giảng viên – quyền tạo và quản lý khóa học", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Instructor" },
                    { 3, "Quản trị viên – toàn quyền hệ thống", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin" },
                    { 4, "Giám đốc tài chính – duyệt rút tiền và hoàn tiền", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "CFO" },
                    { 5, "Giám đốc marketing – quản lý chiến dịch", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "CMO" },
                    { 6, "Kiểm duyệt viên – duyệt nội dung khóa học", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Moderator" },
                    { 7, "Nhân viên hỗ trợ khách hàng", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "CS" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BaiGiangs_MaChuong",
                table: "BaiGiangs",
                column: "MaChuong");

            migrationBuilder.CreateIndex(
                name: "IX_BaiGiangs_MaTaiNguyen",
                table: "BaiGiangs",
                column: "MaTaiNguyen");

            migrationBuilder.CreateIndex(
                name: "IX_BaiKiemTras_MaBaiGiang",
                table: "BaiKiemTras",
                column: "MaBaiGiang");

            migrationBuilder.CreateIndex(
                name: "IX_CauHois_MaBaiKiemTra",
                table: "CauHois",
                column: "MaBaiKiemTra");

            migrationBuilder.CreateIndex(
                name: "IX_ChienDichs_TaoBoi",
                table: "ChienDichs",
                column: "TaoBoi");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietDonHangs_MaDonHang",
                table: "ChiTietDonHangs",
                column: "MaDonHang");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietDonHangs_MaKhoaHoc",
                table: "ChiTietDonHangs",
                column: "MaKhoaHoc");

            migrationBuilder.CreateIndex(
                name: "IX_ChuDeHoiDaps_MaBaiGiang",
                table: "ChuDeHoiDaps",
                column: "MaBaiGiang");

            migrationBuilder.CreateIndex(
                name: "IX_ChuDeHoiDaps_MaHocVien",
                table: "ChuDeHoiDaps",
                column: "MaHocVien");

            migrationBuilder.CreateIndex(
                name: "IX_ChungChis_MaHocVien",
                table: "ChungChis",
                column: "MaHocVien");

            migrationBuilder.CreateIndex(
                name: "IX_ChungChis_MaKhoaHoc",
                table: "ChungChis",
                column: "MaKhoaHoc");

            migrationBuilder.CreateIndex(
                name: "IX_ChuoiHocLienTieps_MaHocVien",
                table: "ChuoiHocLienTieps",
                column: "MaHocVien");

            migrationBuilder.CreateIndex(
                name: "IX_Chuongs_MaKhoaHoc",
                table: "Chuongs",
                column: "MaKhoaHoc");

            migrationBuilder.CreateIndex(
                name: "IX_DanhGias_MaHocVien",
                table: "DanhGias",
                column: "MaHocVien");

            migrationBuilder.CreateIndex(
                name: "IX_DanhGias_MaKhoaHoc",
                table: "DanhGias",
                column: "MaKhoaHoc");

            migrationBuilder.CreateIndex(
                name: "IX_DanhMucs_MaDanhMucCha",
                table: "DanhMucs",
                column: "MaDanhMucCha");

            migrationBuilder.CreateIndex(
                name: "IX_DapAnHocViens_MaCauHoi",
                table: "DapAnHocViens",
                column: "MaCauHoi");

            migrationBuilder.CreateIndex(
                name: "IX_DapAnHocViens_MaLanLam",
                table: "DapAnHocViens",
                column: "MaLanLam");

            migrationBuilder.CreateIndex(
                name: "IX_DapAnHocViens_MaLuaChonDaChon",
                table: "DapAnHocViens",
                column: "MaLuaChonDaChon");

            migrationBuilder.CreateIndex(
                name: "IX_DiemDanhs_MaHocVien",
                table: "DiemDanhs",
                column: "MaHocVien");

            migrationBuilder.CreateIndex(
                name: "IX_DiemDanhs_MaKhoaHoc",
                table: "DiemDanhs",
                column: "MaKhoaHoc");

            migrationBuilder.CreateIndex(
                name: "IX_DiemDanhs_MaLichHoc",
                table: "DiemDanhs",
                column: "MaLichHoc");

            migrationBuilder.CreateIndex(
                name: "IX_DonHangs_MaGiamGia",
                table: "DonHangs",
                column: "MaGiamGia");

            migrationBuilder.CreateIndex(
                name: "IX_DonHangs_MaHocVien",
                table: "DonHangs",
                column: "MaHocVien");

            migrationBuilder.CreateIndex(
                name: "IX_GhiChus_MaBaiGiang",
                table: "GhiChus",
                column: "MaBaiGiang");

            migrationBuilder.CreateIndex(
                name: "IX_GhiChus_MaHocVien",
                table: "GhiChus",
                column: "MaHocVien");

            migrationBuilder.CreateIndex(
                name: "IX_GhiDanhs_MaDonHang",
                table: "GhiDanhs",
                column: "MaDonHang");

            migrationBuilder.CreateIndex(
                name: "IX_GhiDanhs_MaHocVien",
                table: "GhiDanhs",
                column: "MaHocVien");

            migrationBuilder.CreateIndex(
                name: "IX_GhiDanhs_MaKhoaHoc",
                table: "GhiDanhs",
                column: "MaKhoaHoc");

            migrationBuilder.CreateIndex(
                name: "IX_GioHangs_MaHocVien",
                table: "GioHangs",
                column: "MaHocVien");

            migrationBuilder.CreateIndex(
                name: "IX_HoanTiens_MaDonHang",
                table: "HoanTiens",
                column: "MaDonHang");

            migrationBuilder.CreateIndex(
                name: "IX_HoanTiens_MaHocVien",
                table: "HoanTiens",
                column: "MaHocVien");

            migrationBuilder.CreateIndex(
                name: "IX_HoanTiens_XuLyBoi",
                table: "HoanTiens",
                column: "XuLyBoi");

            migrationBuilder.CreateIndex(
                name: "IX_KhoaHocs_MaDanhMuc",
                table: "KhoaHocs",
                column: "MaDanhMuc");

            migrationBuilder.CreateIndex(
                name: "IX_KhoaHocs_MaGiangVien",
                table: "KhoaHocs",
                column: "MaGiangVien");

            migrationBuilder.CreateIndex(
                name: "IX_KhoaHocs_MaVideoGioiThieu",
                table: "KhoaHocs",
                column: "MaVideoGioiThieu");

            migrationBuilder.CreateIndex(
                name: "IX_LanLamBais_MaBaiKiemTra",
                table: "LanLamBais",
                column: "MaBaiKiemTra");

            migrationBuilder.CreateIndex(
                name: "IX_LanLamBais_MaHocVien",
                table: "LanLamBais",
                column: "MaHocVien");

            migrationBuilder.CreateIndex(
                name: "IX_LichHocs_MaHocVien",
                table: "LichHocs",
                column: "MaHocVien");

            migrationBuilder.CreateIndex(
                name: "IX_LichHocs_MaKhoaHoc",
                table: "LichHocs",
                column: "MaKhoaHoc");

            migrationBuilder.CreateIndex(
                name: "IX_LuaChonDapAns_MaCauHoi",
                table: "LuaChonDapAns",
                column: "MaCauHoi");

            migrationBuilder.CreateIndex(
                name: "IX_MaGiamGia_MaChienDich",
                table: "MaGiamGia",
                column: "MaChienDich");

            migrationBuilder.CreateIndex(
                name: "IX_MaGiamGia_MaGiangVien",
                table: "MaGiamGia",
                column: "MaGiangVien");

            migrationBuilder.CreateIndex(
                name: "IX_MaHoaVideos_MaTaiNguyen",
                table: "MaHoaVideos",
                column: "MaTaiNguyen");

            migrationBuilder.CreateIndex(
                name: "IX_MaOtps_MaNguoiDung",
                table: "MaOtps",
                column: "MaNguoiDung");

            migrationBuilder.CreateIndex(
                name: "IX_MatHangGioHangs_MaGioHang",
                table: "MatHangGioHangs",
                column: "MaGioHang");

            migrationBuilder.CreateIndex(
                name: "IX_MatHangGioHangs_MaKhoaHoc",
                table: "MatHangGioHangs",
                column: "MaKhoaHoc");

            migrationBuilder.CreateIndex(
                name: "IX_NhanKhoaHocs_MaNhan",
                table: "NhanKhoaHocs",
                column: "MaNhan");

            migrationBuilder.CreateIndex(
                name: "IX_PhanHoiPhieus_MaNguoiGui",
                table: "PhanHoiPhieus",
                column: "MaNguoiGui");

            migrationBuilder.CreateIndex(
                name: "IX_PhanHoiPhieus_MaPhieu",
                table: "PhanHoiPhieus",
                column: "MaPhieu");

            migrationBuilder.CreateIndex(
                name: "IX_PhienLamViecs_MaNguoiDung",
                table: "PhienLamViecs",
                column: "MaNguoiDung");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuHoTros_MaNguoiGuiYeuCau",
                table: "PhieuHoTros",
                column: "MaNguoiGuiYeuCau");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuHoTros_PhanCongCho",
                table: "PhieuHoTros",
                column: "PhanCongCho");

            migrationBuilder.CreateIndex(
                name: "IX_SoCaiKeToans_MaDonHang",
                table: "SoCaiKeToans",
                column: "MaDonHang");

            migrationBuilder.CreateIndex(
                name: "IX_SoCaiKeToans_MaGiangVien",
                table: "SoCaiKeToans",
                column: "MaGiangVien");

            migrationBuilder.CreateIndex(
                name: "IX_SoCaiKeToans_MaKhoaHoc",
                table: "SoCaiKeToans",
                column: "MaKhoaHoc");

            migrationBuilder.CreateIndex(
                name: "IX_TaiNguyenSos_TaiLenBoi",
                table: "TaiNguyenSos",
                column: "TaiLenBoi");

            migrationBuilder.CreateIndex(
                name: "IX_ThongBaoKhoaHocs_MaGiangVien",
                table: "ThongBaoKhoaHocs",
                column: "MaGiangVien");

            migrationBuilder.CreateIndex(
                name: "IX_ThongBaoKhoaHocs_MaKhoaHoc",
                table: "ThongBaoKhoaHocs",
                column: "MaKhoaHoc");

            migrationBuilder.CreateIndex(
                name: "IX_TienDoHocTaps_MaBaiGiang",
                table: "TienDoHocTaps",
                column: "MaBaiGiang");

            migrationBuilder.CreateIndex(
                name: "IX_TienDoHocTaps_MaGhiDanh",
                table: "TienDoHocTaps",
                column: "MaGhiDanh");

            migrationBuilder.CreateIndex(
                name: "IX_TongHopDoanhThuNgays_MaKhoaHoc",
                table: "TongHopDoanhThuNgays",
                column: "MaKhoaHoc");

            migrationBuilder.CreateIndex(
                name: "IX_TraLoiHoiDaps_MaChuDe",
                table: "TraLoiHoiDaps",
                column: "MaChuDe");

            migrationBuilder.CreateIndex(
                name: "IX_TraLoiHoiDaps_MaNguoiDung",
                table: "TraLoiHoiDaps",
                column: "MaNguoiDung");

            migrationBuilder.CreateIndex(
                name: "IX_TuongTacNguoiDungs_MaNguoiDung",
                table: "TuongTacNguoiDungs",
                column: "MaNguoiDung");

            migrationBuilder.CreateIndex(
                name: "IX_VaiTroNguoiDungs_MaNguoiDung",
                table: "VaiTroNguoiDungs",
                column: "MaNguoiDung");

            migrationBuilder.CreateIndex(
                name: "IX_VaiTroNguoiDungs_MaVaiTro",
                table: "VaiTroNguoiDungs",
                column: "MaVaiTro");

            migrationBuilder.CreateIndex(
                name: "IX_YeuCauRutTiens_DuyetBoi",
                table: "YeuCauRutTiens",
                column: "DuyetBoi");

            migrationBuilder.CreateIndex(
                name: "IX_YeuCauRutTiens_MaGiangVien",
                table: "YeuCauRutTiens",
                column: "MaGiangVien");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CauHinhHeThongs");

            migrationBuilder.DropTable(
                name: "ChiTietDonHangs");

            migrationBuilder.DropTable(
                name: "ChungChis");

            migrationBuilder.DropTable(
                name: "ChuoiHocLienTieps");

            migrationBuilder.DropTable(
                name: "DanhGias");

            migrationBuilder.DropTable(
                name: "DapAnHocViens");

            migrationBuilder.DropTable(
                name: "DiemDanhs");

            migrationBuilder.DropTable(
                name: "GhiChus");

            migrationBuilder.DropTable(
                name: "HoanTiens");

            migrationBuilder.DropTable(
                name: "HoSoGiangViens");

            migrationBuilder.DropTable(
                name: "HoSoNhanViens");

            migrationBuilder.DropTable(
                name: "LanDangNhaps");

            migrationBuilder.DropTable(
                name: "MaHoaVideos");

            migrationBuilder.DropTable(
                name: "MaOtps");

            migrationBuilder.DropTable(
                name: "MatHangGioHangs");

            migrationBuilder.DropTable(
                name: "NhanKhoaHocs");

            migrationBuilder.DropTable(
                name: "NhatKyKiemToans");

            migrationBuilder.DropTable(
                name: "PhanHoiPhieus");

            migrationBuilder.DropTable(
                name: "PhienLamViecs");

            migrationBuilder.DropTable(
                name: "SoCaiKeToans");

            migrationBuilder.DropTable(
                name: "ThongBaoKhoaHocs");

            migrationBuilder.DropTable(
                name: "ThongKeThoatBaiHocs");

            migrationBuilder.DropTable(
                name: "TienDoHocTaps");

            migrationBuilder.DropTable(
                name: "TongHopDoanhThuNgays");

            migrationBuilder.DropTable(
                name: "TraLoiHoiDaps");

            migrationBuilder.DropTable(
                name: "TuongTacNguoiDungs");

            migrationBuilder.DropTable(
                name: "VaiTroNguoiDungs");

            migrationBuilder.DropTable(
                name: "YeuCauRutTiens");

            migrationBuilder.DropTable(
                name: "LanLamBais");

            migrationBuilder.DropTable(
                name: "LuaChonDapAns");

            migrationBuilder.DropTable(
                name: "LichHocs");

            migrationBuilder.DropTable(
                name: "GioHangs");

            migrationBuilder.DropTable(
                name: "NhanTuKhoas");

            migrationBuilder.DropTable(
                name: "PhieuHoTros");

            migrationBuilder.DropTable(
                name: "GhiDanhs");

            migrationBuilder.DropTable(
                name: "ChuDeHoiDaps");

            migrationBuilder.DropTable(
                name: "VaiTros");

            migrationBuilder.DropTable(
                name: "CauHois");

            migrationBuilder.DropTable(
                name: "DonHangs");

            migrationBuilder.DropTable(
                name: "BaiKiemTras");

            migrationBuilder.DropTable(
                name: "MaGiamGia");

            migrationBuilder.DropTable(
                name: "BaiGiangs");

            migrationBuilder.DropTable(
                name: "ChienDichs");

            migrationBuilder.DropTable(
                name: "Chuongs");

            migrationBuilder.DropTable(
                name: "KhoaHocs");

            migrationBuilder.DropTable(
                name: "DanhMucs");

            migrationBuilder.DropTable(
                name: "TaiNguyenSos");

            migrationBuilder.DropTable(
                name: "NguoiDungs");
        }
    }
}
