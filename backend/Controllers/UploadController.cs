using System.IO;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;

        public UploadController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        [HttpPost("avatar")]
        [RequestSizeLimit(5 * 1024 * 1024)] // 5MB limit
        public async Task<IActionResult> UploadAvatar(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest(new { message = "Không tìm thấy tệp đính kèm." });
            }

            // Validations
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (string.IsNullOrEmpty(extension) || !allowedExtensions.Contains(extension))
            {
                return BadRequest(
                    new { message = "Định dạng ảnh không hợp lệ. Chỉ chấp nhận JPG, PNG, GIF." }
                );
            }

            // Setup paths
            var uploadsFolder = Path.Combine(
                _environment.WebRootPath
                    ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"),
                "uploads",
                "avatars"
            );
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var uniqueFileName = Guid.NewGuid().ToString() + extension;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            // Return relative URL (this assumes the app is using static files)
            var fileUrl = $"/uploads/avatars/{uniqueFileName}";

            return Ok(new { url = fileUrl, message = "Tải ảnh thành công." });
        }
    }
}
