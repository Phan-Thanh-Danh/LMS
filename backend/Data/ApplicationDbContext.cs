using backend.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace backend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        // IAM & Config
        public DbSet<NguoiDung> NguoiDungs { get; set; }
        public DbSet<VaiTro> VaiTros { get; set; }
        public DbSet<VaiTroNguoiDung> VaiTroNguoiDungs { get; set; }
        public DbSet<MaOtp> MaOtps { get; set; }
        public DbSet<PhienLamViec> PhienLamViecs { get; set; }
        public DbSet<NhatKyKiemToan> NhatKyKiemToans { get; set; }
        public DbSet<HoSoNhanVien> HoSoNhanViens { get; set; }
        public DbSet<LanDangNhap> LanDangNhaps { get; set; }
        public DbSet<CauHinhHeThong> CauHinhHeThongs { get; set; }
        public DbSet<TaiNguyenSo> TaiNguyenSos { get; set; }
        public DbSet<MaHoaVideo> MaHoaVideos { get; set; }
        public DbSet<ChienDich> ChienDichs { get; set; }
        public DbSet<TuongTacNguoiDung> TuongTacNguoiDungs { get; set; }
        public DbSet<HoSoGiangVien> HoSoGiangViens { get; set; }
        public DbSet<PhieuHoTro> PhieuHoTros { get; set; }
        public DbSet<PhanHoiPhieu> PhanHoiPhieus { get; set; }

        // Course Lifecycle
        public DbSet<DanhMuc> DanhMucs { get; set; }
        public DbSet<NhanTuKhoa> NhanTuKhoas { get; set; }
        public DbSet<KhoaHoc> KhoaHocs { get; set; }
        public DbSet<NhanKhoaHoc> NhanKhoaHocs { get; set; }
        public DbSet<Chuong> Chuongs { get; set; }
        public DbSet<BaiGiang> BaiGiangs { get; set; }
        public DbSet<BaiKiemTra> BaiKiemTras { get; set; }
        public DbSet<CauHoi> CauHois { get; set; }
        public DbSet<LuaChonDapAn> LuaChonDapAns { get; set; }
        public DbSet<ThongBaoKhoaHoc> ThongBaoKhoaHocs { get; set; }

        // Transaction & Commerce
        public DbSet<GiamGia> GiamGias { get; set; }
        public DbSet<GioHang> GioHangs { get; set; }
        public DbSet<MatHangGioHang> MatHangGioHangs { get; set; }
        public DbSet<DonHang> DonHangs { get; set; }
        public DbSet<ChiTietDonHang> ChiTietDonHangs { get; set; }
        public DbSet<GhiDanh> GhiDanhs { get; set; }
        public DbSet<HoanTien> HoanTiens { get; set; }
        public DbSet<SoCaiKeToan> SoCaiKeToans { get; set; }
        public DbSet<YeuCauRutTien> YeuCauRutTiens { get; set; }
        public DbSet<TongHopDoanhThuNgay> TongHopDoanhThuNgays { get; set; }

        // Learning & Analytics
        public DbSet<TienDoHocTap> TienDoHocTaps { get; set; }
        public DbSet<LanLamBai> LanLamBais { get; set; }
        public DbSet<DapAnHocVien> DapAnHocViens { get; set; }
        public DbSet<ChungChi> ChungChis { get; set; }
        public DbSet<GhiChu> GhiChus { get; set; }
        public DbSet<LichHoc> LichHocs { get; set; }
        public DbSet<DiemDanh> DiemDanhs { get; set; }
        public DbSet<ChuoiHocLienTiep> ChuoiHocLienTieps { get; set; }
        public DbSet<DanhGia> DanhGias { get; set; }
        public DbSet<ThongKeThoatBaiHoc> ThongKeThoatBaiHocs { get; set; }
        public DbSet<ChuDeHoiDap> ChuDeHoiDaps { get; set; }
        public DbSet<TraLoiHoiDap> TraLoiHoiDaps { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Composite Keys
            modelBuilder.Entity<NhanKhoaHoc>().HasKey(nk => new { nk.MaKhoaHoc, nk.MaNhan });

            modelBuilder
                .Entity<TongHopDoanhThuNgay>()
                .HasKey(th => new { th.NgayTongHop, th.MaKhoaHoc });

            modelBuilder.Entity<LanDangNhap>()
                .HasKey(ld => ld.MaLanLam);

            modelBuilder.Seed();

            // Tắt Cascade Delete mặc định của EF Core để tránh lỗi "multiple cascade paths" trên SQL Server
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }
}
