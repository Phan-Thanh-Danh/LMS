-- ============================================================
--  HỆ THỐNG LMS - NỀN TẢNG HỌC TRỰC TUYẾN
--  SQL Server Management Studio (T-SQL)
--  Phiên bản 1.0  |  Collation: Vietnamese_CI_AS
--  Tất cả tên bảng & cột theo tài liệu thiết kế tiếng Việt
-- ============================================================

USE master;
GO

IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'LMS_DB')
BEGIN
    CREATE DATABASE LMS_DB COLLATE Vietnamese_CI_AS;
    PRINT N'>>> Đã tạo database LMS_DB';
END
GO

USE LMS_DB;
GO

-- ============================================================
--  NHÓM 1 : QUẢN TRỊ ĐỊNH DANH & PHÂN QUYỀN (IAM)
-- ============================================================

-- ----------------------------------------------------------------
--  Bảng: NguoiDung
--  Lưu tài khoản tất cả người dùng (học viên, giảng viên, admin…)
-- ----------------------------------------------------------------
CREATE TABLE NguoiDung (
    MaNguoiDung         UNIQUEIDENTIFIER    NOT NULL DEFAULT NEWSEQUENTIALID(),
    Email               NVARCHAR(255)       NOT NULL,
    MatKhauBam          NVARCHAR(512)       NOT NULL,
    MuoiMatKhau         NVARCHAR(256)       NOT NULL,
    HoTen               NVARCHAR(200)       NOT NULL,
    DuongDanAnhDaiDien  NVARCHAR(500)       NULL,
    TieuSu              NVARCHAR(MAX)       NULL,
    DangHoatDong        BIT                 NOT NULL DEFAULT 1,
    DaXoa               BIT                 NOT NULL DEFAULT 0,
    EmailDaXacThuc      BIT                 NOT NULL DEFAULT 0,
    LaNhanVien          BIT                 NOT NULL DEFAULT 0,
    LanDangNhapCuoi     DATETIME            NULL,
    NgayTao             DATETIME            NOT NULL DEFAULT GETDATE(),
    NgayCapNhat         DATETIME            NULL,

    CONSTRAINT PK_NguoiDung     PRIMARY KEY (MaNguoiDung),
    CONSTRAINT UQ_NguoiDung_Email UNIQUE (Email)
);
GO

-- ----------------------------------------------------------------
--  Bảng: VaiTro
--  Danh mục vai trò theo mô hình RBAC
-- ----------------------------------------------------------------
CREATE TABLE VaiTro (
    MaVaiTro    INT             NOT NULL IDENTITY(1,1),
    TenVaiTro   NVARCHAR(100)   NOT NULL,
    MoTa        NVARCHAR(500)   NULL,
    NgayTao     DATETIME        NOT NULL DEFAULT GETDATE(),

    CONSTRAINT PK_VaiTro        PRIMARY KEY (MaVaiTro),
    CONSTRAINT UQ_VaiTro_Ten    UNIQUE (TenVaiTro)
);
GO

-- ----------------------------------------------------------------
--  Bảng: VaiTroNguoiDung
--  Liên kết nhiều-nhiều NguoiDung ↔ VaiTro
-- ----------------------------------------------------------------
CREATE TABLE VaiTroNguoiDung (
    MaVaiTroNguoiDung   INT                 NOT NULL IDENTITY(1,1),
    MaNguoiDung         UNIQUEIDENTIFIER    NOT NULL,
    MaVaiTro            INT                 NOT NULL,
    NgayGanVaiTro       DATETIME            NOT NULL DEFAULT GETDATE(),
    GanBoi              UNIQUEIDENTIFIER    NULL,

    CONSTRAINT PK_VaiTroNguoiDung           PRIMARY KEY (MaVaiTroNguoiDung),
    CONSTRAINT UQ_VaiTroNguoiDung_Cap       UNIQUE (MaNguoiDung, MaVaiTro),
    CONSTRAINT FK_VTNguoiDung_NguoiDung     FOREIGN KEY (MaNguoiDung)
        REFERENCES NguoiDung(MaNguoiDung)   ON DELETE NO ACTION,
    CONSTRAINT FK_VTNguoiDung_VaiTro        FOREIGN KEY (MaVaiTro)
        REFERENCES VaiTro(MaVaiTro)         ON DELETE NO ACTION,
    CONSTRAINT FK_VTNguoiDung_GanBoi        FOREIGN KEY (GanBoi)
        REFERENCES NguoiDung(MaNguoiDung)   ON DELETE NO ACTION
);
GO

-- ----------------------------------------------------------------
--  Bảng: MaOtp
--  Mã OTP/token một lần cho xác thực email, đặt lại mật khẩu
-- ----------------------------------------------------------------
CREATE TABLE MaOtp (
    MaToken     INT                 NOT NULL IDENTITY(1,1),
    MaNguoiDung UNIQUEIDENTIFIER    NOT NULL,
    GiaTriToken NVARCHAR(512)       NOT NULL,
    LoaiToken   NVARCHAR(50)        NOT NULL,   -- EmailVerify | PasswordReset | TwoFA
    HetHanLuc   DATETIME            NOT NULL,
    DaSuDung    BIT                 NOT NULL DEFAULT 0,
    NgayTao     DATETIME            NOT NULL DEFAULT GETDATE(),

    CONSTRAINT PK_MaOtp             PRIMARY KEY (MaToken),
    CONSTRAINT FK_MaOtp_NguoiDung   FOREIGN KEY (MaNguoiDung)
        REFERENCES NguoiDung(MaNguoiDung) ON DELETE CASCADE
);
GO

-- ----------------------------------------------------------------
--  Bảng: PhienLamViec
--  Quản lý phiên đăng nhập, hỗ trợ đa thiết bị & đăng xuất từ xa
-- ----------------------------------------------------------------
CREATE TABLE PhienLamViec (
    MaPhien             UNIQUEIDENTIFIER    NOT NULL DEFAULT NEWSEQUENTIALID(),
    MaNguoiDung         UNIQUEIDENTIFIER    NOT NULL,
    TokenLamMoi         NVARCHAR(512)       NOT NULL,
    DiaChiIP            NVARCHAR(50)        NULL,
    ThongTinThietBi     NVARCHAR(500)       NULL,
    ViTri               NVARCHAR(200)       NULL,
    HetHanLuc           DATETIME            NOT NULL,
    DaThuHoi            BIT                 NOT NULL DEFAULT 0,
    NgayTao             DATETIME            NOT NULL DEFAULT GETDATE(),

    CONSTRAINT PK_PhienLamViec          PRIMARY KEY (MaPhien),
    CONSTRAINT FK_PhienLamViec_NguoiDung FOREIGN KEY (MaNguoiDung)
        REFERENCES NguoiDung(MaNguoiDung) ON DELETE CASCADE
);
GO

-- ----------------------------------------------------------------
--  Bảng: NhatKyKiemToan
--  Ghi nhận hành động quan trọng phục vụ kiểm tra bảo mật
-- ----------------------------------------------------------------
CREATE TABLE NhatKyKiemToan (
    MaLog           BIGINT              NOT NULL IDENTITY(1,1),
    MaNguoiDung     UNIQUEIDENTIFIER    NULL,
    HanhDong        NVARCHAR(200)       NOT NULL,
    LoaiDoiTuong    NVARCHAR(100)       NULL,
    MaDoiTuong      NVARCHAR(100)       NULL,
    GiaTriCu        NVARCHAR(MAX)       NULL,   -- JSON
    GiaTriMoi       NVARCHAR(MAX)       NULL,   -- JSON
    DiaChiIP        NVARCHAR(50)        NULL,
    TrinhDuyet      NVARCHAR(500)       NULL,
    ThoiDiem        DATETIME            NOT NULL DEFAULT GETDATE(),

    CONSTRAINT PK_NhatKyKiemToan        PRIMARY KEY (MaLog),
    CONSTRAINT FK_NhatKy_NguoiDung      FOREIGN KEY (MaNguoiDung)
        REFERENCES NguoiDung(MaNguoiDung) ON DELETE SET NULL
);
GO

-- ============================================================
--  NHÓM 2 : DỮ LIỆU GỐC & CẤU HÌNH HỆ THỐNG
-- ============================================================

-- ----------------------------------------------------------------
--  Bảng: DanhMuc
--  Phân loại khóa học, hỗ trợ cấu trúc cây cha-con (đệ quy)
-- ----------------------------------------------------------------
CREATE TABLE DanhMuc (
    MaDanhMuc       INT             NOT NULL IDENTITY(1,1),
    MaDanhMucCha    INT             NULL,
    TenDanhMuc      NVARCHAR(200)   NOT NULL,
    DuongDanURL     VARCHAR(200)    NOT NULL,
    ThuTuHienThi    INT             NOT NULL DEFAULT 0,
    DangHienThi     BIT             NOT NULL DEFAULT 1,
    DuongDanIcon    NVARCHAR(500)   NULL,
    NgayTao         DATETIME        NOT NULL DEFAULT GETDATE(),

    CONSTRAINT PK_DanhMuc           PRIMARY KEY (MaDanhMuc),
    CONSTRAINT UQ_DanhMuc_URL       UNIQUE (DuongDanURL),
    CONSTRAINT FK_DanhMuc_ChaId     FOREIGN KEY (MaDanhMucCha)
        REFERENCES DanhMuc(MaDanhMuc) ON DELETE NO ACTION
);
GO

