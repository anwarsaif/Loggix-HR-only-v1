using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Logix.Application.DTOs.HR
{
    public class HrLicenseDto
    {
        public long? Id { get; set; }
        //[Required]
        public long? EmpId { get; set; }
        public int? JobCat { get; set; }
        [Required]
        public int? LicenseType { get; set; }
        [StringLength(50)]
        public string? LicenseNo { get; set; }
        [StringLength(50)]
        [Required]
        public string? LicenseFormerPlace { get; set; }
        [StringLength(10)]
        public string? IssuedDate { get; set; }
        [StringLength(10)]
        public string? ExpiryDate { get; set; }
        [StringLength(250)]
        public string? FileUrl { get; set; }
        public string? EmpCode { get; set; }
        public string? Note { get; set; }

        public bool? IsDeleted { get; set; }
    }
    public class HrLicenseEditDto
    {
        public long Id { get; set; }
        public long? EmpId { get; set; }
        public string? EmpCode { get; set; }
        public string? EmpName { get; set; }
        public int? JobCat { get; set; }
        public int? LicenseType { get; set; }
        [StringLength(50)]
        public string? LicenseNo { get; set; }
        [StringLength(50)]
        public string? LicenseFormerPlace { get; set; }
        [StringLength(10)]
        public string? IssuedDate { get; set; }
        [StringLength(10)]
        public string? ExpiryDate { get; set; }
        [StringLength(250)]
        public string? FileUrl { get; set; }
        public string? Note { get; set; }

    }

    public class HrLicensesFilterDto
    {
        public string? EmpCode { get; set; }
        public int? LicenseType { get; set; }
        public string? LicenseNo { get; set; }
        public string? IssuedDate { get; set; }
        public string? ExpiryDate { get; set; }
        public string? EmpName { get; set; }
    }
}
