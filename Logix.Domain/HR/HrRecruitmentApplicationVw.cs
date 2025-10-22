using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrRecruitmentApplicationVw
    {
        [Column("Vacancy_Name")]
        [StringLength(250)]
        public string? VacancyName { get; set; }
        [Column("ID")]
        public int Id { get; set; }
        [Column("Vacancy_ID")]
        public int? VacancyId { get; set; }
        [Column("Applicant_ID")]
        public int? ApplicantId { get; set; }
        [Column("ID_No")]
        [StringLength(250)]
        public string? IdNo { get; set; }
        [Column("ID_Expiry_Date")]
        [StringLength(10)]
        public string? IdExpiryDate { get; set; }
        [Column("ID_Occupation")]
        [StringLength(250)]
        public string? IdOccupation { get; set; }
        [StringLength(250)]
        public string? Name { get; set; }
        [Column("Name_En")]
        [StringLength(250)]
        public string? NameEn { get; set; }
        [StringLength(250)]
        public string? Mobile { get; set; }
        [StringLength(250)]
        public string? Phone { get; set; }
        [StringLength(250)]
        public string? Email { get; set; }
        public int? Country { get; set; }
        [StringLength(100)]
        public string? City { get; set; }
        [StringLength(100)]
        public string? Location { get; set; }
        [Column("CV_URL")]
        [StringLength(250)]
        public string? CvUrl { get; set; }
        [Column("Marital_Status")]
        public int? MaritalStatus { get; set; }
        public int? Gender { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string? BirthDate { get; set; }
        [Column("Qualification_ID")]
        public int? QualificationId { get; set; }
        [Column("Specialization_ID")]
        public int? SpecializationId { get; set; }
        [Column("Nationality_ID")]
        public int? NationalityId { get; set; }
        [Column("Last_Job_Position")]
        [StringLength(100)]
        public string? LastJobPosition { get; set; }
        [Column("Year_Of_Exp")]
        public int? YearOfExp { get; set; }
        [Column("Has_License")]
        public bool? HasLicense { get; set; }
        [Column("Has_Car")]
        public bool? HasCar { get; set; }
        [Column("Expected_Salary", TypeName = "decimal(18, 2)")]
        public decimal? ExpectedSalary { get; set; }
        public long? CreatedBy { get; set; }
        [Column(TypeName = "date")]
        public DateTime? CreatedOn { get; set; }
        [Column(TypeName = "date")]
        public DateTime? ModifiedOn { get; set; }
        public long? ModifiedBy { get; set; }
        [Column("Qualification_Name")]
        [StringLength(250)]
        public string? QualificationName { get; set; }
        [Column("Qualification_Name2")]
        [StringLength(250)]
        public string? QualificationName2 { get; set; }
        [Column("Country_Name")]
        [StringLength(250)]
        public string? CountryName { get; set; }
        [Column("Country_Name2")]
        [StringLength(250)]
        public string? CountryName2 { get; set; }
        [Column("Specialization_Name")]
        [StringLength(250)]
        public string? SpecializationName { get; set; }
        [Column("Specialization_Name2")]
        [StringLength(250)]
        public string? SpecializationName2 { get; set; }
        [Column("Nationality_Name")]
        [StringLength(250)]
        public string? NationalityName { get; set; }
        [Column("Nationality_Name2")]
        [StringLength(250)]
        public string? NationalityName2 { get; set; }
        public bool? IsDeleted { get; set; }
        [Column("Status_ID")]
        public int? StatusId { get; set; }
        [Column("Status_Name")]
        [StringLength(250)]
        public string? StatusName { get; set; }
        [Column("Catagories_ID")]
        public int? CatagoriesId { get; set; }
        [Column("Vacancy_Apply_Date")]
        [StringLength(10)]
        public string? VacancyApplyDate { get; set; }
    }
}
