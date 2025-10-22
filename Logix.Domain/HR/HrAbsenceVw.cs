using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrAbsenceVw
    {
        [Column("Emp_name")]
        [StringLength(250)]
        public string? EmpName { get; set; }
        [Column("ID")]
        public long Id { get; set; }
        [Column("Emp_ID")]
        public long EmpId { get; set; }
        [Column("Absence_Type_Id")]
        public int AbsenceTypeId { get; set; }
        [Column("Absence_Date")]
        [StringLength(10)]
        [Unicode(false)]
        public string AbsenceDate { get; set; } = null!;
        public long CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public bool Expr1 { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string Expr2 { get; set; } = null!;
        [Column("Emp_Code")]
        [StringLength(50)]
        public string EmpCode { get; set; } = null!;
        [StringLength(10)]
        public string? Type { get; set; }
        public string? Note { get; set; }
        [Column("Dept_ID")]
        public int? DeptId { get; set; }
        public int? Location { get; set; }
        [Column("BRANCH_ID")]
        public int? BranchId { get; set; }
        [Column("Emp_name2")]
        [StringLength(250)]
        public string? EmpName2 { get; set; }
        [Column("Nationality_Name2")]
        [StringLength(250)]
        public string? NationalityName2 { get; set; }
        [Column("Facility_Name2")]
        [StringLength(500)]
        public string? FacilityName2 { get; set; }
        [Column("Dep_Name2")]
        [StringLength(200)]
        public string? DepName2 { get; set; }
        [Column("Location_Name2")]
        [StringLength(200)]
        public string? LocationName2 { get; set; }
        [Column("Manager3_Name2")]
        [StringLength(250)]
        public string? Manager3Name2 { get; set; }
        [Column("Manager2_Name2")]
        [StringLength(250)]
        public string? Manager2Name2 { get; set; }
        [Column("Manager_Name2")]
        [StringLength(250)]
        public string? ManagerName2 { get; set; }
        [Column("Qualification_Name2")]
        [StringLength(250)]
        public string? QualificationName2 { get; set; }
        [Column("Location_ID")]
        public long? LocationId { get; set; }
        [Column("TimeTable_ID")]
        public int? TimeTableId { get; set; }
        [Column("Status_ID")]
        public int? StatusId { get; set; }
        [Column("Dep_Name")]
        [StringLength(200)]
        public string? DepName { get; set; }
        [Column("Location_Name")]
        [StringLength(200)]
        public string? LocationName { get; set; }
    }
}
