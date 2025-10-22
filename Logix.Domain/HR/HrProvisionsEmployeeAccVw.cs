using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrProvisionsEmployeeAccVw
    {
        [Column("ID")]
        public int Id { get; set; }
        [Column("Type_ID")]
        public long? TypeId { get; set; }
        [StringLength(50)]
        public string? Code { get; set; }
        public long? No { get; set; }
        [Column("P_Date")]
        [StringLength(10)]
        public string? PDate { get; set; }
        [Column("Fin_Year")]
        public long? FinYear { get; set; }
        [Column("Month_ID")]
        public int? MonthId { get; set; }
        public string? Description { get; set; }
        [Column("Yearly_or_Monthly")]
        public long? YearlyOrMonthly { get; set; }
        [Column("FacilityID")]
        public long? FacilityId { get; set; }
        public long Expr1 { get; set; }
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
        public long? FacilityId1 { get; set; }
        [Column("Branch_ID")]
        public long? BranchId { get; set; }
        [Column("Salary_Group_ID")]
        public long? SalaryGroupId { get; set; }
        [Column("CC_ID")]
        public long? CcId { get; set; }
        public long? Expr2 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public long CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
        [Column("Emp_ID")]
        public long? EmpId { get; set; }
        [StringLength(50)]
        public string Expr3 { get; set; } = null!;
        [Column("Emp_name")]
        [StringLength(250)]
        public string? EmpName { get; set; }
        public bool IsDeleted { get; set; }
        [Column("IsDeleted_D")]
        public bool IsDeletedD { get; set; }
        [Column("CC_ID2")]
        public long? CcId2 { get; set; }
        [Column("CC_ID3")]
        public long? CcId3 { get; set; }
        [Column("CC_ID4")]
        public long? CcId4 { get; set; }
        [Column("CC_ID5")]
        public long? CcId5 { get; set; }
    }

    public class ProvisionsAccDto
    {
        public long? AccountID { get; set; }
        public decimal? Credit { get; set; }
        public decimal? Debit { get; set; }
        public string Description { get; set; }
        public long? CCID { get; set; }
        public long? CCID2 { get; set; }
        public long? CCID3 { get; set; }
        public long? CCID4 { get; set; }
        public long? CCID5 { get; set; }
        public long? ReferenceNo { get; set; }
        public int? ReferenceTypeID { get; set; }
    }

}
