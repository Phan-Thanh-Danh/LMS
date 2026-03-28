using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.DTOs.Category;
using backend.Models;
using backend.Repository.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/v1/sub-categories")]
    [ApiController]
    public class SubCategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public SubCategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubCategoryResponse>>> GetSubCategories(
            [FromQuery] int? parentId
        )
        {
            var subCats = await _categoryRepository.GetSubCategoriesAsync(parentId);
            var response = subCats.Select(s => new SubCategoryResponse
            {
                MaDanhMuc = s.MaDanhMuc,
                TenDanhMuc = s.TenDanhMuc,
                DuongDanURL = s.DuongDanURL,
                MaDanhMucCha = s.MaDanhMucCha ?? 0,
                TenNhomCha = s.DanhMucCha?.TenDanhMuc,
                ThuTuHienThi = s.ThuTuHienThi,
                DangHienThi = s.DangHienThi,
                DuongDanIcon = s.DuongDanIcon,
                NgayTao = s.NgayTao,
            });

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SubCategoryResponse>> GetSubCategory(int id)
        {
            var s = await _categoryRepository.GetSubCategoryByIdAsync(id);
            if (s == null)
                return NotFound(new { message = "Không tìm thấy danh mục con" });

            return Ok(
                new SubCategoryResponse
                {
                    MaDanhMuc = s.MaDanhMuc,
                    TenDanhMuc = s.TenDanhMuc,
                    DuongDanURL = s.DuongDanURL,
                    MaDanhMucCha = s.MaDanhMucCha ?? 0,
                    TenNhomCha = s.DanhMucCha?.TenDanhMuc,
                    ThuTuHienThi = s.ThuTuHienThi,
                    DangHienThi = s.DangHienThi,
                    DuongDanIcon = s.DuongDanIcon,
                    NgayTao = s.NgayTao,
                }
            );
        }

        [Authorize(Roles = "Admin,CMO")]
        [HttpPost]
        public async Task<ActionResult<SubCategoryResponse>> CreateSubCategory(
            [FromBody] CreateSubCategoryRequest request
        )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Kiểm tra nhóm cha tồn tại
            var parent = await _categoryRepository.GetGroupByIdAsync(request.MaDanhMucCha);
            if (parent == null)
                return BadRequest(new { message = "Nhóm danh mục cha không tồn tại" });

            var slug = string.IsNullOrWhiteSpace(request.DuongDanURL)
                ? GenerateSlug(request.TenDanhMuc)
                : request.DuongDanURL;

            if (!await _categoryRepository.IsSlugUniqueAsync(slug))
            {
                return Conflict(new { message = "URL danh mục đã tồn tại" });
            }

            var subCat = new DanhMuc
            {
                TenDanhMuc = request.TenDanhMuc,
                DuongDanURL = slug,
                MaDanhMucCha = request.MaDanhMucCha,
                ThuTuHienThi = request.ThuTuHienThi,
                DuongDanIcon = request.DuongDanIcon,
                DangHienThi = true,
            };

            var success = await _categoryRepository.AddCategoryAsync(subCat);
            if (!success)
                return StatusCode(500, new { message = "Lỗi khi lưu dữ liệu" });

            return CreatedAtAction(
                nameof(GetSubCategory),
                new { id = subCat.MaDanhMuc },
                new SubCategoryResponse
                {
                    MaDanhMuc = subCat.MaDanhMuc,
                    TenDanhMuc = subCat.TenDanhMuc,
                    DuongDanURL = subCat.DuongDanURL,
                    MaDanhMucCha = subCat.MaDanhMucCha ?? 0,
                    TenNhomCha = parent.TenDanhMuc,
                    ThuTuHienThi = subCat.ThuTuHienThi,
                    DangHienThi = subCat.DangHienThi,
                    DuongDanIcon = subCat.DuongDanIcon,
                    NgayTao = subCat.NgayTao,
                }
            );
        }

        [Authorize(Roles = "Admin,CMO")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubCategory(
            int id,
            [FromBody] UpdateSubCategoryRequest request
        )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var subCat = await _categoryRepository.GetSubCategoryByIdAsync(id);
            if (subCat == null)
                return NotFound(new { message = "Không tìm thấy danh mục con" });

            // Kiểm tra nhóm cha mới (nếu đổi)
            var parent = await _categoryRepository.GetGroupByIdAsync(request.MaDanhMucCha);
            if (parent == null)
                return BadRequest(new { message = "Nhóm danh mục cha không tồn tại" });

            var slug = string.IsNullOrWhiteSpace(request.DuongDanURL)
                ? GenerateSlug(request.TenDanhMuc)
                : request.DuongDanURL;

            if (!await _categoryRepository.IsSlugUniqueAsync(slug, id))
            {
                return Conflict(new { message = "URL danh mục đã tồn tại" });
            }

            subCat.TenDanhMuc = request.TenDanhMuc;
            subCat.DuongDanURL = slug;
            subCat.MaDanhMucCha = request.MaDanhMucCha;
            subCat.ThuTuHienThi = request.ThuTuHienThi;
            subCat.DangHienThi = request.DangHienThi;
            subCat.DuongDanIcon = request.DuongDanIcon;

            var success = await _categoryRepository.UpdateGroupAsync(subCat);
            if (!success)
                return StatusCode(500, new { message = "Lỗi khi cập nhật dữ liệu" });

            return Ok(new { message = "Cập nhật thành công" });
        }

        [Authorize(Roles = "Admin,CMO")]
        [HttpPatch("{id}/status")]
        public async Task<IActionResult> ToggleSubCategoryStatus(int id)
        {
            var success = await _categoryRepository.ToggleStatusAsync(id);
            if (!success)
                return NotFound(new { message = "Không tìm thấy danh mục con" });

            return Ok(new { message = "Đã cập nhật trạng thái hiển thị" });
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubCategory(int id)
        {
            // Kiểm tra ràng buộc khóa học
            if (await _categoryRepository.HasCoursesAsync(id))
            {
                return Conflict(
                    new { message = "Không thể xóa danh mục đang có khóa học trực thuộc." }
                );
            }

            var success = await _categoryRepository.HardDeleteGroupAsync(id);
            if (!success)
                return NotFound(new { message = "Không tìm thấy danh mục con" });

            return Ok(new { message = "Xóa cứng danh mục con thành công" });
        }

        private string GenerateSlug(string phrase)
        {
            string str = phrase.ToLower();
            str = System.Text.RegularExpressions.Regex.Replace(str, @"[^a-z0-9\s-]", "");
            str = System.Text.RegularExpressions.Regex.Replace(str, @"\s+", " ").Trim();
            str = str.Replace(" ", "-");
            return str;
        }
    }
}
