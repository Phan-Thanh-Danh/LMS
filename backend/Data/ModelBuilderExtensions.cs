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
