using Logix.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Domain.HR
{
    [Table("HR_Contractes")]
    public class HrContracte : TraceEntity
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }

        [Column("Emp_ID")]
        public long? EmpId { get; set; }

        [Column("BRANCH_ID")]
        public int? BranchId { get; set; }

        [Column("Department_ID")]
        public int? DepartmentId { get; set; }

        [Column("Location_ID")]
        public int? LocationId { get; set; }

        [Column("T_Date")]
        [StringLength(10)]
        public string? TDate { get; set; }

        [Column("Facility_ID")]
        public int? FacilityId { get; set; }

        [Column("Start_Contract_Date")]
        [StringLength(10)]
        public string? StartContractDate { get; set; }

        [Column("Contract_expiry_Date")]
        [StringLength(10)]
        public string? ContractExpiryDate { get; set; }

        [Column("New_Start_Contract_Date")]
        [StringLength(10)]
        public string? NewStartContractDate { get; set; }

        [Column("New_Contract_expiry_Date")]
        [StringLength(10)]
        public string? NewContractExpiryDate { get; set; }

        [Column("Contract_Duration_Type")]
        public int? ContractDurationType { get; set; }

        [Column("Contract_Duration_No")]
        public int? ContractDurationNo { get; set; }

        [Column("Type_ID")]
        public int? TypeId { get; set; }

        public string? Note { get; set; }

        [Column("With_Salary_Inc")]
        public bool? WithSalaryInc { get; set; }

        [Column("Old_Salary", TypeName = "decimal(18, 2)")]
        public decimal? OldSalary { get; set; }

        [Column("New_Salary", TypeName = "decimal(18, 2)")]
        public decimal? NewSalary { get; set; }

        [Column("Inc_Rate", TypeName = "decimal(18, 2)")]
        public decimal? IncRate { get; set; }

        [Column("Inc_Amount", TypeName = "decimal(18, 2)")]
        public decimal? IncAmount { get; set; }

        [Column("Start_Date")]
        [StringLength(10)]
        public string? StartDate { get; set; }

        [Column("Apply_Type")]
        public int? ApplyType { get; set; }

        [Column("Decision_No")]
        [StringLength(250)]
        public string? DecisionNo { get; set; }

        [Column("Decision_Date")]
        [StringLength(10)]
        public string? DecisionDate { get; set; }
    }
}