-- ----------------------------------------------------------------
--  Bảng: NhanTuKhoa
--  Nhãn từ khóa dùng phân loại và gợi ý khóa học
-- ----------------------------------------------------------------
CREATE TABLE NhanTuKhoa (
    MaNhan      INT             NOT NULL IDENTITY(1,1),
    TenNhan     NVARCHAR(100)   NOT NULL,
    DuongDanURL VARCHAR(100)    NOT NULL,
    SoLuotDung  INT             NOT NULL DEFAULT 0,

    CONSTRAINT PK_NhanTuKhoa        PRIMARY KEY (MaNhan),
    CONSTRAINT UQ_NhanTuKhoa_Ten    UNIQUE (TenNhan),
    CONSTRAINT UQ_NhanTuKhoa_URL    UNIQUE (DuongDanURL)
);
GO

-- ----------------------------------------------------------------
--  Bảng: CauHinhHeThong
--  Tham số cấu hình hệ thống thay đổi không cần sửa mã nguồn
-- ----------------------------------------------------------------
CREATE TABLE CauHinhHeThong (
    KhoaCauHinh     VARCHAR(200)        NOT NULL,
    GiaTriCauHinh   NVARCHAR(MAX)       NOT NULL,
    KieuDuLieu      VARCHAR(50)         NOT NULL,   -- Int | String | Boolean | JSON | Decimal
    MoTa            NVARCHAR(500)       NULL,
    NgayCapNhat     DATETIME            NULL,
    CapNhatBoi      UNIQUEIDENTIFIER    NULL,

    CONSTRAINT PK_CauHinhHeThong        PRIMARY KEY (KhoaCauHinh),
    CONSTRAINT FK_CauHinh_NguoiDung     FOREIGN KEY (CapNhatBoi)
        REFERENCES NguoiDung(MaNguoiDung) ON DELETE SET NULL
);
GO

-- ============================================================
--  NHÓM 3 : QUẢN TRỊ TÀI NGUYÊN SỐ (MEDIA)
-- ============================================================

-- ----------------------------------------------------------------
--  Bảng: TaiNguyenSo
--  Quản lý tệp đa phương tiện: video, PDF, ảnh, source code
-- ----------------------------------------------------------------
CREATE TABLE TaiNguyenSo (
    MaTaiNguyen     UNIQUEIDENTIFIER    NOT NULL DEFAULT NEWSEQUENTIALID(),
    TaiLenBoi       UNIQUEIDENTIFIER    NOT NULL,
    TenTep          NVARCHAR(500)       NOT NULL,
    LoaiTep         VARCHAR(50)         NOT NULL,   -- Video | PDF | Image | ZipCode | Audio
    KieuMIME        VARCHAR(100)        NOT NULL,
    DungLuongByte   BIGINT              NOT NULL,
    DuongDanLuuTru  NVARCHAR(1000)      NOT NULL,
    ThoiLuong       INT                 NULL,       -- giây, chỉ dùng cho Video
    TrangThai       VARCHAR(50)         NOT NULL DEFAULT 'Pending',  -- Pending | Processing | Ready | Failed
    NgayTao         DATETIME            NOT NULL DEFAULT GETDATE(),

    CONSTRAINT PK_TaiNguyenSo           PRIMARY KEY (MaTaiNguyen),
    CONSTRAINT FK_TaiNguyen_NguoiDung   FOREIGN KEY (TaiLenBoi)
        REFERENCES NguoiDung(MaNguoiDung) ON DELETE NO ACTION
);
GO

-- ----------------------------------------------------------------
--  Bảng: MaHoaVideo
--  Phiên bản video đã mã hóa nhiều độ phân giải phục vụ HLS
-- ----------------------------------------------------------------
CREATE TABLE MaHoaVideo (
    MaMaHoa         INT                 NOT NULL IDENTITY(1,1),
    MaTaiNguyen     UNIQUEIDENTIFIER    NOT NULL,
    DoPhanGiai      VARCHAR(20)         NOT NULL,   -- 1080p | 720p | 480p | 360p
    DuongDanHLS     NVARCHAR(1000)      NOT NULL,
    TocDoBitKbps    INT                 NULL,
    MaHoaLuc        DATETIME            NOT NULL DEFAULT GETDATE(),

    CONSTRAINT PK_MaHoaVideo            PRIMARY KEY (MaMaHoa),
    CONSTRAINT FK_MaHoaVideo_TaiNguyen  FOREIGN KEY (MaTaiNguyen)
        REFERENCES TaiNguyenSo(MaTaiNguyen) ON DELETE CASCADE
);
GO

-- ============================================================
--  NHÓM 4 : VÒNG ĐỜI KHÓA HỌC
-- ============================================================

-- ----------------------------------------------------------------
--  Bảng: KhoaHoc
--  Thông tin định danh và thương mại của toàn bộ khóa học
-- ----------------------------------------------------------------
CREATE TABLE KhoaHoc (
    MaKhoaHoc               UNIQUEIDENTIFIER    NOT NULL DEFAULT NEWSEQUENTIALID(),
    MaGiangVien             UNIQUEIDENTIFIER    NOT NULL,
    MaDanhMuc               INT                 NOT NULL,
    TieuDe                  NVARCHAR(500)       NOT NULL,
    DuongDanURL             VARCHAR(500)        NOT NULL,
    TieuDeNho               NVARCHAR(500)       NULL,
    MoTa                    NVARCHAR(MAX)       NULL,
    MucTieuDauRa            NVARCHAR(MAX)       NULL,   -- JSON array
    YeuCauDieuKien          NVARCHAR(MAX)       NULL,   -- JSON array
    TrinhDo                 VARCHAR(50)         NOT NULL,   -- Beginner | Intermediate | Advanced | AllLevels
    NgonNgu                 VARCHAR(50)         NOT NULL DEFAULT 'Vietnamese',
    Gia                     DECIMAL(18,2)       NOT NULL,
    DuongDanAnhDaiDien      NVARCHAR(1000)      NULL,
    MaVideoGioiThieu        UNIQUEIDENTIFIER    NULL,
    TongThoiLuong           INT                 NOT NULL DEFAULT 0,
    TongBaiGiang            INT                 NOT NULL DEFAULT 0,
    DiemDanhGiaTrungBinh    DECIMAL(3,2)        NOT NULL DEFAULT 0,
    TongDanhGia             INT                 NOT NULL DEFAULT 0,
    TongGhiDanh             INT                 NOT NULL DEFAULT 0,
    TrangThai               INT                 NOT NULL DEFAULT 0,
        -- 0=Draft | 1=Pending | 2=Published | 3=Rejected | 4=Archived
    GhiChuTuChoi            NVARCHAR(MAX)       NULL,
    XuatBanLuc              DATETIME            NULL,
    LuuTruLuc               DATETIME            NULL,
    DaXoa                   BIT                 NOT NULL DEFAULT 0,
    NgayTao                 DATETIME            NOT NULL DEFAULT GETDATE(),
    NgayCapNhat             DATETIME            NULL,

    CONSTRAINT PK_KhoaHoc               PRIMARY KEY (MaKhoaHoc),
    CONSTRAINT UQ_KhoaHoc_URL           UNIQUE (DuongDanURL),
    CONSTRAINT CHK_KhoaHoc_Gia          CHECK (Gia >= 0),
    CONSTRAINT FK_KhoaHoc_GiangVien     FOREIGN KEY (MaGiangVien)
        REFERENCES NguoiDung(MaNguoiDung)   ON DELETE NO ACTION,
    CONSTRAINT FK_KhoaHoc_DanhMuc       FOREIGN KEY (MaDanhMuc)
        REFERENCES DanhMuc(MaDanhMuc)       ON DELETE NO ACTION,
    CONSTRAINT FK_KhoaHoc_VideoGT       FOREIGN KEY (MaVideoGioiThieu)
        REFERENCES TaiNguyenSo(MaTaiNguyen) ON DELETE SET NULL
);
GO

-- ----------------------------------------------------------------
--  Bảng: NhanKhoaHoc
--  Liên kết nhiều-nhiều KhoaHoc ↔ NhanTuKhoa
-- ----------------------------------------------------------------
CREATE TABLE NhanKhoaHoc (
    MaKhoaHoc   UNIQUEIDENTIFIER    NOT NULL,
    MaNhan      INT                 NOT NULL,
    NgayTao     DATETIME            NOT NULL DEFAULT GETDATE(),

    CONSTRAINT PK_NhanKhoaHoc           PRIMARY KEY (MaKhoaHoc, MaNhan),
    CONSTRAINT FK_NhanKH_KhoaHoc        FOREIGN KEY (MaKhoaHoc)
        REFERENCES KhoaHoc(MaKhoaHoc)   ON DELETE CASCADE,
    CONSTRAINT FK_NhanKH_NhanTuKhoa     FOREIGN KEY (MaNhan)
        REFERENCES NhanTuKhoa(MaNhan)   ON DELETE CASCADE
);
GO

