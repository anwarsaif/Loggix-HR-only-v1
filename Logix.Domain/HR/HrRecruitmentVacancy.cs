using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Table("HR_Recruitment_Vacancy")]
    public partial class HrRecruitmentVacancy
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Column("Vacancy_Name")]
        [StringLength(250)]
        public string? VacancyName { get; set; }
        [Column("Job_ID")]
        public long? JobId { get; set; }
        [Column("BRANCH_ID")]
        public int? BranchId { get; set; }
        [Column("Dept_ID")]
        public int? DeptId { get; set; }
        [Column("Location_ID")]
        public int? LocationId { get; set; }
        [Column("Facility_ID")]
        public int? FacilityId { get; set; }
        [Column("Number_Position")]
        public int? NumberPosition { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
        public bool? IsDeleted { get; set; }
        [Column("Job_Description")]
        public string? JobDescription { get; set; }
        [Column("Status_ID")]
        public int? StatusId { get; set; }
        public long? CreatedBy { get; set; }
        public string? ShortDescription { get; set; }
        [Column("Job_Type")]
        public int? JobType { get; set; }
        [Column("Is_Online")]
        public bool? IsOnline { get; set; }
        [Column("Salary_From", TypeName = "decimal(18, 2)")]
        public decimal? SalaryFrom { get; set; }
        [Column("Salary_To", TypeName = "decimal(18, 2)")]
        public decimal? SalaryTo { get; set; }
        [StringLength(50)]
        public string? Experience { get; set; }
        [Column("Qualification_ID")]
        public int? QualificationId { get; set; }
        [Column("Specification_ID")]
        public int? SpecificationId { get; set; }
        public int? Nationality { get; set; }
        [StringLength(50)]
        public string? Gender { get; set; }
        [Column("Age_from")]
        public int? AgeFrom { get; set; }
        [Column("Age_To")]
        public int? AgeTo { get; set; }
        [Column("Last_Apply_Date")]
        [StringLength(10)]
        public string? LastApplyDate { get; set; }
        public long? Country { get; set; }
        public long? City { get; set; }
    }
}
