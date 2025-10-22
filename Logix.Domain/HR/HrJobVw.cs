using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrJobVw
    {
        [Column("ID")]
        public long Id { get; set; }
        [Column("Job_No")]
        [StringLength(50)]
        public string? JobNo { get; set; }
        [Column("Job_Name")]
        [StringLength(255)]
        public string? JobName { get; set; }
        [Column("Level_ID")]
        public long? LevelId { get; set; }
        [Column("Status_ID")]
        public int? StatusId { get; set; }
        [Column("Dept_ID")]
        public long? DeptId { get; set; }
        [Column("Location_ID")]
        public long? LocationId { get; set; }
        [Column("Create_Date")]
        [StringLength(10)]
        public string? CreateDate { get; set; }
        [Column("Dec_No")]
        [StringLength(50)]
        public string? DecNo { get; set; }
        [Column("Dec_Date")]
        [StringLength(50)]
        public string? DecDate { get; set; }
        [Column("Dec_Source_ID")]
        public long? DecSourceId { get; set; }
        [Column("Job_Catagories_ID")]
        public int? JobCatagoriesId { get; set; }
        [Column("Facility_ID")]
        public long? FacilityId { get; set; }
        [Column("Branch_ID")]
        public long? BranchId { get; set; }
        public bool IsDeleted { get; set; }
        [Column("Status_Name")]
        [StringLength(250)]
        public string? StatusName { get; set; }
        [Column("BRA_NAME")]
        public string? BraName { get; set; }
        [Column("Level_Name")]
        [StringLength(250)]
        public string? LevelName { get; set; }
        [Column("Dep_Name")]
        [StringLength(200)]
        public string? DepName { get; set; }
        [Column("Location_Name")]
        [StringLength(200)]
        public string? LocationName { get; set; }
        [Column("Status_Name2")]
        [StringLength(250)]
        public string? StatusName2 { get; set; }
        [Column("Dep_Name2")]
        [StringLength(200)]
        public string? DepName2 { get; set; }
        [Column("Location_Name2")]
        [StringLength(200)]
        public string? LocationName2 { get; set; }
        [Column("BRA_NAME2")]
        public string? BraName2 { get; set; }
    }
}