-- ----------------------------------------------------------------
--  Bảng: Chuong
--  Chương/phần lớn trong cấu trúc giáo trình (cấp 1)
-- ----------------------------------------------------------------
CREATE TABLE Chuong (
    MaChuong    INT                 NOT NULL IDENTITY(1,1),
    MaKhoaHoc   UNIQUEIDENTIFIER    NOT NULL,
    TieuDe      NVARCHAR(500)       NOT NULL,
    MoTa        NVARCHAR(MAX)       NULL,
    ThuTu       INT                 NOT NULL,
    NgayTao     DATETIME            NOT NULL DEFAULT GETDATE(),

    CONSTRAINT PK_Chuong            PRIMARY KEY (MaChuong),
    CONSTRAINT FK_Chuong_KhoaHoc    FOREIGN KEY (MaKhoaHoc)
        REFERENCES KhoaHoc(MaKhoaHoc) ON DELETE CASCADE
);
GO

-- ----------------------------------------------------------------
--  Bảng: BaiGiang
--  Bài giảng cụ thể trong từng chương (cấp 2 – đơn vị học nhỏ nhất)
-- ----------------------------------------------------------------
CREATE TABLE BaiGiang (
    MaBaiGiang          INT                 NOT NULL IDENTITY(1,1),
    MaChuong            INT                 NOT NULL,
    TieuDe              NVARCHAR(500)       NOT NULL,
    LoaiBaiGiang        VARCHAR(50)         NOT NULL,   -- Video | Quiz | Document | Assignment
    MaTaiNguyen         UNIQUEIDENTIFIER    NULL,
    MoTa                NVARCHAR(MAX)       NULL,
    ThoiLuong           INT                 NULL,       -- giây
    ThuTu               INT                 NOT NULL,
    XemMienPhi          BIT                 NOT NULL DEFAULT 0,
    TyLeXemToiThieu     INT                 NOT NULL DEFAULT 80,
    DangKhoa            BIT                 NOT NULL DEFAULT 0,
    NgayTao             DATETIME            NOT NULL DEFAULT GETDATE(),

    CONSTRAINT PK_BaiGiang              PRIMARY KEY (MaBaiGiang),
    CONSTRAINT FK_BaiGiang_Chuong       FOREIGN KEY (MaChuong)
        REFERENCES Chuong(MaChuong)         ON DELETE CASCADE,
    CONSTRAINT FK_BaiGiang_TaiNguyen    FOREIGN KEY (MaTaiNguyen)
        REFERENCES TaiNguyenSo(MaTaiNguyen) ON DELETE SET NULL
);
GO

-- ----------------------------------------------------------------
--  Bảng: BaiKiemTra
--  Thông tin bài kiểm tra trắc nghiệm gắn với bài giảng
-- ----------------------------------------------------------------
CREATE TABLE BaiKiemTra (
    MaBaiKiemTra        INT         NOT NULL IDENTITY(1,1),
    MaBaiGiang          INT         NOT NULL,
    TieuDe              NVARCHAR(500) NOT NULL,
    DiemDat             DECIMAL(5,2)  NOT NULL DEFAULT 70,
    GioiHanThoiGianPhut INT         NULL,
    SoLanToiDa          INT         NULL,
    XaoTronNgauNhien    BIT         NOT NULL DEFAULT 1,
    NgayTao             DATETIME    NOT NULL DEFAULT GETDATE(),

    CONSTRAINT PK_BaiKiemTra            PRIMARY KEY (MaBaiKiemTra),
    CONSTRAINT FK_BaiKiemTra_BaiGiang   FOREIGN KEY (MaBaiGiang)
        REFERENCES BaiGiang(MaBaiGiang) ON DELETE CASCADE
);
GO

-- ----------------------------------------------------------------
--  Bảng: CauHoi
--  Ngân hàng câu hỏi của các bài kiểm tra
-- ----------------------------------------------------------------
CREATE TABLE CauHoi (
    MaCauHoi        INT             NOT NULL IDENTITY(1,1),
    MaBaiKiemTra    INT             NOT NULL,
    NoiDungCauHoi   NVARCHAR(MAX)   NOT NULL,
    LoaiCauHoi      VARCHAR(50)     NOT NULL,   -- SingleChoice | MultipleChoice | TrueFalse
    GiaiThich       NVARCHAR(MAX)   NULL,
    ThuTu           INT             NOT NULL,
    NgayTao         DATETIME        NOT NULL DEFAULT GETDATE(),

    CONSTRAINT PK_CauHoi                PRIMARY KEY (MaCauHoi),
    CONSTRAINT FK_CauHoi_BaiKiemTra     FOREIGN KEY (MaBaiKiemTra)
        REFERENCES BaiKiemTra(MaBaiKiemTra) ON DELETE CASCADE
);
GO

-- ----------------------------------------------------------------
--  Bảng: LuaChonDapAn
--  Các lựa chọn đáp án của từng câu hỏi trắc nghiệm
-- ----------------------------------------------------------------
CREATE TABLE LuaChonDapAn (
    MaLuaChon       INT             NOT NULL IDENTITY(1,1),
    MaCauHoi        INT             NOT NULL,
    NoiDungLuaChon  NVARCHAR(MAX)   NOT NULL,
    LaDapAnDung     BIT             NOT NULL DEFAULT 0,
    ThuTu           INT             NOT NULL,

    CONSTRAINT PK_LuaChonDapAn          PRIMARY KEY (MaLuaChon),
    CONSTRAINT FK_LuaChon_CauHoi        FOREIGN KEY (MaCauHoi)
        REFERENCES CauHoi(MaCauHoi)     ON DELETE CASCADE
);
GO

-- ============================================================
--  NHÓM 5 : GIAO DỊCH & GHI DANH
-- ============================================================

-- ----------------------------------------------------------------
--  Bảng: MaGiamGia   (tạo TRƯỚC DonHang vì DonHang FK tới đây)
--  Mã giảm giá của nền tảng hoặc giảng viên
-- ----------------------------------------------------------------
CREATE TABLE MaGiamGia (
    MaGiamGia           VARCHAR(100)        NOT NULL,
    MaChienDich         INT                 NULL,   -- FK thêm sau khi có ChienDich
    MaGiangVien         UNIQUEIDENTIFIER    NULL,
    LoaiGiamGia         INT                 NOT NULL,   -- 1=% | 2=Số tiền cố định
    GiaTriGiam          DECIMAL(18,2)       NOT NULL,
    GiamToiDa           DECIMAL(18,2)       NULL,
    DonHangToiThieu     DECIMAL(18,2)       NOT NULL DEFAULT 0,
    GioiHanLuotDung     INT                 NULL,
    SoLuotDaDung        INT                 NOT NULL DEFAULT 0,
    NgayBatDau          DATETIME            NOT NULL,
    NgayKetThuc         DATETIME            NOT NULL,
    DangHoatDong        BIT                 NOT NULL DEFAULT 1,
    NgayTao             DATETIME            NOT NULL DEFAULT GETDATE(),

    CONSTRAINT PK_MaGiamGia             PRIMARY KEY (MaGiamGia),
    CONSTRAINT CHK_MaGiamGia_GiaTri     CHECK (GiaTriGiam > 0),
    CONSTRAINT FK_MaGiamGia_GiangVien   FOREIGN KEY (MaGiangVien)
        REFERENCES NguoiDung(MaNguoiDung) ON DELETE SET NULL
);
GO

-- ----------------------------------------------------------------
--  Bảng: GioHang
--  Giỏ hàng tạm thời của học viên (1 học viên = 1 giỏ)
-- ----------------------------------------------------------------
CREATE TABLE GioHang (
    MaGioHang   INT                 NOT NULL IDENTITY(1,1),
    MaHocVien   UNIQUEIDENTIFIER    NOT NULL,
    NgayTao     DATETIME            NOT NULL DEFAULT GETDATE(),
    NgayCapNhat DATETIME            NULL,

    CONSTRAINT PK_GioHang               PRIMARY KEY (MaGioHang),
    CONSTRAINT UQ_GioHang_HocVien       UNIQUE (MaHocVien),
    CONSTRAINT FK_GioHang_HocVien       FOREIGN KEY (MaHocVien)
        REFERENCES NguoiDung(MaNguoiDung) ON DELETE CASCADE
);
GO

