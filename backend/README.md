# LMS Platform — Tài Liệu Kỹ Thuật Nội Bộ

> Phiên bản: 1.0 | Ngôn ngữ DB: T-SQL (SQL Server) | Collation: Vietnamese_CI_AS  
> Tài liệu này được tối ưu cho AI-assisted development (Cursor, Copilot, GPT-4).  
> Không dùng cho mục đích marketing hay onboarding.

---

## Mục Lục

1. [Tổng Quan Dự Án](#1-tổng-quan-dự-án)
2. [Kiến Trúc Hệ Thống](#2-kiến-trúc-hệ-thống)
3. [Các Module Nghiệp Vụ](#3-các-module-nghiệp-vụ)
4. [Luồng Nghiệp Vụ Cốt Lõi](#4-luồng-nghiệp-vụ-cốt-lõi)
5. [Nguyên Tắc Thiết Kế Database](#5-nguyên-tắc-thiết-kế-database)
6. [Quy Ước Đặt Tên](#6-quy-ước-đặt-tên)
7. [Hướng Dẫn Thiết Kế API](#7-hướng-dẫn-thiết-kế-api)
8. [Cấu Trúc Dự Án](#8-cấu-trúc-dự-án)
9. [Ràng Buộc Dành Cho AI](#9-ràng-buộc-dành-cho-ai)

---

## 1. Tổng Quan Dự Án

Hệ thống LMS (Learning Management System) theo mô hình marketplace giáo dục trực tuyến tương tự Udemy. Kiến trúc xoay quanh ba actor chính: **Học viên (Student)**, **Giảng viên (Instructor)**, và **Ban vận hành (Admin/CFO/CMO/Moderator/CS)**. Hệ thống xử lý toàn bộ vòng đời từ tạo nội dung → kiểm duyệt → thanh toán → học tập → cấp chứng chỉ → phân chia doanh thu.

**Công nghệ nền tảng:**

- Database: Microsoft SQL Server (T-SQL), Collation `Vietnamese_CI_AS`
- Primary Key chính: `UNIQUEIDENTIFIER` với `NEWSEQUENTIALID()` (tránh fragmentation)
- Primary Key lookup/config: `INT IDENTITY`
- Soft delete toàn hệ thống thông qua cột `DaXoa BIT`
- Video streaming: HLS (HTTP Live Streaming) qua bảng `MaHoaVideo`
- File storage: S3 / Azure Blob (URL lưu trong `DuongDanLuuTru`)

**Quy mô database:** 46 bảng, 11 nhóm chức năng, quan hệ FK chặt chẽ.

---

## 2. Kiến Trúc Hệ Thống

```
┌─────────────────────────────────────────────────────────────┐
│                        CLIENT LAYER                         │
│          Web App / Mobile App / Admin Dashboard             │
└──────────────────────────┬──────────────────────────────────┘
                           │ REST API / WebSocket
┌──────────────────────────▼──────────────────────────────────┐
│                       API GATEWAY                           │
│          Auth middleware · Rate limit · Route               │
└──┬──────────┬──────────┬──────────┬──────────┬─────────────┘
   │          │          │          │          │
┌──▼──┐  ┌───▼──┐  ┌────▼──┐  ┌───▼──┐  ┌────▼──┐
│ IAM │  │Course│  │Payment│  │Media │  │Finance│  ...
│Svc  │  │  Svc │  │  Svc  │  │  Svc │  │  Svc  │
└──┬──┘  └───┬──┘  └────┬──┘  └───┬──┘  └────┬──┘
   └──────────┴──────────┴─────────┴──────────┘
                         │
┌────────────────────────▼────────────────────────────────────┐
│                    SQL SERVER (LMS_DB)                      │
│          46 tables · FK constraints · Soft delete           │
└─────────────────────────────────────────────────────────────┘
                         │
        ┌────────────────┼────────────────┐
   ┌────▼────┐     ┌─────▼─────┐   ┌──────▼──────┐
   │  S3 /   │     │  Message  │   │  Cron Jobs  │
   │  Azure  │     │   Queue   │   │ (Scheduler) │
   │  Blob   │     │ (Webhook) │   │             │
   └─────────┘     └───────────┘   └─────────────┘
```

**Các thành phần ngoại vi quan trọng:**

- **Payment Webhook**: Cổng thanh toán callback → cập nhật `DonHang.TrangThai` → trigger tạo `GhiDanh`
- **Media Pipeline**: Upload → `TaiNguyenSo` (Pending) → Encode job → `MaHoaVideo` (nhiều resolution) → `TaiNguyenSo.TrangThai = Ready`
- **Cron Jobs**: Tổng hợp doanh thu hằng ngày vào `TongHopDoanhThuNgay`, gửi nhắc lịch học từ `LichHoc`
- **Audit Logger**: Ghi mọi hành động quan trọng vào `NhatKyKiemToan`

---

## 3. Các Module Nghiệp Vụ

### 3.1 Quản Trị Định Danh & Phân Quyền (IAM)

**Mục đích:** Xác thực, phân quyền theo mô hình RBAC, quản lý phiên làm việc đa thiết bị, audit log.

**Bảng chính:**

| Bảng | Kiểu PK | Mục đích |
|------|---------|---------|
| `NguoiDung` | GUID | Tài khoản trung tâm — học viên, giảng viên, admin, nhân viên |
| `VaiTro` | INT | Danh mục vai trò: Student, Instructor, Admin, CFO, CMO, Moderator, CS |
| `VaiTroNguoiDung` | INT (junction) | Gán nhiều vai trò cho một tài khoản (N-N) |
| `MaOtp` | INT | Token OTP một lần: EmailVerify, PasswordReset, TwoFA |
| `PhienLamViec` | GUID | Refresh token, theo dõi IP/thiết bị, hỗ trợ force-logout |
| `NhatKyKiemToan` | BIGINT | Append-only audit log, lưu GiaTriCu/GiaTriMoi dạng JSON |

**Quy tắc nghiệp vụ:**
- Một tài khoản được gán nhiều vai trò đồng thời (giảng viên vừa là học viên).
- `NguoiDung.DaXoa = 1` là soft delete — không xóa vật lý.
- `NguoiDung.EmailDaXacThuc = 0` → chặn truy cập tài nguyên trả phí.
- `NguoiDung.LaNhanVien = 1` → tài khoản nội bộ (admin, CFO, CS...).
- `NhatKyKiemToan.MaNguoiDung` có thể NULL khi hành động do hệ thống tự động thực hiện.
- Không cho phép xóa `NguoiDung` nếu còn `VaiTroNguoiDung` tham chiếu (ON DELETE RESTRICT).

---

### 3.2 Dữ Liệu Gốc & Cấu Hình Hệ Thống

**Mục đích:** Dữ liệu tham chiếu tĩnh và cấu hình động không cần deploy lại.

**Bảng chính:**

| Bảng | Kiểu PK | Mục đích |
|------|---------|---------|
| `DanhMuc` | INT | Cây danh mục khóa học (đệ quy self-ref), hỗ trợ SEO slug |
| `NhanTuKhoa` | INT | Tag/label gán cho khóa học, đếm lượt dùng |
| `CauHinhHeThong` | VARCHAR (PK) | Key-value config: phí nền tảng, giới hạn upload, v.v. |

**Quy tắc nghiệp vụ:**
- `DanhMuc.MaDanhMucCha = NULL` → danh mục gốc cấp 1.
- Cấu trúc cây danh mục có CHECK constraint chống vòng lặp đệ quy.
- `CauHinhHeThong.KieuDuLieu` điều khiển cách parse giá trị: `Int | String | Boolean | JSON | Decimal`.

---

### 3.3 Quản Trị Tài Nguyên Số (Media)

**Mục đích:** Quản lý vòng đời file đa phương tiện — upload, encode, phân phối HLS.

**Bảng chính:**

| Bảng | Kiểu PK | Mục đích |
|------|---------|---------|
| `TaiNguyenSo` | GUID | Master record cho mọi file: Video, PDF, Image, ZipCode, Audio |
| `MaHoaVideo` | INT | Các phiên bản HLS sau encode: 1080p, 720p, 480p, 360p |

**Luồng trạng thái `TaiNguyenSo.TrangThai`:**
```
Pending → Processing → Ready
                    └→ Failed
```

**Quy tắc nghiệp vụ:**
- `DuongDanLuuTru` lưu URL đầy đủ trên S3/Azure — backend không lưu file cục bộ.
- `MaHoaVideo.DuongDanHLS` trỏ tới file `.m3u8` của từng resolution.
- `BaiGiang.MaTaiNguyen` SET NULL khi xóa resource (bài giảng vẫn tồn tại, chỉ mất media).
- Khi `TaiNguyenSo` bị xóa → tự động xóa toàn bộ `MaHoaVideo` liên quan (CASCADE).

---

### 3.4 Vòng Đời Khóa Học (Course Lifecycle)

**Mục đích:** Quản lý nội dung khóa học từ tạo → kiểm duyệt → xuất bản → lưu trữ.

**Bảng chính:**

| Bảng | Kiểu PK | Mục đích |
|------|---------|---------|
| `KhoaHoc` | GUID | Entity trung tâm: giá, trạng thái, metadata SEO |
| `NhanKhoaHoc` | Composite (junction) | N-N KhoaHoc ↔ NhanTuKhoa |
| `Chuong` | INT | Cấp 1 trong cây giáo trình (Chapter) |
| `BaiGiang` | INT | Cấp 2 trong cây giáo trình (Lecture) — đơn vị học nhỏ nhất |
| `BaiKiemTra` | INT | Quiz gắn với BaiGiang (1-1) |
| `CauHoi` | INT | Ngân hàng câu hỏi của BaiKiemTra |
| `LuaChonDapAn` | INT | Các lựa chọn đáp án của từng CauHoi |

**Luồng trạng thái `KhoaHoc.TrangThai`:**
```
0=Draft → 1=Pending (gửi duyệt) → 2=Published
                               └→ 3=Rejected (có GhiChuTuChoi)
2=Published → 4=Archived
```

**Quy tắc nghiệp vụ:**
- `KhoaHoc.Gia = 0` → khóa học miễn phí, vẫn cần tạo `GhiDanh`.
- `KhoaHoc.TongThoiLuong` và `TongBaiGiang` là denormalized counter — phải cập nhật khi thêm/xóa `BaiGiang`.
- `KhoaHoc.DiemDanhGiaTrungBinh` và `TongDanhGia` được tổng hợp từ bảng `DanhGia`.
- `BaiGiang.XemMienPhi = 1` → cho phép preview không cần mua.
- `LoaiBaiGiang`: `Video | Quiz | Document | Assignment` — kiểu Video mới có `MaTaiNguyen`.
- Xóa `KhoaHoc` CASCADE xuống `Chuong` → `BaiGiang` → `BaiKiemTra` → `CauHoi` → `LuaChonDapAn`.
- RESTRICT xóa `KhoaHoc` nếu còn `GhiDanh` hoặc `DonHang` tham chiếu.

---

### 3.5 Giao Dịch & Ghi Danh (E-Commerce)

**Mục đích:** Xử lý toàn bộ luồng mua hàng — giỏ hàng, đặt hàng, thanh toán, ghi danh, hoàn tiền.

**Bảng chính:**

| Bảng | Kiểu PK | Mục đích |
|------|---------|---------|
| `GioHang` | GUID | Giỏ hàng (1-1 với NguoiDung, lazy create) |
| `MatHangGioHang` | INT | Items trong giỏ hàng |
| `DonHang` | GUID | Đơn hàng sau checkout — immutable sau khi thanh toán thành công |
| `ChiTietDonHang` | INT | Dòng chi tiết đơn hàng, lưu `GiaChot` tại thời điểm mua |
| `GhiDanh` | GUID | Quyền truy cập khóa học của học viên sau thanh toán |
| `HoanTien` | INT | Yêu cầu hoàn tiền (1-1 với DonHang) |

**Quy tắc nghiệp vụ:**
- `ChiTietDonHang.GiaChot` lưu giá snapshot tại thời điểm mua — không thay đổi khi giá khóa học thay đổi sau này.
- `GhiDanh.MaDonHang` có thể NULL nếu ghi danh thủ công bởi admin.
- Khi payment webhook callback thành công: cập nhật `DonHang.TrangThai` → tạo `GhiDanh` cho từng `ChiTietDonHang`.
- `DonHang.MaGiamGia` SET NULL khi coupon bị xóa (lịch sử đơn hàng vẫn giữ).
- RESTRICT xóa `DonHang` nếu còn `GhiDanh` hoặc `SoCaiKeToan` tham chiếu.

---

### 3.6 Tiến Trình Học Tập & Đánh Giá (Learning Progress)

**Mục đích:** Theo dõi tiến độ học, kết quả quiz, cấp chứng chỉ, ghi chú cá nhân.

**Bảng chính:**

| Bảng | Kiểu PK | Mục đích |
|------|---------|---------|
| `TienDoHocTap` | INT | Progress record: học viên × bài giảng (qua GhiDanh) |
| `LanLamBai` | INT | Mỗi lần nộp bài kiểm tra |
| `DapAnHocVien` | INT | Chi tiết đáp án đã chọn trong từng lần làm bài |
| `ChungChi` | GUID | Chứng chỉ số, cấp khi hoàn thành 100% |
| `GhiChu` | INT | Ghi chú tại timestamp cụ thể trong video |

**Quy tắc nghiệp vụ:**
- `TienDoHocTap` phụ thuộc `GhiDanh` (không phải trực tiếp `NguoiDung`) — học viên không có `GhiDanh` thì không có progress.
- Tỷ lệ hoàn thành khóa học = `COUNT(TienDoHocTap WHERE HoanThanh=1) / TongBaiGiang`.
- `ChungChi` được tạo tự động khi tỷ lệ hoàn thành đạt 100% và điểm quiz vượt ngưỡng cấu hình.
- `GhiChu.ThoiDiemGiay` lưu giây trong video — phục vụ navigation note.
- Xóa `GhiDanh` → CASCADE xóa toàn bộ `TienDoHocTap` liên quan.

---

### 3.7 Lịch Học & Gamification

**Mục đích:** Quản lý cam kết học tập, điểm danh, chuỗi học liên tiếp (streak).

**Bảng chính:**

| Bảng | Kiểu PK | Mục đích |
|------|---------|---------|
| `LichHoc` | INT | Lịch học cam kết do học viên thiết lập |
| `DiemDanh` | INT | Bản ghi điểm danh từng phiên học |
| `ChuoiHocLienTiep` | INT (1-1 với NguoiDung) | Streak counter — phục vụ gamification |

**Quy tắc nghiệp vụ:**
- `ChuoiHocLienTiep` là bản ghi duy nhất per học viên (UNIQUE constraint).
- Streak bị reset về 0 nếu học viên không điểm danh trong ngày theo lịch đã cam kết.
- Cron job đêm kiểm tra `LichHoc` → tạo nhắc nhở → cập nhật `ChuoiHocLienTiep`.

---

### 3.8 Tương Tác Học Thuật & Đánh Giá (Engagement)

**Mục đích:** Đánh giá khóa học (review), hỏi đáp học thuật (Q&A).

**Bảng chính:**

| Bảng | Kiểu PK | Mục đích |
|------|---------|---------|
| `DanhGia` | INT | Rating 1-5 sao + nhận xét của học viên |
| `ChuDeHoiDap` | INT | Thread câu hỏi gắn với BaiGiang cụ thể |
| `TraLoiHoiDap` | INT | Reply trong thread Q&A (giảng viên hoặc học viên) |

**Quy tắc nghiệp vụ:**
- Học viên chỉ được đánh giá sau khi có `GhiDanh` hợp lệ.
- `KhoaHoc.DiemDanhGiaTrungBinh` phải được tính toán lại khi thêm/sửa/xóa `DanhGia`.
- RESTRICT xóa `NguoiDung` nếu còn `DanhGia` hoặc `ChuDeHoiDap` tham chiếu (bảo toàn lịch sử).
- Xóa `BaiGiang` CASCADE xóa toàn bộ `ChuDeHoiDap` và `TraLoiHoiDap` liên quan.

---

### 3.9 Marketing & Khuyến Mãi

**Mục đích:** Chiến dịch, mã giảm giá, tracking hành vi người dùng, thông báo từ giảng viên.

**Bảng chính:**

| Bảng | Kiểu PK | Mục đích |
|------|---------|---------|
| `ChienDich` | INT | Chiến dịch marketing (Flash Sale, Black Friday...) |
| `MaGiamGia` | INT | Coupon/voucher — quản lý điều kiện, giới hạn, hết hạn |
| `TuongTacNguoiDung` | BIGINT | Event tracking: xem, tìm kiếm, thêm giỏ, mua |
| `ThongBaoKhoaHoc` | INT | Thông báo giảng viên gửi tới học viên đã mua |

**Quy tắc nghiệp vụ:**
- `MaGiamGia.MaChienDich` SET NULL khi chiến dịch kết thúc/xóa — coupon vẫn có thể hoạt động độc lập.
- `MaGiamGia.MaGiangVien` SET NULL khi giảng viên bị xóa — coupon vẫn tồn tại trong lịch sử.
- `TuongTacNguoiDung` là bảng event log — chỉ INSERT, không UPDATE/DELETE.
- `ThongBaoKhoaHoc` chỉ gửi đến học viên có `GhiDanh` hợp lệ trong khóa học đó.

---

### 3.10 Tài Chính & Giải Ngân (Finance)

**Mục đích:** Theo dõi doanh thu giảng viên, sổ cái kế toán, xử lý yêu cầu rút tiền.

**Bảng chính:**

| Bảng | Kiểu PK | Mục đích |
|------|---------|---------|
| `HoSoGiangVien` | GUID (PK=FK) | Profile mở rộng giảng viên, thông tin KYC, tài khoản ngân hàng |
| `SoCaiKeToan` | BIGINT | Sổ cái kế toán — bút toán phân bổ doanh thu sau mỗi đơn hàng |
| `YeuCauRutTien` | INT | Yêu cầu rút tiền — chờ CFO phê duyệt |
| `TongHopDoanhThuNgay` | INT | Bảng aggregate doanh thu hằng ngày (do Cron Job tạo) |
| `ThongKeThoatBaiHoc` | INT (PK=FK) | Thống kê drop-off rate từng bài giảng |

**Quy tắc nghiệp vụ:**
- `HoSoGiangVien.MaGiangVien` vừa là PK vừa là FK tới `NguoiDung` (1-1 shared PK pattern).
- `SoCaiKeToan` là append-only ledger — không UPDATE, chỉ INSERT bút toán mới để điều chỉnh.
- Công thức phân bổ: `DoanhThuNenTang = GiaChot * PhiNenTang%`; `DoanhThuGiangVien = GiaChot - DoanhThuNenTang`.
- `TongHopDoanhThuNgay` được tạo bởi Cron Job lúc 00:00 — không tính toán real-time.
- `ThongKeThoatBaiHoc` được cập nhật batch từ dữ liệu `TienDoHocTap`.

---

### 3.11 Hỗ Trợ Khách Hàng (Support Ticketing)

**Mục đích:** Quản lý vòng đời phiếu hỗ trợ từ tạo → xử lý → đóng.

**Bảng chính:**

| Bảng | Kiểu PK | Mục đích |
|------|---------|---------|
| `PhieuHoTro` | GUID | Ticket: người gửi, người được gán, đơn hàng liên quan |
| `PhanHoiPhieu` | INT | Luồng trao đổi trong từng ticket |

**Quy tắc nghiệp vụ:**
- `PhieuHoTro.PhanCongCho` SET NULL khi nhân viên CS bị xóa — ticket vẫn tồn tại, chờ assign lại.
- `PhieuHoTro.MaDonHangLienQuan` SET NULL khi đơn hàng bị xóa.
- Không được thêm `PhanHoiPhieu` khi `PhieuHoTro.TrangThai = Closed` (business rule, enforce ở application layer).
- Xóa `PhieuHoTro` CASCADE xóa toàn bộ `PhanHoiPhieu`.

---

## 4. Luồng Nghiệp Vụ Cốt Lõi

### 4.1 Luồng Học Viên

```
[Đăng ký] ──► NguoiDung (DangHoatDong=0, EmailDaXacThuc=0)
     │
     ▼
[Xác thực OTP] ──► MaOtp.DaSuDung=1 ──► NguoiDung.EmailDaXacThuc=1
     │
     ▼
[Đăng nhập] ──► PhienLamViec (RefreshToken) + VaiTroNguoiDung
     │
     ▼
[Duyệt khóa học] ──► TuongTacNguoiDung (event: View/Search)
     │
     ▼
[Thêm giỏ hàng] ──► GioHang (lazy create) ──► MatHangGioHang
     │
     ▼
[Checkout] ──► DonHang + ChiTietDonHang (GiaChot snapshot) + MaGiamGia applied
     │
     ▼
[Thanh toán] ──► Payment Gateway ──► Webhook callback
     │
     ▼
[Ghi danh] ──► DonHang.TrangThai=Success ──► GhiDanh (per course)
     │
     ▼
[Học tập] ──► TienDoHocTap (per lecture) + GhiChu + LanLamBai/DapAnHocVien
     │
     ▼
[Hoàn thành 100%] ──► ChungChi (auto-generate)
```

### 4.2 Luồng Giảng Viên

```
[Đăng ký] ──► NguoiDung + VaiTroNguoiDung (Instructor) + HoSoGiangVien (KYC)
     │
     ▼
[Tạo khóa học] ──► KhoaHoc (TrangThai=0/Draft)
     │
     ▼
[Upload nội dung] ──► TaiNguyenSo (Pending) ──► MaHoaVideo (encode job)
     │                                               ──► TaiNguyenSo (Ready)
     ▼
[Cấu trúc nội dung] ──► Chuong + BaiGiang + BaiKiemTra/CauHoi/LuaChonDapAn
     │
     ▼
[Gửi duyệt] ──► KhoaHoc.TrangThai=1 (Pending)
     │
     ▼
[Kiểm duyệt] ──► KhoaHoc.TrangThai=2 (Published) HOẶC 3 (Rejected + GhiChuTuChoi)
     │
     ▼
[Xuất bản] ──► Học viên có thể mua ──► SoCaiKeToan (ledger entry per sale)
     │
     ▼
[Yêu cầu rút tiền] ──► YeuCauRutTien ──► CFO phê duyệt ──► Payout
```

### 4.3 Luồng Hoàn Tiền

```
[Học viên yêu cầu] ──► HoanTien (TrangThai=Pending)
     │
     ▼
[CS/CFO xét duyệt] ──► HoanTien.TrangThai=Approved
     │
     ▼
[Xử lý hoàn tiền] ──► Payment Gateway refund API
     │
     ▼
[Ghi sổ cái] ──► SoCaiKeToan (bút toán âm) ──► GhiDanh.TrangThai=Revoked
```

---

## 5. Nguyên Tắc Thiết Kế Database

### 5.1 Chiến Lược Khóa Chính

| Nhóm bảng | Kiểu PK | Lý do |
|-----------|---------|-------|
| Entity chính (NguoiDung, KhoaHoc, DonHang, GhiDanh...) | `UNIQUEIDENTIFIER` + `NEWSEQUENTIALID()` | Phân tán, merge-safe, tránh index fragmentation |
| Lookup / Config (VaiTro, DanhMuc, NhanTuKhoa...) | `INT IDENTITY` | Compact, FK nhẹ, ít join cost |
| Audit/Log (NhatKyKiemToan, TongHopDoanhThuNgay) | `BIGINT IDENTITY` | Volume lớn, append-only |
| 1-1 shared PK (HoSoGiangVien, ThongKeThoatBaiHoc) | FK làm PK | Cưỡng chế quan hệ 1-1 |

### 5.2 Soft Delete

- **Bảng áp dụng:** `NguoiDung`, `KhoaHoc` và các entity trung tâm có giá trị lịch sử.
- **Cột:** `DaXoa BIT NOT NULL DEFAULT 0`.
- **Nguyên tắc:** Mọi query business logic phải thêm `WHERE DaXoa = 0`. Không filter `DaXoa` trong audit/report query.
- **Không áp dụng:** Bảng event log (`NhatKyKiemToan`, `TuongTacNguoiDung`), bảng junction, bảng aggregate.

### 5.3 Quy Tắc ON DELETE

| Quy tắc | Áp dụng khi | Ví dụ |
|---------|-------------|-------|
| `CASCADE` | Bản ghi con không có ý nghĩa khi cha bị xóa | KhoaHoc → Chuong → BaiGiang |
| `RESTRICT` / `NO ACTION` | Bảo toàn tính toàn vẹn — phải xử lý con trước | NguoiDung → DonHang |
| `SET NULL` | Bản ghi con vẫn hợp lệ khi cha không còn | DonHang.MaGiamGia → NULL |

### 5.4 Denormalized Counters

Các cột aggregate trên `KhoaHoc` phải được cập nhật đồng bộ khi dữ liệu nguồn thay đổi:

- `TongThoiLuong` ← tổng `BaiGiang.ThoiLuong` trong khóa
- `TongBaiGiang` ← COUNT `BaiGiang` trong khóa
- `DiemDanhGiaTrungBinh` ← AVG `DanhGia.DiemSo`
- `TongDanhGia` ← COUNT `DanhGia`
- `TongGhiDanh` ← COUNT `GhiDanh` active

### 5.5 Indexing Conventions

- FK columns phải có index.
- `Email`, `DuongDanURL` (slug) có UNIQUE index.
- Bảng log/event: index trên `(MaNguoiDung, ThoiDiem)`.
- Bảng progress: composite index trên `(MaGhiDanh, MaBaiGiang)`.

---

## 6. Quy Ước Đặt Tên

### 6.1 Database (T-SQL)

| Thành phần | Quy ước | Ví dụ |
|-----------|---------|-------|
| Tên bảng | PascalCase, tiếng Việt có dấu | `NguoiDung`, `KhoaHoc`, `ChiTietDonHang` |
| Tên cột | PascalCase, tiếng Việt có dấu | `MaNguoiDung`, `NgayTao`, `DaXoa` |
| PK column | `Ma` + TênBảng | `MaNguoiDung`, `MaKhoaHoc` |
| FK column | `Ma` + TênBảngCha | `MaKhoaHoc`, `MaGiangVien` |
| Cột boolean | prefix `Da`, `La`, `Dang` | `DaXoa`, `LaNhanVien`, `DangHoatDong` |
| Cột ngày giờ | `NgayTao`, `NgayCapNhat`, `HetHanLuc` | — |
| Constraint PK | `PK_TênBảng` | `PK_NguoiDung` |
| Constraint FK | `FK_TênBảngCon_TênBảngCha` | `FK_MaOtp_NguoiDung` |
| Constraint UQ | `UQ_TênBảng_TênCột` | `UQ_NguoiDung_Email` |

### 6.2 API (REST)

| Thành phần | Quy ước | Ví dụ |
|-----------|---------|-------|
| Endpoint path | kebab-case, tiếng Anh | `/api/v1/courses`, `/api/v1/order-items` |
| JSON field | camelCase | `courseId`, `createdAt`, `isDeleted` |
| Query param | camelCase | `?pageSize=20&sortBy=createdAt` |
| Enum value | SCREAMING_SNAKE_CASE | `PENDING`, `PUBLISHED`, `SET_NULL` |

### 6.3 Mapping Bảng ↔ API Resource

| Bảng DB | API Resource |
|---------|-------------|
| `NguoiDung` | `/users` |
| `KhoaHoc` | `/courses` |
| `BaiGiang` | `/courses/{id}/lectures` |
| `GioHang` | `/cart` |
| `DonHang` | `/orders` |
| `GhiDanh` | `/enrollments` |
| `TienDoHocTap` | `/progress` |
| `PhieuHoTro` | `/support/tickets` |

---

## 7. Hướng Dẫn Thiết Kế API

### 7.1 Cấu Trúc Response

```json
{
  "success": true,
  "data": { ... },
  "meta": {
    "page": 1,
    "pageSize": 20,
    "total": 150
  },
  "error": null
}
```

### 7.2 Phân Trang

- Mặc định: `page=1`, `pageSize=20`, `maxPageSize=100`.
- Bảng event log (`TuongTacNguoiDung`, `NhatKyKiemToan`): cursor-based pagination thay vì offset.

### 7.3 Soft Delete trong API

- `GET /courses` → mặc định chỉ trả `DaXoa=0`.
- `GET /courses?includeDeleted=true` → chỉ dành cho Admin role.
- `DELETE /courses/{id}` → không xóa vật lý, chỉ set `DaXoa=1` + ghi `NhatKyKiemToan`.

### 7.4 Authentication & Authorization

- Access Token: JWT short-lived (15 phút).
- Refresh Token: lưu trong `PhienLamViec`, TTL 7 ngày.
- RBAC check: đọc `VaiTroNguoiDung` của user hiện tại, map với permission matrix.
- Mọi action quan trọng (xóa, approve, payout) phải ghi `NhatKyKiemToan`.

### 7.5 Idempotency

- `POST /orders/{id}/pay` phải idempotent — kiểm tra `DonHang.TrangThai` trước khi tạo `GhiDanh`.
- Payment webhook handler phải có idempotency key để tránh double enrollment.

### 7.6 HTTP Status Conventions

| Tình huống | Status Code |
|-----------|-------------|
| Tạo mới thành công | 201 Created |
| Cập nhật/xóa thành công | 200 OK |
| Không tìm thấy | 404 Not Found |
| Vi phạm business rule | 422 Unprocessable Entity |
| Chưa xác thực | 401 Unauthorized |
| Không có quyền | 403 Forbidden |
| Conflict (trùng email, đã ghi danh) | 409 Conflict |

---

## 8. Cấu Trúc Dự Án

```
/
├── src/
│   ├── modules/
│   │   ├── iam/                    # NguoiDung, VaiTro, MaOtp, PhienLamViec, NhatKyKiemToan
│   │   ├── course/                 # KhoaHoc, Chuong, BaiGiang, BaiKiemTra, CauHoi, LuaChonDapAn
│   │   ├── media/                  # TaiNguyenSo, MaHoaVideo
│   │   ├── ecommerce/              # GioHang, DonHang, ChiTietDonHang, GhiDanh, HoanTien
│   │   ├── learning/               # TienDoHocTap, LanLamBai, DapAnHocVien, ChungChi, GhiChu
│   │   ├── engagement/             # DanhGia, ChuDeHoiDap, TraLoiHoiDap
│   │   ├── marketing/              # ChienDich, MaGiamGia, TuongTacNguoiDung, ThongBaoKhoaHoc
│   │   ├── finance/                # HoSoGiangVien, SoCaiKeToan, YeuCauRutTien, TongHopDoanhThuNgay
│   │   ├── scheduling/             # LichHoc, DiemDanh, ChuoiHocLienTiep
│   │   └── support/                # PhieuHoTro, PhanHoiPhieu
│   ├── shared/
│   │   ├── database/               # Connection pool, migrations, seeds
│   │   ├── auth/                   # JWT middleware, RBAC guard
│   │   ├── audit/                  # NhatKyKiemToan interceptor
│   │   ├── storage/                # S3/Azure Blob adapter
│   │   └── payment/                # Payment gateway adapter, webhook handler
│   └── jobs/                       # Cron jobs: revenue aggregation, streak reset, reminder
├── database/
│   ├── migrations/
│   ├── seeds/
│   └── LMS_Database_VI.sql         # Schema gốc
└── docs/
    ├── DB_Design_LMS_v3_VI.docx
    ├── DB_Relations_LMS_v3_VI.docx
    └── LMS_dbdiagram.dbml
```

---

## 9. Ràng Buộc Dành Cho AI

Phần này định nghĩa các nguyên tắc bất biến mà AI assistant phải tuân thủ khi sinh code, query, hoặc schema cho dự án này.

### 9.1 Database Constraints

- **KHÔNG bao giờ** dùng `DELETE` vật lý trên các bảng có cột `DaXoa` — phải dùng `UPDATE DaXoa=1`.
- **KHÔNG bao giờ** dùng `NEWID()` cho PK — phải dùng `NEWSEQUENTIALID()` để tránh index fragmentation.
- **KHÔNG bao giờ** cập nhật `ChiTietDonHang.GiaChot` sau khi đơn hàng đã có trạng thái thành công — đây là immutable snapshot.
- **KHÔNG bao giờ** xóa bản ghi `SoCaiKeToan` — sổ cái chỉ được INSERT thêm bút toán đảo.
- **LUÔN** filter `DaXoa = 0` trong query business logic trừ khi yêu cầu explicit là lấy dữ liệu đã xóa.
- **LUÔN** include `NgayCapNhat = GETDATE()` khi UPDATE bảng có cột này.
- Khi tạo `GhiDanh`, **phải** kiểm tra xem `(MaHocVien, MaKhoaHoc)` đã tồn tại chưa (UNIQUE constraint).
- Khi tính tỷ lệ hoàn thành, **phải** dùng `KhoaHoc.TongBaiGiang` (denormalized) làm mẫu số — không COUNT trực tiếp `BaiGiang` mỗi lần.

### 9.2 Business Logic Constraints

- **Không cho phép** học viên mua lại khóa học đã có `GhiDanh` active.
- **Không cho phép** giảng viên sửa nội dung khóa học đang ở trạng thái `Pending (1)` — phải Reject trước.
- **Không cho phép** tạo `YeuCauRutTien` nếu số dư khả dụng trong `SoCaiKeToan` không đủ.
- **Không cho phép** xóa `KhoaHoc` nếu còn `GhiDanh` active — phải Archive (`TrangThai=4`) thay thế.
- **Không cho phép** apply cùng một `MaGiamGia` cho cùng một học viên hai lần (business rule, enforce ở application layer).
- Khi tạo `ChungChi`, **phải** kiểm tra `COUNT(TienDoHocTap WHERE HoanThanh=1) = KhoaHoc.TongBaiGiang`.

### 9.3 API Generation Constraints

- Mọi endpoint thay đổi trạng thái quan trọng (approve/reject course, process payout, refund) **phải** ghi `NhatKyKiemToan`.
- Endpoint trả về danh sách **phải** support pagination — không trả toàn bộ bảng.
- FK lookup fields trong response **phải** include object đầy đủ (hoặc `{id, name}` minimum) — không chỉ trả mã FK.
- Endpoint public (không cần auth) chỉ được trả dữ liệu của `KhoaHoc.TrangThai = 2 (Published)` và `DaXoa = 0`.

### 9.4 Naming Constraints

- **Không được** tự đặt tên bảng/cột mới bằng tiếng Anh nếu domain đó đã có tên tiếng Việt trong schema.
- Khi thêm cột mới vào bảng hiện có, **phải** tuân thủ quy ước đặt tên tại mục 6.1.
- Tên API endpoint và JSON field **phải** dùng tiếng Anh (camelCase/kebab-case) — không dùng tiếng Việt trong API contract.

### 9.5 Schema Extension Constraints

Khi thêm tính năng mới, **ưu tiên** mở rộng theo thứ tự:

1. Thêm cột nullable vào bảng hiện có (nếu quan hệ 1-1).
2. Tạo bảng extension với PK = FK (pattern như `HoSoGiangVien`).
3. Tạo bảng mới với FK về entity hiện có.
4. Tạo junction table nếu quan hệ N-N.

**Không tạo** cột `data NVARCHAR(MAX)` kiểu blob JSON để bypass schema — phải normalize.

### 9.6 Transaction Boundaries

Các operation sau **bắt buộc** chạy trong một transaction:

- Xử lý payment webhook: cập nhật `DonHang` + tạo `GhiDanh` (n bản ghi) + ghi `NhatKyKiemToan`.
- Approve payout: cập nhật `YeuCauRutTien` + INSERT `SoCaiKeToan` bút toán âm + ghi `NhatKyKiemToan`.
- Refund: cập nhật `HoanTien` + INSERT `SoCaiKeToan` + cập nhật `GhiDanh.TrangThai`.
- Tạo khóa học (submit): cập nhật `KhoaHoc.TrangThai` + ghi `NhatKyKiemToan`.

---

*Tài liệu này được tạo từ schema `LMS_Database_VI.sql`, `LMS_dbdiagram.dbml`, `DB_Design_LMS_v3_VI.docx`, `DB_Relations_LMS_v3_VI.docx` và `Tài_liệu_Đặc_tả_Kỹ_thuật_Nghiệp_vụ.docx`.*  
*Cập nhật khi có thay đổi schema hoặc nghiệp vụ.*