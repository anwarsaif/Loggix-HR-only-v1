using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrProvisionsEmployeeVw
    {
        [Column("ID")]
        public long Id { get; set; }
        [Column("P_ID")]
        public long? PId { get; set; }
        [Column("Basic_Salary", TypeName = "decimal(18, 2)")]
        public decimal? BasicSalary { get; set; }
        [Column("Total_Allowances", TypeName = "decimal(18, 2)")]
        public decimal? TotalAllowances { get; set; }
        [Column("Total_Deductions", TypeName = "decimal(18, 2)")]
        public decimal? TotalDeductions { get; set; }
        [Column("Net_Salary", TypeName = "decimal(18, 2)")]
        public decimal? NetSalary { get; set; }
        [Column("Total_Salary", TypeName = "decimal(18, 2)")]
        public decimal? TotalSalary { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Amount { get; set; }
        [Column("Dept_ID")]
        public long? DeptId { get; set; }
        [Column("Location_ID")]
        public long? LocationId { get; set; }
        [Column("Facility_ID")]
        public long? FacilityId { get; set; }
        [Column("Branch_ID")]
        public long? BranchId { get; set; }
        [Column("Salary_Group_ID")]
        public long? SalaryGroupId { get; set; }
        [Column("CC_ID")]
        public long? CcId { get; set; }
        public long? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        [Column("Emp_name")]
        [StringLength(250)]
        public string? EmpName { get; set; }
        [Column("Entry_NO")]
        [StringLength(50)]
        public string? EntryNo { get; set; }
        [Column("Dep_Name")]
        [StringLength(200)]
        public string? DepName { get; set; }
        [Column("Dep_Name2")]
        [StringLength(200)]
        public string? DepName2 { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string? BirthDate { get; set; }
        [Column("Location_Name")]
        [StringLength(200)]
        public string? LocationName { get; set; }
        [Column("Location_Name2")]
        [StringLength(200)]
        public string? LocationName2 { get; set; }
        [Column("Emp_Code")]
        [StringLength(50)]
        public string EmpCode { get; set; } = null!;
        [Column("Emp_ID")]
        public long? EmpId { get; set; }
        [Column("P_Date")]
        [StringLength(10)]
        public string? PDate { get; set; }
    }
}