-- ----------------------------------------------------------------
--  Bảng: MatHangGioHang
--  Các khóa học nằm trong giỏ hàng
-- ----------------------------------------------------------------
CREATE TABLE MatHangGioHang (
    MaMatHang   INT                 NOT NULL IDENTITY(1,1),
    MaGioHang   INT                 NOT NULL,
    MaKhoaHoc   UNIQUEIDENTIFIER    NOT NULL,
    ThemVaoLuc  DATETIME            NOT NULL DEFAULT GETDATE(),

    CONSTRAINT PK_MatHangGioHang        PRIMARY KEY (MaMatHang),
    CONSTRAINT FK_MatHang_GioHang       FOREIGN KEY (MaGioHang)
        REFERENCES GioHang(MaGioHang)   ON DELETE CASCADE,
    CONSTRAINT FK_MatHang_KhoaHoc       FOREIGN KEY (MaKhoaHoc)
        REFERENCES KhoaHoc(MaKhoaHoc)   ON DELETE NO ACTION
);
GO

-- ----------------------------------------------------------------
--  Bảng: DonHang
--  Đơn hàng thanh toán, ghi nhận giao dịch
-- ----------------------------------------------------------------
CREATE TABLE DonHang (
    MaDonHang               VARCHAR(50)         NOT NULL,   -- ORD-YYYYMMDD-HEX
    MaHocVien               UNIQUEIDENTIFIER    NOT NULL,
    MaGiamGia               VARCHAR(100)        NULL,
    SoTienGoc               DECIMAL(18,2)       NOT NULL,
    SoTienGiam              DECIMAL(18,2)       NOT NULL DEFAULT 0,
    TongTien                DECIMAL(18,2)       NOT NULL,
    DonViTienTe             VARCHAR(10)         NOT NULL DEFAULT 'VND',
    CongThanhToan           VARCHAR(50)         NOT NULL,   -- VNPay | Momo | Stripe | PayPal
    MaGiaoDichCong          NVARCHAR(200)       NULL,
    TrangThaiThanhToan      VARCHAR(50)         NOT NULL DEFAULT 'Pending',
        -- Pending | Paid | Failed | Refunded
    ThanhToanLuc            DATETIME            NULL,
    DaXoa                   BIT                 NOT NULL DEFAULT 0,
    NgayTao                 DATETIME            NOT NULL DEFAULT GETDATE(),

    CONSTRAINT PK_DonHang               PRIMARY KEY (MaDonHang),
    CONSTRAINT CHK_DonHang_SoTienGoc    CHECK (SoTienGoc >= 0),
    CONSTRAINT CHK_DonHang_TongTien     CHECK (TongTien >= 0),
    CONSTRAINT CHK_DonHang_TongCT       CHECK (TongTien = SoTienGoc - SoTienGiam),
    CONSTRAINT FK_DonHang_HocVien       FOREIGN KEY (MaHocVien)
        REFERENCES NguoiDung(MaNguoiDung)   ON DELETE NO ACTION,
    CONSTRAINT FK_DonHang_MaGiamGia     FOREIGN KEY (MaGiamGia)
        REFERENCES MaGiamGia(MaGiamGia)     ON DELETE SET NULL
);
GO

-- ----------------------------------------------------------------
--  Bảng: ChiTietDonHang
--  Chi tiết từng khóa học trong đơn hàng, lưu giá chốt lúc mua
-- ----------------------------------------------------------------
CREATE TABLE ChiTietDonHang (
    MaChiTietDonHang    INT                 NOT NULL IDENTITY(1,1),
    MaDonHang           VARCHAR(50)         NOT NULL,
    MaKhoaHoc           UNIQUEIDENTIFIER    NOT NULL,
    GiaTaiThoiDiemMua   DECIMAL(18,2)       NOT NULL,
    GiamGiaApDung       DECIMAL(18,2)       NOT NULL DEFAULT 0,

    CONSTRAINT PK_ChiTietDonHang        PRIMARY KEY (MaChiTietDonHang),
    CONSTRAINT FK_CTDH_DonHang          FOREIGN KEY (MaDonHang)
        REFERENCES DonHang(MaDonHang)   ON DELETE CASCADE,
    CONSTRAINT FK_CTDH_KhoaHoc          FOREIGN KEY (MaKhoaHoc)
        REFERENCES KhoaHoc(MaKhoaHoc)   ON DELETE NO ACTION
);
GO

-- ----------------------------------------------------------------
--  Bảng: GhiDanh
--  Ánh xạ quyền sở hữu & truy cập khóa học sau thanh toán
-- ----------------------------------------------------------------
CREATE TABLE GhiDanh (
    MaGhiDanh           INT                 NOT NULL IDENTITY(1,1),
    MaHocVien           UNIQUEIDENTIFIER    NOT NULL,
    MaKhoaHoc           UNIQUEIDENTIFIER    NOT NULL,
    MaDonHang           VARCHAR(50)         NULL,   -- NULL = ghi danh miễn phí
    GhiDanhLuc          DATETIME            NOT NULL DEFAULT GETDATE(),
    TrangThaiTruyCap    VARCHAR(50)         NOT NULL DEFAULT 'Active',   -- Active | Revoked
    ThuHoiLuc           DATETIME            NULL,

    CONSTRAINT PK_GhiDanh               PRIMARY KEY (MaGhiDanh),
    CONSTRAINT UQ_GhiDanh_HocVienKH     UNIQUE (MaHocVien, MaKhoaHoc),
    CONSTRAINT FK_GhiDanh_HocVien       FOREIGN KEY (MaHocVien)
        REFERENCES NguoiDung(MaNguoiDung)   ON DELETE NO ACTION,
    CONSTRAINT FK_GhiDanh_KhoaHoc       FOREIGN KEY (MaKhoaHoc)
        REFERENCES KhoaHoc(MaKhoaHoc)       ON DELETE NO ACTION,
    CONSTRAINT FK_GhiDanh_DonHang       FOREIGN KEY (MaDonHang)
        REFERENCES DonHang(MaDonHang)       ON DELETE NO ACTION
);
GO

-- ----------------------------------------------------------------
--  Bảng: HoanTien
--  Quản lý yêu cầu và trạng thái hoàn tiền
-- ----------------------------------------------------------------
CREATE TABLE HoanTien (
    MaHoanTien  INT                 NOT NULL IDENTITY(1,1),
    MaDonHang   VARCHAR(50)         NOT NULL,
    MaHocVien   UNIQUEIDENTIFIER    NOT NULL,
    LyDo        NVARCHAR(MAX)       NULL,
    SoTienHoan  DECIMAL(18,2)       NOT NULL,
    TrangThai   VARCHAR(50)         NOT NULL DEFAULT 'Pending',
        -- Pending | Approved | Rejected | Completed
    XuLyBoi     UNIQUEIDENTIFIER    NULL,
    XuLyLuc     DATETIME            NULL,
    NgayTao     DATETIME            NOT NULL DEFAULT GETDATE(),

    CONSTRAINT PK_HoanTien          PRIMARY KEY (MaHoanTien),
    CONSTRAINT FK_HoanTien_DonHang  FOREIGN KEY (MaDonHang)
        REFERENCES DonHang(MaDonHang)       ON DELETE NO ACTION,
    CONSTRAINT FK_HoanTien_HocVien  FOREIGN KEY (MaHocVien)
        REFERENCES NguoiDung(MaNguoiDung)   ON DELETE NO ACTION,
    CONSTRAINT FK_HoanTien_XuLyBoi  FOREIGN KEY (XuLyBoi)
        REFERENCES NguoiDung(MaNguoiDung)   ON DELETE NO ACTION
);
GO

-- ============================================================
--  NHÓM 6 : TIẾN TRÌNH HỌC TẬP & ĐÁNH GIÁ
-- ============================================================

-- ----------------------------------------------------------------
--  Bảng: TienDoHocTap
--  Theo dõi tiến độ học viên trên từng bài giảng
-- ----------------------------------------------------------------
CREATE TABLE TienDoHocTap (
    MaTienDo            BIGINT      NOT NULL IDENTITY(1,1),
    MaGhiDanh           INT         NOT NULL,
    MaBaiGiang          INT         NOT NULL,
    ThoiGianXemGiay     INT         NOT NULL DEFAULT 0,
    ViTriXemCuoi        INT         NOT NULL DEFAULT 0,
    TrangThai           VARCHAR(50) NOT NULL DEFAULT 'NotStarted',
        -- NotStarted | InProgress | Completed
    HoanThanhLuc        DATETIME    NULL,
    NgayCapNhat         DATETIME    NULL,

    CONSTRAINT PK_TienDoHocTap          PRIMARY KEY (MaTienDo),
    CONSTRAINT UQ_TienDo_GhiDanhBai     UNIQUE (MaGhiDanh, MaBaiGiang),
    CONSTRAINT FK_TienDo_GhiDanh        FOREIGN KEY (MaGhiDanh)
        REFERENCES GhiDanh(MaGhiDanh)  ON DELETE CASCADE,
    CONSTRAINT FK_TienDo_BaiGiang       FOREIGN KEY (MaBaiGiang)
        REFERENCES BaiGiang(MaBaiGiang) ON DELETE CASCADE
);
GO

