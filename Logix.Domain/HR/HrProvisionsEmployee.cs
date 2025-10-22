using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Logix.Domain.HR
{
    [Table("HR_Provisions_Employee", Schema = "dbo")]
    public partial class HrProvisionsEmployee
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Column("P_ID")]
        public long? PId { get; set; }
        [Column("Emp_ID")]
        public long? EmpId { get; set; }
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
    }
}
