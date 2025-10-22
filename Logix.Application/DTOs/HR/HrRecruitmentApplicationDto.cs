using System.ComponentModel.DataAnnotations;

namespace Logix.Application.DTOs.HR
{
    public class HrRecruitmentApplicationDto
    {

        public int Id { get; set; }
        public int? VacancyId { get; set; }
        public int? ApplicantId { get; set; }
        public string? VacancyApplyDate { get; set; }
        public string? IdNo { get; set; }
        public string? IdExpiryDate { get; set; }
        public string? IdOccupation { get; set; }
        public string? Name { get; set; }
        public string? NameEn { get; set; }
        public string? Mobile { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public int? Country { get; set; }
        public string? City { get; set; }
        public string? Location { get; set; }
        public string? CvUrl { get; set; }
        public int? MaritalStatus { get; set; }
        public int? Gender { get; set; }
        public string? BirthDate { get; set; }
        public int? QualificationId { get; set; }
        public int? SpecializationId { get; set; }
        public int? NationalityId { get; set; }
        public string? LastJobPosition { get; set; }
        public int? YearOfExp { get; set; }
        public bool? HasLicense { get; set; }
        public bool? HasCar { get; set; }
        public decimal? ExpectedSalary { get; set; }
        public DateTime? CreatedOn { get; set; }
        public long? CreatedBy { get; set; }
        public bool? IsDeleted { get; set; }
        public int? StatusId { get; set; }
    }
    public class HrRecruitmentApplicationEditDto
    {

        public int Id { get; set; }
        public int? VacancyId { get; set; }
        public int? ApplicantId { get; set; }
        public string? VacancyApplyDate { get; set; }
        public string? IdNo { get; set; }
        public string? IdExpiryDate { get; set; }
        public string? IdOccupation { get; set; }
        public string? Name { get; set; }
        public string? NameEn { get; set; }
        public string? Mobile { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public int? Country { get; set; }
        public string? City { get; set; }
        public string? Location { get; set; }
        public string? CvUrl { get; set; }
        public int? MaritalStatus { get; set; }
        public int? Gender { get; set; }
        public string? BirthDate { get; set; }
        public int? QualificationId { get; set; }
        public int? SpecializationId { get; set; }
        public int? NationalityId { get; set; }
        public string? LastJobPosition { get; set; }
        public int? YearOfExp { get; set; }
        public bool? HasLicense { get; set; }
        public bool? HasCar { get; set; }
        public decimal? ExpectedSalary { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public int? StatusId { get; set; }
    }

    public class HrRecruitmentApplicationPopUpDto
    {
        public int? ApplicantId { get; set; }
        public int? Id { get; set; }
        public int? VacancyId { get; set; }
        public string? Name { get; set; }
        public string? VacancyName { get; set; }
    }
}
