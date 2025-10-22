using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrGosiEmployeeVw
    {
        [Column("ID")]
        public long Id { get; set; }
        [Column("Gosi_ID")]
        public long? GosiId { get; set; }
        [Column("Emp_ID")]
        public long? EmpId { get; set; }
        [Column("Basic_Salary", TypeName = "decimal(18, 2)")]
        public decimal? BasicSalary { get; set; }
        [Column("Housing_Allowance", TypeName = "decimal(18, 2)")]
        public decimal? HousingAllowance { get; set; }
        [Column("Other_Allowance", TypeName = "decimal(18, 2)")]
        public decimal? OtherAllowance { get; set; }
        [Column("Total_Salary", TypeName = "decimal(18, 2)")]
        public decimal? TotalSalary { get; set; }
        [Column("Gosi_Emp", TypeName = "decimal(18, 2)")]
        public decimal? GosiEmp { get; set; }
        [Column("Gosi_Company", TypeName = "decimal(18, 2)")]
        public decimal? GosiCompany { get; set; }
        [Column("Gosi_Rate", TypeName = "decimal(18, 2)")]
        public decimal? GosiRate { get; set; }
        [Column("Gosi_Emp_Rate", TypeName = "decimal(18, 2)")]
        public decimal? GosiEmpRate { get; set; }
        [Column("Gosi_Company_Rate", TypeName = "decimal(18, 2)")]
        public decimal? GosiCompanyRate { get; set; }
        public long? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        [Column("Gosi_No")]
        [StringLength(50)]
        public string? GosiNo { get; set; }
        [Column("Gosi_TypeName")]
        [StringLength(250)]
        public string? GosiTypeName { get; set; }
        [Column("Emp_Code")]
        [StringLength(50)]
        public string EmpCode { get; set; } = null!;
        [Column("Emp_name")]
        [StringLength(250)]
        public string? EmpName { get; set; }
        [Column("ID_No")]
        [StringLength(50)]
        public string? IdNo { get; set; }
        [Column("CC_ID")]
        public long? CcId { get; set; }
        [Column("CostCenter_Name")]
        [StringLength(150)]
        public string? CostCenterName { get; set; }
        [Column("CostCenter_Code")]
        [StringLength(50)]
        public string? CostCenterCode { get; set; }
        [Column("CostCenter_Name2")]
        [StringLength(150)]
        public string? CostCenterName2 { get; set; }
        [Column("Facility_ID")]
        public long? FacilityId { get; set; }
        [Column("BRANCH_ID")]
        public int? BranchId { get; set; }
        [Column("Status_ID")]
        public int? StatusId { get; set; }
        [Column("Status_name")]
        [StringLength(250)]
        public string? StatusName { get; set; }
        [Column("T_Date")]
        [StringLength(50)]
        public string? TDate { get; set; }
        [Column("Dept_ID")]
        public int? DeptId { get; set; }
        public int? Location { get; set; }
        [Column("T_Month")]
        [StringLength(2)]
        public string? TMonth { get; set; }
        public int? FinancelYear { get; set; }
        public long? Expr1 { get; set; }
        [Column("Emp_name2")]
        [StringLength(250)]
        public string? EmpName2 { get; set; }
        [Column("Dep_Name")]
        [StringLength(200)]
        public string? DepName { get; set; }
        [Column("Dep_Name2")]
        [StringLength(200)]
        public string? DepName2 { get; set; }
        [Column("Location_Name2")]
        [StringLength(200)]
        public string? LocationName2 { get; set; }
        [Column("Location_Name")]
        [StringLength(200)]
        public string? LocationName { get; set; }
    }
}
