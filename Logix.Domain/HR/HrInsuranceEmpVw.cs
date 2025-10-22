using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrInsuranceEmpVw
    {
        [Column("ID")]
        public long Id { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Amount { get; set; }
        public string? Note { get; set; }
        [Column("Refrance_Ins_Emp_ID")]
        public long? RefranceInsEmpId { get; set; }
        [Column("Emp_Code")]
        [StringLength(50)]
        public string EmpCode { get; set; } = null!;
        [Column("Emp_name")]
        [StringLength(250)]
        public string? EmpName { get; set; }
        [Column("Dependent_Name")]
        [StringLength(50)]
        public string? DependentName { get; set; }
        [Column("Insurance_ID")]
        public long? InsuranceId { get; set; }
        public bool IsDeleted { get; set; }
        [Column("To_Dependents")]
        public bool? ToDependents { get; set; }
        [Column("Class_Name")]
        [StringLength(250)]
        public string? ClassName { get; set; }
        [Column("Catagories_ID")]
        public int? CatagoriesId { get; set; }
        [Column("Dependent_ID")]
        public long? DependentId { get; set; }
        [Column("Class_ID")]
        public int? ClassId { get; set; }
        [Column("Trans_Type_ID")]
        public int? TransTypeId { get; set; }
        [Column("CostCenter_Name")]
        [StringLength(150)]
        public string? CostCenterName { get; set; }
        [Column("CostCenter_Code")]
        [StringLength(50)]
        public string? CostCenterCode { get; set; }
        [Column("Status_name")]
        [StringLength(250)]
        public string? StatusName { get; set; }
        [Column("Status_ID")]
        public int? StatusId { get; set; }
        [Column("CC_ID")]
        public long? CcId { get; set; }
        [Column("Policy_ID")]
        public long? PolicyId { get; set; }
        [Column("Insurance_Type")]
        public int? InsuranceType { get; set; }
        [Column("BRANCH_ID")]
        public int? BranchId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        [Column("Emp_ID")]
        public long? EmpId { get; set; }
        public int? Location { get; set; }
        [Column("Dept_ID")]
        public int? DeptId { get; set; }
        [Column("Dep_Name")]
        [StringLength(200)]
        public string? DepName { get; set; }
        [Column("Location_Name")]
        [StringLength(200)]
        public string? LocationName { get; set; }
        [Column("Insurance_Date")]
        [StringLength(10)]
        public string? InsuranceDate { get; set; }
        [Column("Insurance_Card_No")]
        [StringLength(50)]
        public string? InsuranceCardNo { get; set; }
        [Column("Policy_Code")]
        [StringLength(250)]
        public string? PolicyCode { get; set; }
        [Column("Job_Catagories_ID")]
        public int? JobCatagoriesID { get; set; }
        [Column("Nationality_ID")]
        public int? NationalityID { get; set; }
        [Column("Facility_ID")]
        public int? FacilityID { get; set; }
        [Column("Emp_name2")]
        public string? Empname2 { get; set; }
        [Column("Salary_Group_ID")]
        public long? SalaryGroupID { get; set; }
        [Column("DOAppointment")]
        public string? DOAppointment { get; set; }
        [Column("Location_Name2")]
        public string? LocationName2 { get; set; }
    }
}