-- ----------------------------------------------------------------
--  Bảng: LanLamBai
--  Kết quả mỗi lần học viên làm bài kiểm tra
-- ----------------------------------------------------------------
CREATE TABLE LanLamBai (
    MaLanLam            INT                 NOT NULL IDENTITY(1,1),
    MaHocVien           UNIQUEIDENTIFIER    NOT NULL,
    MaBaiKiemTra        INT                 NOT NULL,
    DiemSo              DECIMAL(5,2)        NOT NULL,
    TongCauHoi          INT                 NOT NULL,
    SoCauDung           INT                 NOT NULL,
    DaDat               BIT                 NOT NULL,
    ThoiGianLamGiay     INT                 NULL,
    BatDauLamLuc        DATETIME            NOT NULL DEFAULT GETDATE(),
    NopBaiLuc           DATETIME            NULL,

    CONSTRAINT PK_LanLamBai             PRIMARY KEY (MaLanLam),
    CONSTRAINT FK_LanLam_HocVien        FOREIGN KEY (MaHocVien)
        REFERENCES NguoiDung(MaNguoiDung)       ON DELETE NO ACTION,
    CONSTRAINT FK_LanLam_BaiKiemTra     FOREIGN KEY (MaBaiKiemTra)
        REFERENCES BaiKiemTra(MaBaiKiemTra)     ON DELETE NO ACTION
);
GO

-- ----------------------------------------------------------------
--  Bảng: DapAnHocVien
--  Chi tiết đáp án học viên chọn trong từng lần làm bài
-- ----------------------------------------------------------------
CREATE TABLE DapAnHocVien (
    MaDapAnChon         INT     NOT NULL IDENTITY(1,1),
    MaLanLam            INT     NOT NULL,
    MaCauHoi            INT     NOT NULL,
    MaLuaChonDaChon     INT     NULL,
    LaDapAnDung         BIT     NOT NULL,

    CONSTRAINT PK_DapAnHocVien          PRIMARY KEY (MaDapAnChon),
    CONSTRAINT FK_DapAn_LanLam          FOREIGN KEY (MaLanLam)
        REFERENCES LanLamBai(MaLanLam)          ON DELETE CASCADE,
    CONSTRAINT FK_DapAn_CauHoi          FOREIGN KEY (MaCauHoi)
        REFERENCES CauHoi(MaCauHoi)             ON DELETE NO ACTION,
    CONSTRAINT FK_DapAn_LuaChon         FOREIGN KEY (MaLuaChonDaChon)
        REFERENCES LuaChonDapAn(MaLuaChon)      ON DELETE NO ACTION
);
GO

-- ----------------------------------------------------------------
--  Bảng: ChungChi
--  Chứng chỉ số cấp khi học viên hoàn thành 100% khóa học
-- ----------------------------------------------------------------
CREATE TABLE ChungChi (
    MaChungChi          UNIQUEIDENTIFIER    NOT NULL DEFAULT NEWSEQUENTIALID(),
    MaHocVien           UNIQUEIDENTIFIER    NOT NULL,
    MaKhoaHoc           UNIQUEIDENTIFIER    NOT NULL,
    NgayCapPhat         DATETIME            NOT NULL DEFAULT GETDATE(),
    MaXacThuc           VARCHAR(100)        NOT NULL,
    DuongDanPDFChungChi NVARCHAR(1000)      NULL,

    CONSTRAINT PK_ChungChi              PRIMARY KEY (MaChungChi),
    CONSTRAINT UQ_ChungChi_MaXacThuc    UNIQUE (MaXacThuc),
    CONSTRAINT UQ_ChungChi_HocVienKH    UNIQUE (MaHocVien, MaKhoaHoc),
    CONSTRAINT FK_ChungChi_HocVien      FOREIGN KEY (MaHocVien)
        REFERENCES NguoiDung(MaNguoiDung)   ON DELETE NO ACTION,
    CONSTRAINT FK_ChungChi_KhoaHoc      FOREIGN KEY (MaKhoaHoc)
        REFERENCES KhoaHoc(MaKhoaHoc)       ON DELETE NO ACTION
);
GO

-- ----------------------------------------------------------------
--  Bảng: GhiChu
--  Ghi chú riêng tư của học viên gắn với từng giây trong video
-- ----------------------------------------------------------------
CREATE TABLE GhiChu (
    MaGhiChu    INT                 NOT NULL IDENTITY(1,1),
    MaHocVien   UNIQUEIDENTIFIER    NOT NULL,
    MaBaiGiang  INT                 NOT NULL,
    ThoiDiem    INT                 NOT NULL,   -- giây trong video
    NoiDung     NVARCHAR(MAX)       NOT NULL,
    NgayTao     DATETIME            NOT NULL DEFAULT GETDATE(),
    NgayCapNhat DATETIME            NULL,

    CONSTRAINT PK_GhiChu                PRIMARY KEY (MaGhiChu),
    CONSTRAINT FK_GhiChu_HocVien        FOREIGN KEY (MaHocVien)
        REFERENCES NguoiDung(MaNguoiDung)   ON DELETE CASCADE,
    CONSTRAINT FK_GhiChu_BaiGiang       FOREIGN KEY (MaBaiGiang)
        REFERENCES BaiGiang(MaBaiGiang)     ON DELETE CASCADE
);
GO

-- ============================================================
--  NHÓM 7 : QUẢN LÝ LỊCH HỌC & ĐIỂM DANH
-- ============================================================

-- ----------------------------------------------------------------
--  Bảng: LichHoc
--  Lịch học cam kết do học viên tự thiết lập
-- ----------------------------------------------------------------
CREATE TABLE LichHoc (
    MaLichHoc               INT                 NOT NULL IDENTITY(1,1),
    MaHocVien               UNIQUEIDENTIFIER    NOT NULL,
    MaKhoaHoc               UNIQUEIDENTIFIER    NOT NULL,
    NgayTrongTuan           VARCHAR(50)         NOT NULL,   -- JSON array vd [1,3,5]
    GioBatDau               TIME                NOT NULL,
    GioKetThuc              TIME                NOT NULL,
    ThoiLuongToiThieuPhut   INT                 NOT NULL DEFAULT 30,
    DangHoatDong            BIT                 NOT NULL DEFAULT 1,
    NgayTao                 DATETIME            NOT NULL DEFAULT GETDATE(),

    CONSTRAINT PK_LichHoc               PRIMARY KEY (MaLichHoc),
    CONSTRAINT FK_LichHoc_HocVien       FOREIGN KEY (MaHocVien)
        REFERENCES NguoiDung(MaNguoiDung)   ON DELETE CASCADE,
    CONSTRAINT FK_LichHoc_KhoaHoc       FOREIGN KEY (MaKhoaHoc)
        REFERENCES KhoaHoc(MaKhoaHoc)       ON DELETE CASCADE
);
GO

-- ----------------------------------------------------------------
--  Bảng: DiemDanh
--  Bản ghi điểm danh từng phiên học
-- ----------------------------------------------------------------
CREATE TABLE DiemDanh (
    MaDiemDanh              BIGINT              NOT NULL IDENTITY(1,1),
    MaLichHoc               INT                 NOT NULL,
    MaHocVien               UNIQUEIDENTIFIER    NOT NULL,
    MaKhoaHoc               UNIQUEIDENTIFIER    NOT NULL,
    NgayLichHoc             DATE                NOT NULL,
    DiemDanhLuc             DATETIME            NULL,
    ThoiGianXemThucTeGiay   INT                 NOT NULL DEFAULT 0,
    HopLe                   BIT                 NOT NULL DEFAULT 0,
    TrangThai               VARCHAR(50)         NOT NULL,   -- Present | Absent | Missed

    CONSTRAINT PK_DiemDanh              PRIMARY KEY (MaDiemDanh),
    CONSTRAINT FK_DiemDanh_LichHoc      FOREIGN KEY (MaLichHoc)
        REFERENCES LichHoc(MaLichHoc)       ON DELETE CASCADE,
    CONSTRAINT FK_DiemDanh_HocVien      FOREIGN KEY (MaHocVien)
        REFERENCES NguoiDung(MaNguoiDung)   ON DELETE NO ACTION,
    CONSTRAINT FK_DiemDanh_KhoaHoc      FOREIGN KEY (MaKhoaHoc)
        REFERENCES KhoaHoc(MaKhoaHoc)       ON DELETE NO ACTION
);
GO

