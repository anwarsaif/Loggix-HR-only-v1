using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrPermissionsVw
    {
        [Column("Type_Name")]
        [StringLength(250)]
        public string? TypeName { get; set; }
        [Column("Reason_Name")]
        [StringLength(250)]
        public string? ReasonName { get; set; }
        [Column("ID")]
        public long Id { get; set; }
        public int? Type { get; set; }
        [Column("Reason_Leave")]
        public int? ReasonLeave { get; set; }
        [Column("Permission_Date")]
        [StringLength(10)]
        public string? PermissionDate { get; set; }
        [Column("Details_Reason")]
        public string? DetailsReason { get; set; }
        [Column("Leaveing_Time")]
        [StringLength(50)]
        public string? LeaveingTime { get; set; }
        [Column("Estimated_Time_Return")]
        [StringLength(50)]
        public string? EstimatedTimeReturn { get; set; }
        [Column("Contact_Number")]
        [StringLength(50)]
        public string? ContactNumber { get; set; }
        public string? Note { get; set; }
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
        [Column("BRANCH_ID")]
        public int? BranchId { get; set; }
        [Column("Emp_ID")]
        public long? EmpId { get; set; }
        [Column("Type_Name2")]
        [StringLength(250)]
        public string? TypeName2 { get; set; }
        [Column("Reason_Name2")]
        [StringLength(250)]
        public string? ReasonName2 { get; set; }
        [Column("Emp_name2")]
        [StringLength(250)]
        public string? EmpName2 { get; set; }
        [Column("Facility_ID")]
        public int? FacilityId { get; set; }
        [Column("Facility_Name")]
        [StringLength(500)]
        public string? FacilityName { get; set; }
        [Column("Facility_Name2")]
        [StringLength(500)]
        public string? FacilityName2 { get; set; }
        [Column("BRA_NAME2")]
        public string? BraName2 { get; set; }
        [Column("BRA_NAME")]
        public string? BraName { get; set; }
        [Column("Dept_ID")]
        public int? DeptId { get; set; }
        [Column("Dep_Name2")]
        [StringLength(200)]
        public string? DepName2 { get; set; }
        [Column("Dep_Name")]
        [StringLength(200)]
        public string? DepName { get; set; }
        [Column("Location_Name")]
        [StringLength(200)]
        public string? LocationName { get; set; }
        [Column("Location_Name2")]
        [StringLength(200)]
        public string? LocationName2 { get; set; }
        public int? Location { get; set; }
        [Column("Time_Difference")]
        [StringLength(5)]
        [Unicode(false)]
        public string? TimeDifference { get; set; }
    }
}
