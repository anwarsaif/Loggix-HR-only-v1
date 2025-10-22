using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrPayrollDeductionVw
    {
        [Column("ID")]
        public long Id { get; set; }
        [Column("MS_ID")]
        public long? MsId { get; set; }
        [Column("MSD_ID")]
        public long? MsdId { get; set; }
        [Column("AD_ID")]
        public long? AdId { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Amount { get; set; }
        [StringLength(250)]
        public string? Name { get; set; }
        public bool IsDeleted { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Debit { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Credit { get; set; }
        [Column("Emp_ID")]
        public long? EmpId { get; set; }
        [Column("Emp_name")]
        [StringLength(250)]
        public string? EmpName { get; set; }
        [Column("Branch_ID")]
        public long? BranchId { get; set; }
        [Column("Dept_ID")]
        public long? DeptId { get; set; }
        public long? Location { get; set; }
        [Column("Emp_Code")]
        [StringLength(50)]
        public string EmpCode { get; set; } = null!;
        [Column("CC_ID")]
        public long? CcId { get; set; }
        [Column("Salary_Group_ID")]
        public long? SalaryGroupId { get; set; }
        [Column("Location_CC_ID")]
        public long? LocationCcId { get; set; }
        [Column("Dept_CC_ID")]
        public long? DeptCcId { get; set; }
        [Column("Branch_CC_ID")]
        public int? BranchCcId { get; set; }
        [Column("Branch_CC_Name")]
        [StringLength(150)]
        public string? BranchCcName { get; set; }
        [Column("Branch_CC_Name2")]
        [StringLength(150)]
        public string? BranchCcName2 { get; set; }
        [Column("Branch_CC_Code")]
        [StringLength(50)]
        public string? BranchCcCode { get; set; }
        [StringLength(250)]
        public string? Name2 { get; set; }
        [Column("CC_ID3")]
        public long? CcId3 { get; set; }
        [Column("CC_ID2")]
        public long? CcId2 { get; set; }
        [Column("CC_ID4")]
        public long? CcId4 { get; set; }
        [Column("CC_ID5")]
        public long? CcId5 { get; set; }
        [Column("CC_Rate", TypeName = "decimal(18, 2)")]
        public decimal? CcRate { get; set; }
        [Column("CC_Rate3", TypeName = "decimal(18, 2)")]
        public decimal? CcRate3 { get; set; }
        [Column("CC_Rate2", TypeName = "decimal(18, 2)")]
        public decimal? CcRate2 { get; set; }
        [Column("CC_Rate4", TypeName = "decimal(18, 2)")]
        public decimal? CcRate4 { get; set; }
        [Column("CC_Rate5", TypeName = "decimal(18, 2)")]
        public decimal? CcRate5 { get; set; }
    }
}
