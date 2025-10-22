using System.ComponentModel.DataAnnotations;

namespace Logix.Application.DTOs.HR
{
    public class HrJobDto
    {
        public long Id { get; set; }
        [Required]
        public string? JobNo { get; set; }
        [Required]
        public string? JobName { get; set; }
        [Required]
        public long? LevelId { get; set; }
        public int? StatusId { get; set; }
        [Required]
        public long? DeptId { get; set; }
        [Required]
        public long? LocationId { get; set; }
        public string? CreateDate { get; set; }
        [Required]
        public string? DecNo { get; set; }
        public string? DecDate { get; set; }
        public long? DecSourceId { get; set; }
        //[Required]
        public int? JobCatagoriesId { get; set; }
        public long? FacilityId { get; set; }
        //[Required]
        public long? BranchId { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsDeleted { get; set; }
        public string? Note { get; set; }
        public string? MofCode { get; set; }
        public long? SectorId { get; set; }
    }

    public class HrJobEditDto
    {
        public long Id { get; set; }
        public string? JobNo { get; set; }
        public string? JobName { get; set; }
        public long? LevelId { get; set; }
        public int? StatusId { get; set; }
        public long? DeptId { get; set; }
        public long? LocationId { get; set; }
        public string? CreateDate { get; set; }
        public string? DecNo { get; set; }
        public string? DecDate { get; set; }
        public long? DecSourceId { get; set; }
        public int? JobCatagoriesId { get; set; }
        public long? FacilityId { get; set; }
        public long? BranchId { get; set; }
        public string? MofCode { get; set; }
        public long? SectorId { get; set; }
        public string? Note { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

    }


    public class HrJobFilterDto
    {
        public long Id { get; set; }
        //كود الوظيفة
        public string? JobNo { get; set; }
        public string? JobName { get; set; }
        public long? LevelId { get; set; }
        public int? StatusId { get; set; }
        public long? DeptId { get; set; }
        public long? LocationId { get; set; }
        public string? CreateDate { get; set; }
        public string? DecNo { get; set; }
        public string? DecDate { get; set; }
        public long? DecSourceId { get; set; }
        public int? JobCatagoriesId { get; set; }
        public long? FacilityId { get; set; }
        public long? BranchId { get; set; }
        public string? StatusName { get; set; }
        public string? StatusName2 { get; set; }
        public string? BraName2 { get; set; }
        public string? BraName { get; set; }
        public string? LevelName { get; set; }
        public string? DepName { get; set; }
        public string? DepName2 { get; set; }
        public string? LocationName { get; set; }
        public string? LocationName2 { get; set; }
        public string? EmpId { get; set; }
        public string? EmpName { get; set; }
        public string? EmpName2 { get; set; }
        public string? Note { get; set; }
        public string? MofCode { get; set; }
        public long? SectorId { get; set; }
        public string? SectorName { get; set; }
        public string? SectorName2 { get; set; }

    }
}
