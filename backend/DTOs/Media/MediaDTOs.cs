using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace backend.DTOs.Media
{
    public class ChunkUploadInitDto
    {
        [Required]
        public string FileName { get; set; } = string.Empty;

        [Required]
        public string ContentType { get; set; } = string.Empty;

        [Required]
        public long FileSize { get; set; }
    }

    public class ChunkUploadDto
    {
        [Required]
        public Guid AssetId { get; set; }

        [Required]
        public int ChunkIndex { get; set; }

        [Required]
        public int TotalChunks { get; set; }

        [Required]
        public IFormFile ChunkData { get; set; } = null!;
    }
}
