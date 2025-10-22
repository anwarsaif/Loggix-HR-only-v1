using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Logix.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Table("HR_Licenses")]
    public partial class HrLicense : TraceEntity
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Column("Emp_ID")]
        public long? EmpId { get; set; }
        public int? JobCat { get; set; }
        [Column("License_Type")]
        public int? LicenseType { get; set; }
        [Column("License_No")]
        [StringLength(50)]
        public string? LicenseNo { get; set; }
        [Column("License_Former_Place")]
        [StringLength(50)]
        public string? LicenseFormerPlace { get; set; }
        [Column("Issued_Date")]
        [StringLength(10)]
        public string? IssuedDate { get; set; }
        [Column("Expiry_Date")]
        [StringLength(10)]
        public string? ExpiryDate { get; set; }
        [Column("File_URL")]
        [StringLength(250)]
        public string? FileUrl { get; set; }
        public string? Note { get; set; }
    }
}