-- ----------------------------------------------------------------
--  Bảng: ChuoiHocLienTiep
--  Theo dõi chuỗi ngày học liên tiếp – Gamification
-- ----------------------------------------------------------------
CREATE TABLE ChuoiHocLienTiep (
    MaChuoi             INT                 NOT NULL IDENTITY(1,1),
    MaHocVien           UNIQUEIDENTIFIER    NOT NULL,
    ChuoiHienTai        INT                 NOT NULL DEFAULT 0,
    ChuoiDaiNhat        INT                 NOT NULL DEFAULT 0,
    NgayHoatDongCuoi    DATE                NULL,
    TheDoangBang        INT                 NOT NULL DEFAULT 0,
    NgayCapNhat         DATETIME            NULL,

    CONSTRAINT PK_ChuoiHocLienTiep          PRIMARY KEY (MaChuoi),
    CONSTRAINT UQ_ChuoiHoc_HocVien          UNIQUE (MaHocVien),
    CONSTRAINT FK_ChuoiHoc_HocVien          FOREIGN KEY (MaHocVien)
        REFERENCES NguoiDung(MaNguoiDung)   ON DELETE CASCADE
);
GO

-- ============================================================
--  NHÓM 8 : TƯƠNG TÁC HỌC THUẬT & ĐÁNH GIÁ
-- ============================================================

-- ----------------------------------------------------------------
--  Bảng: DanhGia
--  Đánh giá sao và nhận xét của học viên về khóa học
-- ----------------------------------------------------------------
CREATE TABLE DanhGia (
    MaDanhGia       INT                 NOT NULL IDENTITY(1,1),
    MaKhoaHoc       UNIQUEIDENTIFIER    NOT NULL,
    MaHocVien       UNIQUEIDENTIFIER    NOT NULL,
    DiemSao         INT                 NOT NULL,
    NhanXet         NVARCHAR(MAX)       NULL,
    DaMuaKhoaHoc    BIT                 NOT NULL DEFAULT 1,
    BiBaoCao        BIT                 NOT NULL DEFAULT 0,
    DaXoa           BIT                 NOT NULL DEFAULT 0,
    NgayTao         DATETIME            NOT NULL DEFAULT GETDATE(),
    NgayCapNhat     DATETIME            NULL,

    CONSTRAINT PK_DanhGia               PRIMARY KEY (MaDanhGia),
    CONSTRAINT CHK_DanhGia_DiemSao      CHECK (DiemSao BETWEEN 1 AND 5),
    CONSTRAINT UQ_DanhGia_HocVienKH     UNIQUE (MaHocVien, MaKhoaHoc),
    CONSTRAINT FK_DanhGia_KhoaHoc       FOREIGN KEY (MaKhoaHoc)
        REFERENCES KhoaHoc(MaKhoaHoc)       ON DELETE NO ACTION,
    CONSTRAINT FK_DanhGia_HocVien       FOREIGN KEY (MaHocVien)
        REFERENCES NguoiDung(MaNguoiDung)   ON DELETE NO ACTION
);
GO

-- ----------------------------------------------------------------
--  Bảng: ChuDeHoiDap
--  Chủ đề câu hỏi học thuật gắn với bài giảng
-- ----------------------------------------------------------------
CREATE TABLE ChuDeHoiDap (
    MaChuDe         INT                 NOT NULL IDENTITY(1,1),
    MaBaiGiang      INT                 NOT NULL,
    MaHocVien       UNIQUEIDENTIFIER    NOT NULL,
    TieuDe          NVARCHAR(500)       NOT NULL,
    NoiDungChiTiet  NVARCHAR(MAX)       NOT NULL,
    SoLuotUpvote    INT                 NOT NULL DEFAULT 0,
    DaGiaiDap       BIT                 NOT NULL DEFAULT 0,
    DaXoa           BIT                 NOT NULL DEFAULT 0,
    NgayTao         DATETIME            NOT NULL DEFAULT GETDATE(),

    CONSTRAINT PK_ChuDeHoiDap           PRIMARY KEY (MaChuDe),
    CONSTRAINT FK_ChuDe_BaiGiang        FOREIGN KEY (MaBaiGiang)
        REFERENCES BaiGiang(MaBaiGiang)     ON DELETE CASCADE,
    CONSTRAINT FK_ChuDe_HocVien         FOREIGN KEY (MaHocVien)
        REFERENCES NguoiDung(MaNguoiDung)   ON DELETE NO ACTION
);
GO

-- ----------------------------------------------------------------
--  Bảng: TraLoiHoiDap
--  Câu trả lời trong các chủ đề Hỏi & Đáp
-- ----------------------------------------------------------------
CREATE TABLE TraLoiHoiDap (
    MaTraLoi        INT                 NOT NULL IDENTITY(1,1),
    MaChuDe         INT                 NOT NULL,
    MaNguoiDung     UNIQUEIDENTIFIER    NOT NULL,
    NoiDungChiTiet  NVARCHAR(MAX)       NOT NULL,
    DuocGhimBoi     BIT                 NOT NULL DEFAULT 0,
    SoLuotUpvote    INT                 NOT NULL DEFAULT 0,
    DaXoa           BIT                 NOT NULL DEFAULT 0,
    NgayTao         DATETIME            NOT NULL DEFAULT GETDATE(),

    CONSTRAINT PK_TraLoiHoiDap          PRIMARY KEY (MaTraLoi),
    CONSTRAINT FK_TraLoi_ChuDe          FOREIGN KEY (MaChuDe)
        REFERENCES ChuDeHoiDap(MaChuDe)     ON DELETE CASCADE,
    CONSTRAINT FK_TraLoi_NguoiDung      FOREIGN KEY (MaNguoiDung)
        REFERENCES NguoiDung(MaNguoiDung)   ON DELETE NO ACTION
);
GO

-- ============================================================
--  NHÓM 9 : MARKETING & KHUYẾN MÃI
-- ============================================================

-- ----------------------------------------------------------------
--  Bảng: ChienDich
--  Chiến dịch marketing (Flash Sale, Black Friday…)
-- ----------------------------------------------------------------
CREATE TABLE ChienDich (
    MaChienDich     INT                 NOT NULL IDENTITY(1,1),
    TenChienDich    NVARCHAR(300)       NOT NULL,
    MoTa            NVARCHAR(MAX)       NULL,
    NgayBatDau      DATETIME            NOT NULL,
    NgayKetThuc     DATETIME            NOT NULL,
    DuongDanBanner  NVARCHAR(1000)      NULL,
    DangHoatDong    BIT                 NOT NULL DEFAULT 1,
    TaoBoi          UNIQUEIDENTIFIER    NOT NULL,
    NgayTao         DATETIME            NOT NULL DEFAULT GETDATE(),

    CONSTRAINT PK_ChienDich             PRIMARY KEY (MaChienDich),
    CONSTRAINT CHK_ChienDich_Ngay       CHECK (NgayKetThuc > NgayBatDau),
    CONSTRAINT FK_ChienDich_TaoBoi      FOREIGN KEY (TaoBoi)
        REFERENCES NguoiDung(MaNguoiDung) ON DELETE NO ACTION
);
GO

-- Bổ sung FK MaChienDich vào MaGiamGia (đã tạo trước đó)
ALTER TABLE MaGiamGia
    ADD CONSTRAINT FK_MaGiamGia_ChienDich
    FOREIGN KEY (MaChienDich)
    REFERENCES ChienDich(MaChienDich) ON DELETE SET NULL;
GO

-- ----------------------------------------------------------------
--  Bảng: TuongTacNguoiDung
--  Hành vi tương tác của người dùng – dữ liệu đầu vào gợi ý
-- ----------------------------------------------------------------
CREATE TABLE TuongTacNguoiDung (
    MaTuongTac      BIGINT              NOT NULL IDENTITY(1,1),
    MaNguoiDung     UNIQUEIDENTIFIER    NOT NULL,
    MaKhoaHoc       UNIQUEIDENTIFIER    NOT NULL,
    LoaiTuongTac    VARCHAR(50)         NOT NULL,
        -- View | AddToCart | Purchase | Search | Wishlist
    ThoiDiem        DATETIME            NOT NULL DEFAULT GETDATE(),
    MaPhien         NVARCHAR(100)       NULL,

    CONSTRAINT PK_TuongTacNguoiDung         PRIMARY KEY (MaTuongTac),
    CONSTRAINT FK_TuongTac_NguoiDung        FOREIGN KEY (MaNguoiDung)
        REFERENCES NguoiDung(MaNguoiDung)   ON DELETE NO ACTION,
    CONSTRAINT FK_TuongTac_KhoaHoc          FOREIGN KEY (MaKhoaHoc)
        REFERENCES KhoaHoc(MaKhoaHoc)       ON DELETE NO ACTION
);
GO

-- ----------------------------------------------------------------
--  Bảng: ThongBaoKhoaHoc
--  Thông báo từ giảng viên gửi đến học viên đã mua khóa học
-- ----------------------------------------------------------------
CREATE TABLE ThongBaoKhoaHoc (
    MaThongBao      INT                 NOT NULL IDENTITY(1,1),
    MaKhoaHoc       UNIQUEIDENTIFIER    NOT NULL,
    MaGiangVien     UNIQUEIDENTIFIER    NOT NULL,
    TieuDeThongBao  NVARCHAR(500)       NOT NULL,
    NoiDung         NVARCHAR(MAX)       NOT NULL,
    GuiLuc          DATETIME            NOT NULL DEFAULT GETDATE(),
    TongNguoiNhan   INT                 NOT NULL DEFAULT 0,

    CONSTRAINT PK_ThongBaoKhoaHoc           PRIMARY KEY (MaThongBao),
    CONSTRAINT FK_ThongBao_KhoaHoc          FOREIGN KEY (MaKhoaHoc)
        REFERENCES KhoaHoc(MaKhoaHoc)       ON DELETE NO ACTION,
    CONSTRAINT FK_ThongBao_GiangVien        FOREIGN KEY (MaGiangVien)
        REFERENCES NguoiDung(MaNguoiDung)   ON DELETE NO ACTION
);
GO

