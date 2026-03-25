using System;
using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.Category
{
    public class CategoryGroupResponse
    {
        public int MaDanhMuc { get; set; }
        public string TenDanhMuc { get; set; } = string.Empty;
        public string DuongDanURL { get; set; } = string.Empty;
        public int ThuTuHienThi { get; set; }
        public bool DangHienThi { get; set; }
        public string? DuongDanIcon { get; set; }
        public DateTime NgayTao { get; set; }
        public int SoDanhMucCon { get; set; }
    }

    public class CreateCategoryGroupRequest
    {
        [Required(ErrorMessage = "Tên nhóm danh mục không được để trống")]
        [StringLength(200)]
        public string TenDanhMuc { get; set; } = string.Empty;

        [StringLength(200)]
        public string? DuongDanURL { get; set; }

        public int ThuTuHienThi { get; set; } = 0;
        public string? DuongDanIcon { get; set; }
    }

    public class UpdateCategoryGroupRequest
    {
        [Required(ErrorMessage = "Tên nhóm danh mục không được để trống")]
        [StringLength(200)]
        public string TenDanhMuc { get; set; } = string.Empty;

        [StringLength(200)]
        public string? DuongDanURL { get; set; }

        public int ThuTuHienThi { get; set; }
        public bool DangHienThi { get; set; }
        public string? DuongDanIcon { get; set; }
    }
}
