using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{

	[Table("HR_Recruitment_Candidate")]
	public partial class HrRecruitmentCandidate
	{
		[Key]
		[Column("ID")]
		public long Id { get; set; }

		[Column("App_Date")]
		[StringLength(10)]
		public string? AppDate { get; set; }

		[StringLength(250)]
		public string? Name { get; set; }

		[Column("Name_En")]
		[StringLength(250)]
		public string? NameEn { get; set; }

		[Column("Vacancy_ID")]
		public long? VacancyId { get; set; }

		[StringLength(250)]
		public string? Mobile { get; set; }

		[StringLength(250)]
		public string? Phone { get; set; }

		[StringLength(250)]
		public string? Email { get; set; }

		[StringLength(250)]
		public string? Website { get; set; }

		[StringLength(250)]
		public string? Facebook { get; set; }

		[StringLength(250)]
		public string? Twitter { get; set; }

		[Column("CV_URL")]
		[StringLength(250)]
		public string? CvUrl { get; set; }

		[Column("BRANCH_ID")]
		public int? BranchId { get; set; }

		[Column("Facility_ID")]
		public int? FacilityId { get; set; }

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

		public string? Note { get; set; }

		public long? CreatedBy { get; set; }

		[Column(TypeName = "datetime")]
		public DateTime? CreatedOn { get; set; }

		public long? ModifiedBy { get; set; }

		[Column(TypeName = "datetime")]
		public DateTime? ModifiedOn { get; set; }

		public bool? IsDeleted { get; set; }

		[Column("ID_No")]
		[StringLength(250)]
		public string? IdNo { get; set; }

		[Column("ID_Expiry_Date")]
		[StringLength(10)]
		public string? IdExpiryDate { get; set; }

		[Column("ID_Occupation")]
		[StringLength(250)]
		public string? IdOccupation { get; set; }

		public int? Country { get; set; }

		[StringLength(100)]
		public string? City { get; set; }

		[StringLength(100)]
		public string? Location { get; set; }

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

		public string? Family { get; set; }

		[Column("Birth_Place")]
		[StringLength(500)]
		public string? BirthPlace { get; set; }

		[StringLength(500)]
		public string? University { get; set; }

		[Column("Year_Graduation")]
		[StringLength(500)]
		public string? YearGraduation { get; set; }

		[Column("height")]
		[StringLength(500)]
		public string? Height { get; set; }

		[Column("weight")]
		[StringLength(500)]
		public string? Weight { get; set; }

		[Column("Range_Experience")]
		public string? RangeExperience { get; set; }

		[Column("First_Name")]
		[StringLength(15)]
		public string? FirstName { get; set; }

		[Column("Father_Name")]
		[StringLength(15)]
		public string? FatherName { get; set; }

		[Column("Grand_Father_Name")]
		[StringLength(15)]
		public string? GrandFatherName { get; set; }

		[Column("Last_Name")]
		[StringLength(15)]
		public string? LastName { get; set; }

		[Column("Relative_Mobile")]
		[StringLength(250)]
		public string? RelativeMobile { get; set; }

		[Column("Relative_Name")]
		[StringLength(250)]
		public string? RelativeName { get; set; }

		[StringLength(15)]
		public string? Neighborhood { get; set; }

		[StringLength(250)]
		public string? ZipCode { get; set; }

		[Column("Country_name")]
		[StringLength(250)]
		public string? CountryName { get; set; }

		[Column("English_Test_Score")]
		public int? EnglishTestScore { get; set; }

		[Column("Cognitive_Ability_Score")]
		public int? CognitiveAbilityScore { get; set; }

		[Column("Cognitive_Test_Pic")]
		[StringLength(250)]
		public string? CognitiveTestPic { get; set; }

		[Column("English_Test_Pic")]
		[StringLength(250)]
		public string? EnglishTestPic { get; set; }

		[Column(TypeName = "decimal(18, 2)")]
		public decimal? Rate { get; set; }

		[Column("Additional_Code")]
		public int? AdditionalCode { get; set; }

		[Column("Relative_Address")]
		[StringLength(250)]
		public string? RelativeAddress { get; set; }

		[Column("National_Address")]
		[StringLength(250)]
		public string? NationalAddress { get; set; }

		[Column("WagePeriods_Certificate")]
		[StringLength(250)]
		public string? WagePeriodsCertificate { get; set; }
	}


}
