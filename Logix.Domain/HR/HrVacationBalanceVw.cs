using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Logix.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrVacationBalanceVw : TraceEntity
    {
        [Column("Vac_balanceId")]
        public long VacBalanceId { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? VacationBalance { get; set; }
        [Column("V_balance_Rate", TypeName = "decimal(18, 2)")]
        public decimal? VBalanceRate { get; set; }
        [Column("Emp_Id")]
        public long? EmpId { get; set; }
        [Column("Emp_name")]
        [StringLength(250)]
        public string? EmpName { get; set; }
        [Column("BRA_NAME")]
        public string? BraName { get; set; }
        [Column("BRANCH_ID")]
        public int? BranchId { get; set; }
        public bool? Isacitve { get; set; }
        [StringLength(10)]
        public string? StartDate { get; set; }
        [Column("Emp_Code")]
        [StringLength(50)]
        public string? EmpCode { get; set; }
        [Column("Vacation_Type_Id")]
        public int? VacationTypeId { get; set; }
        [Column("Vacation_Type_Name")]
        [StringLength(500)]
        public string? VacationTypeName { get; set; }
        public string? Note { get; set; }
        [Column("Vacation_Type_Name2")]
        [StringLength(500)]
        public string? VacationTypeName2 { get; set; }
        [Column("Emp_name2")]
        [StringLength(250)]
        public string? EmpName2 { get; set; }
        [Column("Location_Name2")]
        [StringLength(200)]
        public string? LocationName2 { get; set; }
        [Column("Dep_Name2")]
        [StringLength(200)]
        public string? DepName2 { get; set; }
        [Column("Qualification_Name2")]
        [StringLength(250)]
        public string? QualificationName2 { get; set; }
        [Column("Facility_Name2")]
        [StringLength(500)]
        public string? FacilityName2 { get; set; }
        [Column("Dept_ID")]
        public int? DeptId { get; set; }
        public int? Location { get; set; }
        [Column("Dep_Name")]
        [StringLength(200)]
        public string? DepName { get; set; }
        [Column("Location_Name")]
        [StringLength(200)]
        public string? LocationName { get; set; }
        [Column("BRA_NAME2")]
        public string? BraName2 { get; set; }
    }
}
