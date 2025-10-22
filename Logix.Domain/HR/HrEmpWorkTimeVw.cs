using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrEmpWorkTimeVw
    {
        public long EmpWorkId { get; set; }
        [Column("Emp_Id")]
        public long? EmpId { get; set; }
        [Column("In_AM")]
        [StringLength(50)]
        public string? InAm { get; set; }
        [Column("Out_AM")]
        [StringLength(50)]
        public string? OutAm { get; set; }
        [Column("In_PM")]
        [StringLength(50)]
        public string? InPm { get; set; }
        [Column("Out_PM")]
        [StringLength(50)]
        public string? OutPm { get; set; }
        [Column("In_PM_Yest")]
        [StringLength(50)]
        public string? InPmYest { get; set; }
        [Column("Out_PM_Yest")]
        [StringLength(50)]
        public string? OutPmYest { get; set; }
        public long? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
        public bool? IsDeleted { get; set; }
        [Column("MinuteAM")]
        public int? MinuteAm { get; set; }
        [Column("MinutePM")]
        public int? MinutePm { get; set; }
        [Column("MinutePMYest")]
        public int? MinutePmyest { get; set; }
        [Column("Emp_name")]
        [StringLength(250)]
        public string? EmpName { get; set; }
        [Column("BRA_NAME")]
        public string? BraName { get; set; }
        [Column("BRANCH_ID")]
        public int? BranchId { get; set; }
        [StringLength(10)]
        public string? FromDate { get; set; }
        [StringLength(10)]
        public string? ToDate { get; set; }
        [Column("Emp_Code")]
        [StringLength(50)]
        public string EmpCode { get; set; } = null!;
    }
}
