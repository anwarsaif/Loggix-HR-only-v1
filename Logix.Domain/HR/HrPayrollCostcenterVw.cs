using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrPayrollCostcenterVw
    {
        [Column("Location_Name")]
        [StringLength(200)]
        public string LocationName { get; set; } = null!;
        [Column("ID")]
        public long Id { get; set; }
        [Column("MS_ID")]
        public long? MsId { get; set; }
        [Column("MSD_ID")]
        public long? MsdId { get; set; }
        [Column("Emp_ID")]
        public long? EmpId { get; set; }
        [Column("Facility_ID")]
        public long? FacilityId { get; set; }
        [Column("Branch_ID")]
        public long? BranchId { get; set; }
        [Column("Dep_ID")]
        public long? DepId { get; set; }
        [Column("Location_ID")]
        public long? LocationId { get; set; }
        [Column("Count_Day_Work")]
        public int? CountDayWork { get; set; }
        [Column("CC_ID")]
        public long? CcId { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Rate { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Amount { get; set; }
        public string? Note { get; set; }
        public long? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        [Column("Start_Date")]
        [StringLength(10)]
        public string? StartDate { get; set; }
        [Column("End_Date")]
        [StringLength(50)]
        public string? EndDate { get; set; }
        [Column("MS_Month")]
        [StringLength(2)]
        public string? MsMonth { get; set; }
        public int? FinancelYear { get; set; }
        [Column("Payroll_Type_ID")]
        public int? PayrollTypeId { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Salary { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? OverTime { get; set; }
        [Column("allowance", TypeName = "decimal(18, 2)")]
        public decimal? Allowance { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Loan { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Deduction { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Absence { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Delay { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Penalties { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Net { get; set; }
    }
}
