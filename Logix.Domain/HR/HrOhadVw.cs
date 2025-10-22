using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrOhadVw
    {
        public long OhdaId { get; set; }
        [Column("Emp_Id")]
        public long? EmpId { get; set; }
        [StringLength(50)]
        public string? OhdaDate { get; set; }
        public bool? IsDeleted { get; set; }
        public string? Note { get; set; }
        [Column("Trans_Type_ID")]
        public int? TransTypeId { get; set; }
        [Column("Emp_Code")]
        [StringLength(50)]
        public string? EmpCode { get; set; }
        [Column("Emp_name")]
        [StringLength(250)]
        public string? EmpName { get; set; }
        public long? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
        [Column("BRANCH_ID")]
        public int? BranchId { get; set; }
        public int? Location { get; set; }
        [Column("Dept_ID")]
        public int? DeptId { get; set; }
        [Column("Work_Date")]
        [StringLength(10)]
        public string? WorkDate { get; set; }
        [Column("Emp_name_Recipient")]
        [StringLength(250)]
        public string? EmpNameRecipient { get; set; }
        [Column("Emp_Code_Recipient")]
        [StringLength(50)]
        public string? EmpCodeRecipient { get; set; }
        [Column("Status_name")]
        [StringLength(250)]
        public string? StatusName { get; set; }
        [Column("Emp_Code_To")]
        [StringLength(50)]
        public string? EmpCodeTo { get; set; }
        [Column("Emp_Name_To")]
        [StringLength(250)]
        public string? EmpNameTo { get; set; }
        [Column("Trans_Type_Name")]
        [StringLength(250)]
        public string? TransTypeName { get; set; }
        public long? Code { get; set; }
        [Column("Emp_ID_Recipient")]
        public long? EmpIdRecipient { get; set; }
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
        [Column("Location_Name2")]
        [StringLength(200)]
        public string? LocationName2 { get; set; }
        [Column("Dep_Name2")]
        [StringLength(200)]
        public string? DepName2 { get; set; }
        [Column("Cat_name2")]
        [StringLength(250)]
        public string? CatName2 { get; set; }
        [Column("Nationality_Name2")]
        [StringLength(250)]
        public string? NationalityName2 { get; set; }
        [Column("Qualification_Name2")]
        [StringLength(250)]
        public string? QualificationName2 { get; set; }
        [Column("Status_Name2")]
        [StringLength(250)]
        public string? StatusName2 { get; set; }
        [Column("Facility_Name2")]
        [StringLength(500)]
        public string? FacilityName2 { get; set; }
    }
}
