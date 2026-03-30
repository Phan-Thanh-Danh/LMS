using System;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Data
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var defaultDate = new DateTime(2024, 1, 1, 0, 0, 0);

            modelBuilder
                .Entity<VaiTro>()
                .HasData(
                    new VaiTro
                    {
                        MaVaiTro = 1,
                        TenVaiTro = "Student",
                        MoTa = "Học viên – quyền truy cập khóa học đã mua",
                        NgayTao = defaultDate,
                    },
                    new VaiTro
                    {
                        MaVaiTro = 2,
                        TenVaiTro = "Instructor",
                        MoTa = "Giảng viên – quyền tạo và quản lý khóa học",
                        NgayTao = defaultDate,
                    },
                    new VaiTro
                    {
                        MaVaiTro = 3,
                        TenVaiTro = "Admin",
                        MoTa = "Quản trị viên – toàn quyền hệ thống",
                        NgayTao = defaultDate,
                    },
                    new VaiTro
                    {
                        MaVaiTro = 4,
                        TenVaiTro = "CFO",
                        MoTa = "Giám đốc tài chính – duyệt rút tiền và hoàn tiền",
                        NgayTao = defaultDate,
                    },
                    new VaiTro
                    {
                        MaVaiTro = 5,
                        TenVaiTro = "CMO",
                        MoTa = "Giám đốc marketing – quản lý chiến dịch",
                        NgayTao = defaultDate,
                    },
                    new VaiTro
                    {
                        MaVaiTro = 6,
                        TenVaiTro = "Moderator",
                        MoTa = "Kiểm duyệt viên – duyệt nội dung khóa học",
                        NgayTao = defaultDate,
                    },
                    new VaiTro
                    {
                        MaVaiTro = 7,
                        TenVaiTro = "CS",
                        MoTa = "Nhân viên hỗ trợ khách hàng",
                        NgayTao = defaultDate,
                    }
                );

            // Seed Users
            var adminId = Guid.Parse("d4a7a8b4-8d4a-4b4a-8d4a-8d4a8d4a8d4a");
            var instructorId = Guid.Parse("e5b8b9c5-9e5b-5c5b-9e5b-9e5b9e5b9e5b");
            var studentId = Guid.Parse("f6c9c0d6-0f6c-6d6c-0f6c-0f6c0f6c0f6c");
            var cfoId = Guid.Parse("a1d2d3e4-1a2d-2e2d-1a2d-1a2d1a2d1a2d");
            var cmoId = Guid.Parse("b2e3e4f5-2b3e-3f3e-2b3e-2b3e2b3e2b3e");
            var moderatorId = Guid.Parse("c3f4f5a6-3c4f-4a4f-3c4f-3c4f3c4f3c4f");
            var csId = Guid.Parse("d4a5a6b7-4d5a-5b5a-4d5a-4d5a4d5a4d5a");

            // Password: Aet@123456
            var salt = "$2a$11$3dfJ1HCcvISVEAFCZr.Tz.";
            var hash = "$2a$11$6jn6T0/Ibm2yNlyM.cRUI.6nkuUZ46OOxIV.m4ZjXdkhstcBgAX4u"; // Hash chuẩn BCrypt từ hệ thống

            modelBuilder
                .Entity<NguoiDung>()
                .HasData(
                    new NguoiDung
                    {
                        MaNguoiDung = adminId,
                        Email = "admin@aet.com",
                        MatKhauBam = hash,
                        MuoiMatKhau = salt,
                        HoTen = "Hệ thống Admin",
                        EmailDaXacThuc = true,
                        LaNhanVien = true,
                        NgayTao = defaultDate,
                    },
                    new NguoiDung
                    {
                        MaNguoiDung = instructorId,
                        Email = "giangvien@aet.com",
                        MatKhauBam = hash,
                        MuoiMatKhau = salt,
                        HoTen = "Giảng viên Mẫu",
                        EmailDaXacThuc = true,
                        LaNhanVien = false,
                        NgayTao = defaultDate,
                    },
                    new NguoiDung
                    {
                        MaNguoiDung = studentId,
                        Email = "hocvien@aet.com",
                        MatKhauBam = hash,
                        MuoiMatKhau = salt,
                        HoTen = "Học viên Mẫu",
                        EmailDaXacThuc = true,
                        LaNhanVien = false,
                        NgayTao = defaultDate,
                    },
                    new NguoiDung
                    {
                        MaNguoiDung = cfoId,
                        Email = "cfo@aet.com",
                        MatKhauBam = hash,
                        MuoiMatKhau = salt,
                        HoTen = "Giám đốc tài chính",
                        EmailDaXacThuc = true,
                        LaNhanVien = true,
                        NgayTao = defaultDate,
                    },
                    new NguoiDung
                    {
                        MaNguoiDung = cmoId,
                        Email = "cmo@aet.com",
                        MatKhauBam = hash,
                        MuoiMatKhau = salt,
                        HoTen = "Giám đốc Marketing",
                        EmailDaXacThuc = true,
                        LaNhanVien = true,
                        NgayTao = defaultDate,
                    },
                    new NguoiDung
                    {
                        MaNguoiDung = moderatorId,
                        Email = "kiemduyet@aet.com",
                        MatKhauBam = hash,
                        MuoiMatKhau = salt,
                        HoTen = "Kiểm duyệt viên",
                        EmailDaXacThuc = true,
                        LaNhanVien = true,
                        NgayTao = defaultDate,
                    },
                    new NguoiDung
                    {
                        MaNguoiDung = csId,
                        Email = "cskh@aet.com",
                        MatKhauBam = hash,
                        MuoiMatKhau = salt,
                        HoTen = "Chăm sóc khách hàng",
                        EmailDaXacThuc = true,
                        LaNhanVien = true,
                        NgayTao = defaultDate,
                    }
                );

            // Seed UserRoles
            modelBuilder
                .Entity<VaiTroNguoiDung>()
                .HasData(
                    new VaiTroNguoiDung
                    {
                        MaVaiTroNguoiDung = 1,
                        MaNguoiDung = adminId,
                        MaVaiTro = 3,
                        NgayGanVaiTro = defaultDate,
                    },
                    new VaiTroNguoiDung
                    {
                        MaVaiTroNguoiDung = 2,
                        MaNguoiDung = instructorId,
                        MaVaiTro = 2,
                        NgayGanVaiTro = defaultDate,
                    },
                    new VaiTroNguoiDung
                    {
                        MaVaiTroNguoiDung = 3,
                        MaNguoiDung = studentId,
                        MaVaiTro = 1,
                        NgayGanVaiTro = defaultDate,
                    },
                    new VaiTroNguoiDung
                    {
                        MaVaiTroNguoiDung = 4,
                        MaNguoiDung = cfoId,
                        MaVaiTro = 4,
                        NgayGanVaiTro = defaultDate,
                    },
                    new VaiTroNguoiDung
                    {
                        MaVaiTroNguoiDung = 5,
                        MaNguoiDung = cmoId,
                        MaVaiTro = 5,
                        NgayGanVaiTro = defaultDate,
                    },
                    new VaiTroNguoiDung
                    {
                        MaVaiTroNguoiDung = 6,
                        MaNguoiDung = moderatorId,
                        MaVaiTro = 6,
                        NgayGanVaiTro = defaultDate,
                    },
                    new VaiTroNguoiDung
                    {
                        MaVaiTroNguoiDung = 7,
                        MaNguoiDung = csId,
                        MaVaiTro = 7,
                        NgayGanVaiTro = defaultDate,
                    }
                );

            modelBuilder
                .Entity<CauHinhHeThong>()
                .HasData(
                    new CauHinhHeThong
                    {
                        KhoaCauHinh = "PhiNenTang",
                        GiaTriCauHinh = "30",
                        KieuDuLieu = "Decimal",
                        MoTa = "Tỷ lệ % phí nền tảng giữ lại từ mỗi đơn hàng",
                    },
                    new CauHinhHeThong
                    {
                        KhoaCauHinh = "MaxUploadSizeMB",
                        GiaTriCauHinh = "2048",
                        KieuDuLieu = "Int",
                        MoTa = "Giới hạn dung lượng tệp tải lên (MB)",
                    },
                    new CauHinhHeThong
                    {
                        KhoaCauHinh = "ThoiGianGiuTienNgay",
                        GiaTriCauHinh = "30",
                        KieuDuLieu = "Int",
                        MoTa = "Số ngày giữ tiền trước khi giải phóng cho giảng viên",
                    },
                    new CauHinhHeThong
                    {
                        KhoaCauHinh = "DiemToiThieuRutTien",
                        GiaTriCauHinh = "100000",
                        KieuDuLieu = "Decimal",
                        MoTa = "Số dư tối thiểu (VNĐ) để giảng viên có thể rút",
                    }
                );
        }
    }
}
