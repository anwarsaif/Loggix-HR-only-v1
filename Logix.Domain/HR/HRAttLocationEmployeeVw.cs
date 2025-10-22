using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]

    public class HrAttLocationEmployeeVw
    {

        [Column("ID")]
        public long Id { get; set; }
        [Column("Location_ID")]
        public long? LocationId { get; set; }
        [Column("Emp_ID")]
        public long? EmpId { get; set; }
        [Column("Begin_Date")]
        [StringLength(10)]
        public string? BeginDate { get; set; }
        [Column("End_Date")]
        [StringLength(10)]
        public string? EndDate { get; set; }
        public bool IsDeleted { get; set; }
        [Column("Location_name")]
        [StringLength(250)]
        public string? LocationName { get; set; }
        [Column("latitude")]
        [StringLength(2400)]
        public string? Latitude { get; set; }
        [Column("longitude")]
        [StringLength(2400)]
        public string? Longitude { get; set; }
        [Column("Emp_Code")]
        [StringLength(50)]
        public string EmpCode { get; set; } = null!;
        [Column("Emp_name")]
        [StringLength(250)]
        public string? EmpName { get; set; }
        [Column("BRANCH_ID")]
        public int? BranchId { get; set; }
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
        [Column("Nationality_Name2")]
        [StringLength(250)]
        public string? NationalityName2 { get; set; }
        [Column("Cat_name2")]
        [StringLength(250)]
        public string? CatName2 { get; set; }
        [Column("Dep_Name2")]
        [StringLength(200)]
        public string? DepName2 { get; set; }
        [Column("Qualification_Name2")]
        [StringLength(250)]
        public string? QualificationName2 { get; set; }
        [Column("Facility_Name2")]
        [StringLength(500)]
        public string? FacilityName2 { get; set; }
        [Column("Status_Name2")]
        [StringLength(250)]
        public string? StatusName2 { get; set; }
        //[Column("Status_ID")]
        //public int? StatusId { get; set; }
    }
}
