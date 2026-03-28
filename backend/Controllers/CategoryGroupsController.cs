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
    [Route("api/v1/category-groups")]
    [ApiController]
    public class CategoryGroupsController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryGroupsController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryGroupResponse>>> GetCategoryGroups()
        {
            var groups = await _categoryRepository.GetGroupsAsync();
            var response = groups.Select(g => new CategoryGroupResponse
            {
                MaDanhMuc = g.MaDanhMuc,
                TenDanhMuc = g.TenDanhMuc,
                DuongDanURL = g.DuongDanURL,
                ThuTuHienThi = g.ThuTuHienThi,
                DangHienThi = g.DangHienThi,
                DuongDanIcon = g.DuongDanIcon,
                NgayTao = g.NgayTao,
                // Giả sử có thêm logic đếm số danh mục con ở đây nếu cần
                SoDanhMucCon = 0,
            });

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryGroupResponse>> GetCategoryGroup(int id)
        {
            var g = await _categoryRepository.GetGroupByIdAsync(id);
            if (g == null)
                return NotFound(new { message = "Không tìm thấy nhóm danh mục" });

            return Ok(
                new CategoryGroupResponse
                {
                    MaDanhMuc = g.MaDanhMuc,
                    TenDanhMuc = g.TenDanhMuc,
                    DuongDanURL = g.DuongDanURL,
                    ThuTuHienThi = g.ThuTuHienThi,
                    DangHienThi = g.DangHienThi,
                    DuongDanIcon = g.DuongDanIcon,
                    NgayTao = g.NgayTao,
                    SoDanhMucCon = 0,
                }
            );
        }

        [Authorize(Roles = "Admin,CMO")]
        [HttpPost]
        public async Task<ActionResult<CategoryGroupResponse>> CreateCategoryGroup(
            [FromBody] CreateCategoryGroupRequest request
        )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Tự động tạo Slug nếu trống
            var slug = string.IsNullOrWhiteSpace(request.DuongDanURL)
                ? GenerateSlug(request.TenDanhMuc)
                : request.DuongDanURL;

            if (!await _categoryRepository.IsSlugUniqueAsync(slug))
            {
                return Conflict(new { message = "URL danh mục đã tồn tại" });
            }

            var group = new DanhMuc
            {
                TenDanhMuc = request.TenDanhMuc,
                DuongDanURL = slug,
                ThuTuHienThi = request.ThuTuHienThi,
                DuongDanIcon = request.DuongDanIcon,
                MaDanhMucCha = null, // Vẫn đảm bảo là Nhóm danh mục cấp 1
                DangHienThi = true,
            };

            var success = await _categoryRepository.AddCategoryAsync(group);
            if (!success)
                return StatusCode(500, new { message = "Lỗi khi lưu dữ liệu" });

            return CreatedAtAction(
                nameof(GetCategoryGroup),
                new { id = group.MaDanhMuc },
                new CategoryGroupResponse
                {
                    MaDanhMuc = group.MaDanhMuc,
                    TenDanhMuc = group.TenDanhMuc,
                    DuongDanURL = group.DuongDanURL,
                    ThuTuHienThi = group.ThuTuHienThi,
                    DangHienThi = group.DangHienThi,
                    DuongDanIcon = group.DuongDanIcon,
                    NgayTao = group.NgayTao,
                }
            );
        }

        [Authorize(Roles = "Admin,CMO")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategoryGroup(
            int id,
            [FromBody] UpdateCategoryGroupRequest request
        )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var group = await _categoryRepository.GetGroupByIdAsync(id);
            if (group == null)
                return NotFound(new { message = "Không tìm thấy nhóm danh mục" });

            var slug = string.IsNullOrWhiteSpace(request.DuongDanURL)
                ? GenerateSlug(request.TenDanhMuc)
                : request.DuongDanURL;

            if (!await _categoryRepository.IsSlugUniqueAsync(slug, id))
            {
                return Conflict(new { message = "URL danh mục đã tồn tại" });
            }

            group.TenDanhMuc = request.TenDanhMuc;
            group.DuongDanURL = slug;
            group.ThuTuHienThi = request.ThuTuHienThi;
            group.DangHienThi = request.DangHienThi;
            group.DuongDanIcon = request.DuongDanIcon;

            var success = await _categoryRepository.UpdateGroupAsync(group);
            if (!success)
                return StatusCode(500, new { message = "Lỗi khi cập nhật dữ liệu" });

            return Ok(new { message = "Cập nhật thành công" });
        }

        [Authorize(Roles = "Admin,CMO")]
        [HttpPatch("{id}/status")]
        public async Task<IActionResult> ToggleCategoryGroupStatus(int id)
        {
            var success = await _categoryRepository.ToggleStatusAsync(id);
            if (!success)
                return NotFound(new { message = "Không tìm thấy nhóm danh mục" });

            return Ok(new { message = "Đã cập nhật trạng thái hiển thị" });
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoryGroup(int id)
        {
            // Kiểm tra ràng buộc: Có con không?
            if (await _categoryRepository.HasChildrenAsync(id))
            {
                return Conflict(
                    new
                    {
                        message = "Không thể xóa nhóm đang có danh mục con. Hãy xóa các danh mục con trước.",
                    }
                );
            }

            // Kiểm tra ràng buộc: Có khóa học không?
            if (await _categoryRepository.HasCoursesAsync(id))
            {
                return Conflict(
                    new { message = "Không thể xóa nhóm đang có khóa học trực thuộc." }
                );
            }

            var success = await _categoryRepository.HardDeleteGroupAsync(id);
            if (!success)
                return NotFound(new { message = "Không tìm thấy nhóm danh mục" });

            return Ok(new { message = "Xóa cứng nhóm danh mục thành công" });
        }

        // Helper đơn giản để tạo Slug (trong thực tế nên dùng thư viện chuẩn hơn)
        private string GenerateSlug(string phrase)
        {
            string str = phrase.ToLower();
            // Loại bỏ dấu tiếng Việt và ký tự đặc biệt (đây là bản giản lược)
            str = System.Text.RegularExpressions.Regex.Replace(str, @"[^a-z0-9\s-]", "");
            str = System.Text.RegularExpressions.Regex.Replace(str, @"\s+", " ").Trim();
            str = str.Replace(" ", "-");
            return str;
        }
    }
}
