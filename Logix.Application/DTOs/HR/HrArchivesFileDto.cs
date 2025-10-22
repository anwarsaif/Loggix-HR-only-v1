using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Logix.Application.DTOs.HR
{
    public class HrArchivesFileDto
    {
        public long ArchiveFileId { get; set; }
        [Required]
        public string? EmpCode { get; set; }
        [Required]
        public string? FileTypeId { get; set; }
        public int? Qty { get; set; }
        [Required]
        public string? Url { get; set; }
        [StringLength(50)]
        public string? NoFolder { get; set; }
        [StringLength(50)]
        public string? NoShelf { get; set; }
        [StringLength(50)]
        public string? NoSafe { get; set; }
        [StringLength(50)]
        [Required]
        public string? Note { get; set; }
        [StringLength(50)]
        public string? NoCartoon { get; set; }
        public int? EmpTypeId { get; set; }
        [StringLength(50)]
        public string? ArchiveDate { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? ShowEmp { get; set; }
        public string? DocName { get; set; }
    }
    public class HrArchivesFileEditDto
    {
        public long ArchiveFileId { get; set; }

        public string? EmpCode { get; set; }
        public string? FileTypeId { get; set; }
        public int? Qty { get; set; }
        [Required]
        public string? Url { get; set; }
        [StringLength(50)]
        public string? NoFolder { get; set; }
        [StringLength(50)]
        public string? NoShelf { get; set; }
        [StringLength(50)]
        public string? NoSafe { get; set; }
        [StringLength(50)]
        [Required]
        public string? Note { get; set; }
        [StringLength(50)]
        public string? NoCartoon { get; set; }
        public int? EmpTypeId { get; set; }
        [StringLength(50)]
        public string? ArchiveDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

    }

    public class HrArchievesFilterDto
    {
        public string? FileTypeId { get; set; }
        public int? BranchId { get; set; }
        public string? EmpCode { get; set; }
        public string? EmpName { get; set; }
    }
}
