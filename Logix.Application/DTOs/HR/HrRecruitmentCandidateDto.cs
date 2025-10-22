using Logix.Domain.HR;
using System.ComponentModel.DataAnnotations;

namespace Logix.Application.DTOs.HR
{
    public class HrRecruitmentCandidateDto
    {
        public long Id { get; set; }
        public string? AppDate { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? NameEn { get; set; }
        [Required]
        public long? VacancyId { get; set; }
        [Required]
        public string? Mobile { get; set; }
        public string? Phone { get; set; }
        [Required]
        public string? Email { get; set; }
        public string? Website { get; set; }
        public string? Facebook { get; set; }
        public string? Twitter { get; set; }
        [Required]
        public string? CvUrl { get; set; }
        public int? BranchId { get; set; }
        public int? FacilityId { get; set; }
        [Required]
        public int? MaritalStatus { get; set; }
        [Required]
        public int? Gender { get; set; }
        [Required]
        public string? BirthDate { get; set; }
        [Required]
        public int? QualificationId { get; set; }
        [Required]
        public int? SpecializationId { get; set; }

        [Required]
        public int? NationalityId { get; set; }
        public string? Note { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool? IsDeleted { get; set; }
        public string? IdNo { get; set; }
        public string? IdExpiryDate { get; set; }
        public string? IdOccupation { get; set; }
        [Required]
        public int? Country { get; set; }

        [Required]
        public string? City { get; set; }
        public string? Location { get; set; }
        public string? LastJobPosition { get; set; }
        [Required]
        public int? YearOfExp { get; set; }
        public bool? HasLicense { get; set; }
        public bool? HasCar { get; set; }
        public decimal? ExpectedSalary { get; set; }
        public string? Family { get; set; }
        [Required]
        public string? BirthPlace { get; set; }
        [Required]
        public string? University { get; set; }
        [Required]
        public string? YearGraduation { get; set; }
        [Required]
        public string? Height { get; set; }
        [Required]
        public string? Weight { get; set; }

        public string? RangeExperience { get; set; }
    }
    public class HrRecruitmentCandidateEditDto
    {
        public long Id { get; set; }
        public string? AppDate { get; set; }
        public string? Name { get; set; }
        public string? NameEn { get; set; }
        public long? VacancyId { get; set; }
        public string? Mobile { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Website { get; set; }
        public string? Facebook { get; set; }
        public string? Twitter { get; set; }
        public string? CvUrl { get; set; }
        public int? BranchId { get; set; }
        public int? FacilityId { get; set; }
        public int? MaritalStatus { get; set; }
        public int? Gender { get; set; }
        public string? BirthDate { get; set; }
        public int? QualificationId { get; set; }
        public int? SpecializationId { get; set; }
        public int? NationalityId { get; set; }
        public string? Note { get; set; }

        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string? IdNo { get; set; }
        public string? IdExpiryDate { get; set; }
        public string? IdOccupation { get; set; }
        public int? Country { get; set; }
        public string? City { get; set; }
        public string? Location { get; set; }
        public string? LastJobPosition { get; set; }
        public int? YearOfExp { get; set; }
        public bool? HasLicense { get; set; }
        public bool? HasCar { get; set; }
        public decimal? ExpectedSalary { get; set; }
        public string? Family { get; set; }
        public string? BirthPlace { get; set; }
        public string? University { get; set; }
        public string? YearGraduation { get; set; }
        public string? Height { get; set; }
        public string? Weight { get; set; }
        public string? RangeExperience { get; set; }
    }


    public class HrRecruitmentCandidateFilterDto
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public long? VacancyId { get; set; }
        public string? VacancyName { get; set; }
        public string? QualificationName { get; set; }
        public int? QualificationId { get; set; }
        public string? University { get; set; }
        public string? YearGraduation { get; set; }
        public string? NationalityName { get; set; }
        public int? NationalityId { get; set; }
        public string? Email { get; set; }
        public int? YearOfExp { get; set; }
        public string? RangeExperience { get; set; }
        public string? CreatedOn { get; set; }
        public string? Mobile { get; set; }
        public int? MaritalStatus { get; set; }
        public string? BirthDate { get; set; }
        public int? Gender { get; set; }
        public int? SpecializationId { get; set; }
        public string? SpecializationName { get; set; }

        public string? AppDate { get; set; }

    }
    public class HrRecruitmentCandidateGetByIdDto
    {

        public List<HrRecruitmentCandidateKpiDVw>? allDetails { get; set; }
        public HrRecruitmentCandidateVw? candidateData { get; set; }
        public string? EvaDate { get; set; }

        public string? VacancyName { get; set; }
        public string? CandidateName { get; set; }
        public string? TemName { get; set; }


    }
}
