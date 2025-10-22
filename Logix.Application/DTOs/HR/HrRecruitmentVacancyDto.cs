using System.ComponentModel.DataAnnotations;

namespace Logix.Application.DTOs.HR
{

    public class HrRecruitmentVacancyFilterDto
    {
        public long? JobId { get; set; }
        public string? VacancyName { get; set; }

        public int? StatusId { get; set; }


    }
    public class HrRecruitmentVacancyDto
    {
        public long? Id { get; set; }
        [Required]
        public string? VacancyName { get; set; }
        [Required]

        public long? JobId { get; set; }
        public int? BranchId { get; set; }
        public int? DeptId { get; set; }
        public int? LocationId { get; set; }
        public int? FacilityId { get; set; }
        [Required]
        public int? NumberPosition { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool? IsDeleted { get; set; }
        public string? JobDescription { get; set; }
        public int? StatusId { get; set; }
        public long? CreatedBy { get; set; }
        public string? ShortDescription { get; set; }
        public int? JobType { get; set; }
        public bool? IsOnline { get; set; }
        public decimal? SalaryFrom { get; set; }
        public decimal? SalaryTo { get; set; }
        public string? Experience { get; set; }
        public int? QualificationId { get; set; }
        public int? SpecificationId { get; set; }
        public int? Nationality { get; set; }
        public string? Gender { get; set; }
        public int? AgeFrom { get; set; }
        public int? AgeTo { get; set; }
        public string? LastApplyDate { get; set; }
        public long? Country { get; set; }
        public long? City { get; set; }
    }

    public class HrRecruitmentVacancyEditDto
    {
        public long Id { get; set; }
        [Required]

        public string? VacancyName { get; set; }
        [Required]

        public long? JobId { get; set; }
        public int? BranchId { get; set; }
        public int? DeptId { get; set; }
        public int? LocationId { get; set; }
        public int? FacilityId { get; set; }
        [Required]

        public int? NumberPosition { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string? JobDescription { get; set; }
        public int? StatusId { get; set; }
        public string? ShortDescription { get; set; }
        public int? JobType { get; set; }
        public bool? IsOnline { get; set; }
        public decimal? SalaryFrom { get; set; }
        public decimal? SalaryTo { get; set; }
        public string? Experience { get; set; }
        public int? QualificationId { get; set; }
        public int? SpecificationId { get; set; }
        public int? Nationality { get; set; }
        public string? Gender { get; set; }
        public int? AgeFrom { get; set; }
        public int? AgeTo { get; set; }
        public string? LastApplyDate { get; set; }
        public long? Country { get; set; }
        public long? City { get; set; }
    }
}
