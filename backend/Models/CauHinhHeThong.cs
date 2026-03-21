using System;
using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class CauHinhHeThong
    {
        [Key]
        [MaxLength(200)]
        public string KhoaCauHinh { get; set; }

        [Required]
        public string GiaTriCauHinh { get; set; }

        [Required]
        [MaxLength(50)]
        public string KieuDuLieu { get; set; }

        [MaxLength(500)]
        public string? MoTa { get; set; }
        public DateTime? NgayCapNhat { get; set; }
        public Guid? CapNhatBoi { get; set; }
    }
}