-- ============================================================
--  NHÓM 10 : TÀI CHÍNH & GIẢI NGÂN
-- ============================================================

-- ----------------------------------------------------------------
--  Bảng: HoSoGiangVien
--  Hồ sơ mở rộng, thông tin KYC và cấu hình tài chính giảng viên
-- ----------------------------------------------------------------
CREATE TABLE HoSoGiangVien (
    MaGiangVien             UNIQUEIDENTIFIER    NOT NULL,
    DanhHieu                NVARCHAR(300)       NULL,
    TieuSuChiTiet           NVARCHAR(MAX)       NULL,
    DuongDanWebsite         NVARCHAR(500)       NULL,
    DuongDanLinkedIn        NVARCHAR(500)       NULL,
    ThongTinNganHangMaHoa   NVARCHAR(MAX)       NULL,   -- AES-256
    MaSoThueMaHoa           NVARCHAR(MAX)       NULL,   -- AES-256
    TyLeDoanhThu            DECIMAL(5,2)        NOT NULL DEFAULT 70,
    TrangThaiKYC            VARCHAR(50)         NOT NULL DEFAULT 'Pending',
        -- Pending | Verified | Rejected
    KyKetDieuKhoanLuc       DATETIME            NULL,
    NgayCapNhat             DATETIME            NULL,

    CONSTRAINT PK_HoSoGiangVien         PRIMARY KEY (MaGiangVien),
    CONSTRAINT FK_HoSoGV_NguoiDung      FOREIGN KEY (MaGiangVien)
        REFERENCES NguoiDung(MaNguoiDung) ON DELETE CASCADE
);
GO

-- ----------------------------------------------------------------
--  Bảng: SoCaiKeToan
--  Bút toán phân bổ doanh thu sau mỗi đơn hàng thành công
-- ----------------------------------------------------------------
CREATE TABLE SoCaiKeToan (
    MaBuToan            BIGINT              NOT NULL IDENTITY(1,1),
    MaDonHang           VARCHAR(50)         NOT NULL,
    MaGiangVien         UNIQUEIDENTIFIER    NOT NULL,
    MaKhoaHoc           UNIQUEIDENTIFIER    NOT NULL,
    DoanhThuGop         DECIMAL(18,2)       NOT NULL,
    PhiNenTang          DECIMAL(18,2)       NOT NULL,
    ThuNhapGiangVien    DECIMAL(18,2)       NOT NULL,
    NgayGiaiPhong       DATETIME            NOT NULL,
    DaGiaiPhong         BIT                 NOT NULL DEFAULT 0,
    GiaiPhongLuc        DATETIME            NULL,
    NgayTao             DATETIME            NOT NULL DEFAULT GETDATE(),

    CONSTRAINT PK_SoCaiKeToan           PRIMARY KEY (MaBuToan),
    CONSTRAINT CHK_SoCai_TongKhop       CHECK (DoanhThuGop = PhiNenTang + ThuNhapGiangVien),
    CONSTRAINT FK_SoCai_DonHang         FOREIGN KEY (MaDonHang)
        REFERENCES DonHang(MaDonHang)       ON DELETE NO ACTION,
    CONSTRAINT FK_SoCai_GiangVien       FOREIGN KEY (MaGiangVien)
        REFERENCES NguoiDung(MaNguoiDung)   ON DELETE NO ACTION,
    CONSTRAINT FK_SoCai_KhoaHoc         FOREIGN KEY (MaKhoaHoc)
        REFERENCES KhoaHoc(MaKhoaHoc)       ON DELETE NO ACTION
);
GO

-- ----------------------------------------------------------------
--  Bảng: YeuCauRutTien
--  Yêu cầu rút tiền doanh thu của giảng viên, CFO phê duyệt
-- ----------------------------------------------------------------
CREATE TABLE YeuCauRutTien (
    MaYeuCau        INT                 NOT NULL IDENTITY(1,1),
    MaGiangVien     UNIQUEIDENTIFIER    NOT NULL,
    SoTienYeuCau    DECIMAL(18,2)       NOT NULL,
    TrangThai       VARCHAR(50)         NOT NULL DEFAULT 'Pending',
        -- Pending | Approved | Paid | Rejected
    SnapshotNganHang NVARCHAR(MAX)      NOT NULL,   -- JSON
    DuyetBoi        UNIQUEIDENTIFIER    NULL,
    GhiChuDuyet     NVARCHAR(MAX)       NULL,
    ThanhToanLuc    DATETIME            NULL,
    NgayTao         DATETIME            NOT NULL DEFAULT GETDATE(),

    CONSTRAINT PK_YeuCauRutTien         PRIMARY KEY (MaYeuCau),
    CONSTRAINT CHK_YeuCau_SoTien        CHECK (SoTienYeuCau > 0),
    CONSTRAINT FK_YeuCau_GiangVien      FOREIGN KEY (MaGiangVien)
        REFERENCES NguoiDung(MaNguoiDung)   ON DELETE NO ACTION,
    CONSTRAINT FK_YeuCau_DuyetBoi       FOREIGN KEY (DuyetBoi)
        REFERENCES NguoiDung(MaNguoiDung)   ON DELETE NO ACTION
);
GO

-- ----------------------------------------------------------------
--  Bảng: TongHopDoanhThuNgay
--  Tổng hợp doanh thu theo ngày (Cron Job tạo lúc nửa đêm)
--  Chỉ đọc – không có API Update/Delete
-- ----------------------------------------------------------------
CREATE TABLE TongHopDoanhThuNgay (
    NgayTongHop     DATE                NOT NULL,
    MaKhoaHoc       UNIQUEIDENTIFIER    NOT NULL,
    TongDonHang     INT                 NOT NULL DEFAULT 0,
    TongDoanhThu    DECIMAL(18,2)       NOT NULL DEFAULT 0,
    TongHoanTien    INT                 NOT NULL DEFAULT 0,
    SoTienHoan      DECIMAL(18,2)       NOT NULL DEFAULT 0,

    CONSTRAINT PK_TongHopDoanhThuNgay   PRIMARY KEY (NgayTongHop, MaKhoaHoc),
    CONSTRAINT FK_TongHop_KhoaHoc       FOREIGN KEY (MaKhoaHoc)
        REFERENCES KhoaHoc(MaKhoaHoc)   ON DELETE NO ACTION
);
GO

-- ----------------------------------------------------------------
--  Bảng: ThongKeThoatBaiHoc
--  Thống kê tỷ lệ hoàn thành & điểm thoát bài học
--  Chỉ đọc – không có API Update/Delete
-- ----------------------------------------------------------------
CREATE TABLE ThongKeThoatBaiHoc (
    MaBaiGiang                  INT             NOT NULL,
    SoLuotXem                   INT             NOT NULL DEFAULT 0,
    SoLuotHoanThanh             INT             NOT NULL DEFAULT 0,
    ThoiGianXemTrungBinhGiay    INT             NOT NULL DEFAULT 0,
    TyLeThoat                   DECIMAL(5,2)    NOT NULL DEFAULT 0,
    CapNhatLanCuoi              DATETIME        NOT NULL,

    CONSTRAINT PK_ThongKeThoatBaiHoc    PRIMARY KEY (MaBaiGiang),
    CONSTRAINT FK_ThongKe_BaiGiang      FOREIGN KEY (MaBaiGiang)
        REFERENCES BaiGiang(MaBaiGiang) ON DELETE CASCADE
);
GO

-- ============================================================
--  NHÓM 11 : HỖ TRỢ KHÁCH HÀNG (TICKETING)
-- ============================================================

