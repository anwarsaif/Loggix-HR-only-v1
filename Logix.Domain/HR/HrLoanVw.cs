using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Logix.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrLoanVw:TraceEntity
    {
        [Column("Emp_name")]
        [StringLength(250)]
        public string? EmpName { get; set; }
        [Column("ID")]
        public long Id { get; set; }
        [Column("Loan_Date")]
        [StringLength(10)]
        [Unicode(false)]
        public string? LoanDate { get; set; }
        [Column("Loan_Value", TypeName = "decimal(18, 2)")]
        public decimal? LoanValue { get; set; }
        [Column("Installment_Value", TypeName = "decimal(18, 2)")]
        public decimal? InstallmentValue { get; set; }
        [Column("Installment_Count")]
        public int? InstallmentCount { get; set; }
        [Column("Installment_Count_paid")]
        public int? InstallmentCountPaid { get; set; }
        [Column("Installment_Count_Remaining")]
        public int? InstallmentCountRemaining { get; set; }
        [Column("Installment_Last_Value", TypeName = "decimal(18, 2)")]
        public decimal? InstallmentLastValue { get; set; }
        [Column("Start_Date_Payment")]
        [StringLength(10)]
        [Unicode(false)]
        public string? StartDatePayment { get; set; }
        [Column("End_Date_Payment")]
        [StringLength(50)]
        [Unicode(false)]
        public string? EndDatePayment { get; set; }
        public bool? IsActive { get; set; }
        [Column("Emp_ID")]
        [StringLength(50)]
        public string? EmpId { get; set; }
        [Column("Emp_Code")]
        [StringLength(50)]
        public string EmpCode { get; set; } = null!;
        public string? Note { get; set; }
        public int? Type { get; set; }
        [Column("Manager_ID")]
        public long? ManagerId { get; set; }
        [Column("BRANCH_ID")]
        public int? BranchId { get; set; }
        public int? Location { get; set; }
        [Column("Dept_ID")]
        public int? DeptId { get; set; }
        [Column("Status_ID")]
        public int? StatusId { get; set; }
        [Column("Emp_name2")]
        [StringLength(250)]
        public string? EmpName2 { get; set; }
        [Column("Manager_Name2")]
        [StringLength(250)]
        public string? ManagerName2 { get; set; }
        [Column("Manager2_Name2")]
        [StringLength(250)]
        public string? Manager2Name2 { get; set; }
        [Column("Manager3_Name2")]
        [StringLength(250)]
        public string? Manager3Name2 { get; set; }
        [Column("Nationality_Name2")]
        [StringLength(250)]
        public string? NationalityName2 { get; set; }
        [Column("Cat_name2")]
        [StringLength(250)]
        public string? CatName2 { get; set; }
        [Column("Dep_Name2")]
        [StringLength(200)]
        public string? DepName2 { get; set; }
        [Column("Location_Name2")]
        [StringLength(200)]
        public string? LocationName2 { get; set; }
        [Column("Qualification_Name2")]
        [StringLength(250)]
        public string? QualificationName2 { get; set; }
        [Column("Facility_Name2")]
        [StringLength(500)]
        public string? FacilityName2 { get; set; }
        [Column("Status_Name2")]
        [StringLength(250)]
        public string? StatusName2 { get; set; }
        [Column("BRA_NAME2")]
        public string? BraName2 { get; set; }
        [Column("Dep_Name")]
        [StringLength(200)]
        public string? DepName { get; set; }
        [Column("Location_Name")]
        [StringLength(200)]
        public string? LocationName { get; set; }
        [Column("BRA_NAME")]
        public string? BraName { get; set; }
    }
}
