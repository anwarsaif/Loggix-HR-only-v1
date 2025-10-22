using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrPayrollAllowanceAccountsVw
    {
        [Column("Type_ID")]
        public int? TypeId { get; set; }
        [Column("Account_Exp_ID")]
        public long? AccountExpId { get; set; }
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
        [Column("Dept_ID")]
        public long? DeptId { get; set; }
        [Column("Branch_ID")]
        public long? BranchId { get; set; }
        [Column("Emp_Code")]
        [StringLength(50)]
        public string EmpCode { get; set; } = null!;
        public long? Location { get; set; }
        [Column("Emp_name")]
        [StringLength(250)]
        public string? EmpName { get; set; }
        [Column("Salary_Group_ID")]
        public long? SalaryGroupId { get; set; }
        [Column("CC_ID")]
        public long? CcId { get; set; }
        public bool? Expr1 { get; set; }
        [Column("Account_Due_ID")]
        public long? AccountDueId { get; set; }
        [Column("Location_CC_ID")]
        public long? LocationCcId { get; set; }
        [Column("Dept_CC_ID")]
        public long? DeptCcId { get; set; }
        [Column("Branch_CC_ID")]
        public int? BranchCcId { get; set; }
        [Column("Branch_CC_Name")]
        [StringLength(150)]
        public string? BranchCcName { get; set; }
        [Column("Branch_CC_Code")]
        [StringLength(50)]
        public string? BranchCcCode { get; set; }
        [Column("Branch_CC_Name2")]
        [StringLength(150)]
        public string? BranchCcName2 { get; set; }
    }
}