-- ----------------------------------------------------------------
--  Bảng: PhieuHoTro
--  Phiếu hỗ trợ khách hàng – quản lý vòng đời từ tạo đến đóng
-- ----------------------------------------------------------------
CREATE TABLE PhieuHoTro (
    MaPhieu             VARCHAR(50)         NOT NULL,   -- TKT-YYYYMMDD-HEX
    MaNguoiGuiYeuCau    UNIQUEIDENTIFIER    NOT NULL,
    PhanCongCho         UNIQUEIDENTIFIER    NULL,
    PhanLoai            VARCHAR(100)        NOT NULL,   -- Tech | Billing | Report | Account | Content
    TieuDeThongBao      NVARCHAR(500)       NOT NULL,
    MucUuTien           VARCHAR(50)         NOT NULL DEFAULT 'Normal',
        -- Low | Normal | High | Urgent
    TrangThai           VARCHAR(50)         NOT NULL DEFAULT 'Open',
        -- Open | InProgress | Resolved | Closed
    MaDonHangLienQuan   VARCHAR(50)         NULL,
    DuLieuBoiCanh       NVARCHAR(MAX)       NULL,   -- JSON
    DiemHaiLong         INT                 NULL,
    GiaiQuyetLuc        DATETIME            NULL,
    DongPhieuLuc        DATETIME            NULL,
    NgayTao             DATETIME            NOT NULL DEFAULT GETDATE(),

    CONSTRAINT PK_PhieuHoTro                PRIMARY KEY (MaPhieu),
    CONSTRAINT CHK_PhieuHoTro_DiemHL        CHECK (DiemHaiLong BETWEEN 1 AND 5),
    CONSTRAINT FK_Phieu_NguoiGuiYeuCau      FOREIGN KEY (MaNguoiGuiYeuCau)
        REFERENCES NguoiDung(MaNguoiDung)   ON DELETE NO ACTION,
    CONSTRAINT FK_Phieu_PhanCongCho         FOREIGN KEY (PhanCongCho)
        REFERENCES NguoiDung(MaNguoiDung)   ON DELETE NO ACTION,
    CONSTRAINT FK_Phieu_DonHang             FOREIGN KEY (MaDonHangLienQuan)
        REFERENCES DonHang(MaDonHang)       ON DELETE SET NULL
);
GO

-- ----------------------------------------------------------------
--  Bảng: PhanHoiPhieu
--  Nội dung trao đổi qua lại trong từng phiếu hỗ trợ
-- ----------------------------------------------------------------
CREATE TABLE PhanHoiPhieu (
    MaTraLoi        INT                 NOT NULL IDENTITY(1,1),
    MaPhieu         VARCHAR(50)         NOT NULL,
    MaNguoiGui      UNIQUEIDENTIFIER    NOT NULL,
    NoiDungTinNhan  NVARCHAR(MAX)       NOT NULL,
    TepDinhKem      NVARCHAR(MAX)       NULL,   -- JSON array URL
    GhiChuNoiBo     BIT                 NOT NULL DEFAULT 0,
    NgayTao         DATETIME            NOT NULL DEFAULT GETDATE(),

    CONSTRAINT PK_PhanHoiPhieu          PRIMARY KEY (MaTraLoi),
    CONSTRAINT FK_PhanHoi_Phieu         FOREIGN KEY (MaPhieu)
        REFERENCES PhieuHoTro(MaPhieu)      ON DELETE CASCADE,
    CONSTRAINT FK_PhanHoi_NguoiGui      FOREIGN KEY (MaNguoiGui)
        REFERENCES NguoiDung(MaNguoiDung)   ON DELETE NO ACTION
);
GO

-- ============================================================
--  NHÓM 12 : NHÂN VIÊN NỘI BỘ & BẢO MẬT ĐĂNG NHẬP
-- ============================================================

-- ----------------------------------------------------------------
--  Bảng: HoSoNhanVien
--  Hồ sơ nhân viên nội bộ (Admin, CFO, CMO, Moderator, CS)
--  Liên kết 1-1 với NguoiDung (LaNhanVien = 1)
-- ----------------------------------------------------------------
CREATE TABLE HoSoNhanVien (
    MaNhanVien          UNIQUEIDENTIFIER    NOT NULL,
    MaNhanVienNoiBo     VARCHAR(50)         NULL,
    PhongBan            NVARCHAR(200)       NULL,   -- Finance | Marketing | Content | CustomerSupport | IT
    ChucDanh            NVARCHAR(200)       NULL,
    SoDienThoaiNoiBo    VARCHAR(20)         NULL,
    QuyenBoSung         NVARCHAR(MAX)       NULL,   -- JSON
    NgayVaoLam          DATE                NULL,
    DangHoatDong        BIT                 NOT NULL DEFAULT 1,
    NgayTao             DATETIME            NOT NULL DEFAULT GETDATE(),
    NgayCapNhat         DATETIME            NULL,

    CONSTRAINT PK_HoSoNhanVien              PRIMARY KEY (MaNhanVien),
    CONSTRAINT UQ_HoSoNhanVien_MaNV         UNIQUE (MaNhanVienNoiBo),
    CONSTRAINT FK_HoSoNhanVien_NguoiDung    FOREIGN KEY (MaNhanVien)
        REFERENCES NguoiDung(MaNguoiDung)   ON DELETE CASCADE
);
GO

-- ----------------------------------------------------------------
--  Bảng: LanDangNhap
--  Lịch sử đăng nhập (thành công & thất bại) – Rate Limiting
--  Dùng tham chiếu mềm theo Email, không FK cứng
-- ----------------------------------------------------------------
CREATE TABLE LanDangNhap (
    MaLanLam            BIGINT          NOT NULL IDENTITY(1,1),
    Email               NVARCHAR(255)   NOT NULL,
    DiaChiIP            NVARCHAR(50)    NULL,
    TrinhDuyet          NVARCHAR(500)   NULL,
    DangNhapThanhCong   BIT             NOT NULL DEFAULT 0,
    LyDoThatBai         NVARCHAR(200)   NULL,   -- WrongPassword | AccountLocked | NotVerified
    BatDauLamLuc        DATETIME        NOT NULL DEFAULT GETDATE(),

    CONSTRAINT PK_LanDangNhap PRIMARY KEY (MaLanLam)
    -- Không FK cứng: lưu cả email không tồn tại (chống brute-force)
);
GO

-- ============================================================
--  CHỈ MỤC (INDEX) KHUYẾN NGHỊ
-- ============================================================

-- Unique indexes (đã khai báo qua UNIQUE constraint ở trên)

-- Composite indexes cho các truy vấn thường xuyên
CREATE INDEX IX_TienDoHocTap_GhiDanhBai
    ON TienDoHocTap (MaGhiDanh, MaBaiGiang);

CREATE INDEX IX_GhiDanh_HocVienKH
    ON GhiDanh (MaHocVien, MaKhoaHoc);

CREATE INDEX IX_TuongTac_NguoiDung_ThoiDiem
    ON TuongTacNguoiDung (MaNguoiDung, MaKhoaHoc, ThoiDiem DESC);

CREATE INDEX IX_TongHop_NgayKhoaHoc
    ON TongHopDoanhThuNgay (NgayTongHop, MaKhoaHoc);

-- Indexes tra cứu thường dùng
CREATE INDEX IX_DonHang_HocVien
    ON DonHang (MaHocVien);

CREATE INDEX IX_DonHang_TrangThai
    ON DonHang (TrangThaiThanhToan);

CREATE INDEX IX_PhieuHoTro_TrangThai
    ON PhieuHoTro (TrangThai);

CREATE INDEX IX_PhieuHoTro_PhanCong
    ON PhieuHoTro (PhanCongCho);

-- Indexes chống brute-force đăng nhập
CREATE INDEX IX_LanDangNhap_Email_Time
    ON LanDangNhap (Email, BatDauLamLuc DESC);

CREATE INDEX IX_LanDangNhap_IP_Time
    ON LanDangNhap (DiaChiIP, BatDauLamLuc DESC);

-- ============================================================
--  DỮ LIỆU MẪU KHỞI TẠO (Seed Data)
-- ============================================================

-- Vai trò mặc định
INSERT INTO VaiTro (TenVaiTro, MoTa) VALUES
    (N'Student',    N'Học viên – quyền truy cập khóa học đã mua'),
    (N'Instructor', N'Giảng viên – quyền tạo và quản lý khóa học'),
    (N'Admin',      N'Quản trị viên – toàn quyền hệ thống'),
    (N'CFO',        N'Giám đốc tài chính – duyệt rút tiền và hoàn tiền'),
    (N'CMO',        N'Giám đốc marketing – quản lý chiến dịch'),
    (N'Moderator',  N'Kiểm duyệt viên – duyệt nội dung khóa học'),
    (N'CS',         N'Nhân viên hỗ trợ khách hàng');

-- Cấu hình hệ thống mặc định
INSERT INTO CauHinhHeThong (KhoaCauHinh, GiaTriCauHinh, KieuDuLieu, MoTa) VALUES
    ('PhiNenTang',          '30',    'Decimal', N'Tỷ lệ % phí nền tảng giữ lại từ mỗi đơn hàng'),
    ('MaxUploadSizeMB',     '2048',  'Int',     N'Giới hạn dung lượng tệp tải lên (MB)'),
    ('ThoiGianGiuTienNgay', '30',    'Int',     N'Số ngày giữ tiền trước khi giải phóng cho giảng viên'),
    ('DiemToiThieuRutTien', '100000','Decimal', N'Số dư tối thiểu (VNĐ) để giảng viên có thể rút');

PRINT N'>>> Script tạo CSDL LMS hoàn tất. Tổng: 49 bảng.';
GO
