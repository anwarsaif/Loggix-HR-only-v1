using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrAttShiftEmployeeMVw
    {
        [StringLength(50)]
        public string? Name { get; set; }
        [Column("ID")]
        public long Id { get; set; }
        [Column("Emp_ID")]
        public long? EmpId { get; set; }
        [Column("Shit_ID")]
        public long? ShitId { get; set; }
        [Column("Begin_Date")]
        [StringLength(10)]
        public string? BeginDate { get; set; }
        [Column("End_Date")]
        [StringLength(10)]
        public string? EndDate { get; set; }
        public long CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        [Column("Emp_Code")]
        [StringLength(50)]
        public string EmpCode { get; set; } = null!;
        [Column("Emp_name")]
        [StringLength(250)]
        public string? EmpName { get; set; }
        [Column("DOAppointment")]
        [StringLength(12)]
        public string? Doappointment { get; set; }
        [Column("Bank_Name")]
        [StringLength(250)]
        public string? BankName { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Salary { get; set; }
        [Column("Location_Name")]
        [StringLength(200)]
        public string? LocationName { get; set; }
        [Column("Facility_ID")]
        public int? FacilityId { get; set; }
        public int? Location { get; set; }
        [Column("Status_ID")]
        public int? StatusId { get; set; }
        [Column("Off_days")]
        public int? OffDays { get; set; }
        [Column("BRANCH_ID")]
        public int? BranchId { get; set; }
        [Column("Emp_name2")]
        [StringLength(250)]
        public string? EmpName2 { get; set; }
        [Column("Location_Name2")]
        [StringLength(200)]
        public string? LocationName2 { get; set; }
        [Column("Nationality_Name2")]
        [StringLength(250)]
        public string? NationalityName2 { get; set; }
        [Column("Qualification_Name2")]
        [StringLength(250)]
        public string? QualificationName2 { get; set; }
        [Column("Facility_Name2")]
        [StringLength(500)]
        public string? FacilityName2 { get; set; }
        [Column("Manager_Name2")]
        [StringLength(250)]
        public string? ManagerName2 { get; set; }
        [Column("Dep_Name2")]
        [StringLength(200)]
        public string? DepName2 { get; set; }
        [Column("Facility_Name")]
        [StringLength(500)]
        public string? FacilityName { get; set; }
        [Column("Dep_Name")]
        [StringLength(200)]
        public string? DepName { get; set; }
        [Column("BRA_NAME")]
        public string? BraName { get; set; }
        [Column("BRA_NAME2")]
        public string? BraName2 { get; set; }
        [Column("Dept_ID")]
        public int? DeptId { get; set; }
    }

    public class HrAttShiftEmployeeMFilterDto
    {
        public int? DDNonAssignedEmployee { get; set; }

        public string? Name { get; set; }
        public long? Id { get; set; }
        public long? EmpId { get; set; }
        public string? BeginDate { get; set; }
        public string? EndDate { get; set; }

        public string? EmpCode { get; set; } 
        public string? EmpName { get; set; }

        public string? LocationName { get; set; }
        public int? FacilityId { get; set; }
        public int? Location { get; set; }
        public int? StatusId { get; set; }
        public int? BranchId { get; set; }

        public string? EmpName2 { get; set; }

        public string? LocationName2 { get; set; }

        public string? NationalityName2 { get; set; }
        public string? FacilityName2 { get; set; }

        public string? FacilityName { get; set; }
        public string? DepName { get; set; }
        public string? BraName { get; set; }
        public string? BraName2 { get; set; }
        public int? DeptId { get; set; }
        public long? ShitId { get; set; }
        public long? ShitNo { get; set; }

    }
}
