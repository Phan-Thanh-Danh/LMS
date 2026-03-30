using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.Instructor
{
    public class KycSubmissionDto
    {
        [Required]
        public Guid CCCDFrontAssetId { get; set; }

        [Required]
        public Guid CCCDBackAssetId { get; set; }

        public List<Guid> CertificateAssetIds { get; set; } = new List<Guid>();

        [MaxLength(1000)]
        public string? BankInfo { get; set; }

        [MaxLength(200)]
        public string? TaxCode { get; set; }
    }

    public class KycReviewDto
    {
        [Required]
        public bool IsApproved { get; set; }

        [MaxLength(1000)]
        public string? RejectionReason { get; set; }
    }
}
