using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class BaiGiang
    {
        [Key]
        public int MaBaiGiang { get; set; }
        public int MaChuong { get; set; }

        [Required]
        [MaxLength(500)]
        public string TieuDe { get; set; }

        [Required]
        [MaxLength(50)]
        public string LoaiBaiGiang { get; set; }
        public Guid? MaTaiNguyen { get; set; }
        public string? MoTa { get; set; }
        public int? ThoiLuong { get; set; }
        public int ThuTu { get; set; }
        public bool XemMienPhi { get; set; } = false;
        public int TyLeXemToiThieu { get; set; } = 80;
        public bool DangKhoa { get; set; } = false;
        public DateTime NgayTao { get; set; } = DateTime.Now;

        [ForeignKey("MaChuong")]
        public virtual Chuong Chuong { get; set; }

        [ForeignKey("MaTaiNguyen")]
        public virtual TaiNguyenSo? TaiNguyenSo { get; set; }
    }
}
