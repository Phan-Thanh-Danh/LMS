using System;
using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.Category
{
    public class SubCategoryResponse
    {
        public int MaDanhMuc { get; set; }
        public string TenDanhMuc { get; set; } = string.Empty;
        public string DuongDanURL { get; set; } = string.Empty;
        public int MaDanhMucCha { get; set; }
        public string? TenNhomCha { get; set; }
        public int ThuTuHienThi { get; set; }
        public bool DangHienThi { get; set; }
        public string? DuongDanIcon { get; set; }
        public DateTime NgayTao { get; set; }
    }

    public class CreateSubCategoryRequest
    {
        [Required(ErrorMessage = "Tên danh mục con không được để trống")]
        [StringLength(200)]
        public string TenDanhMuc { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phải chọn Nhóm danh mục cha")]
        public int MaDanhMucCha { get; set; }

        [StringLength(200)]
        public string? DuongDanURL { get; set; }

        public int ThuTuHienThi { get; set; } = 0;
        public string? DuongDanIcon { get; set; }
    }

    public class UpdateSubCategoryRequest
    {
        [Required(ErrorMessage = "Tên danh mục con không được để trống")]
        [StringLength(200)]
        public string TenDanhMuc { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phải chọn Nhóm danh mục cha")]
        public int MaDanhMucCha { get; set; }

        [StringLength(200)]
        public string? DuongDanURL { get; set; }

        public int ThuTuHienThi { get; set; }
        public bool DangHienThi { get; set; }
        public string? DuongDanIcon { get; set; }
    }
}
