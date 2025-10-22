using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrRecruitmentVacancyVw
    {
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
        public bool? IsDeleted { get; set; }
        [Column("BRA_NAME")]
        public string? BraName { get; set; }
        [Column("Job_Catagories_Name")]
        [StringLength(250)]
        public string? JobCatagoriesName { get; set; }
        [Column("Dep_Name")]
        [StringLength(200)]
        public string? DepName { get; set; }
        [Column("Job_Description")]
        public string? JobDescription { get; set; }
        [Column("Status_ID")]
        public int? StatusId { get; set; }
        public long? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
        [Column("Status_Name")]
        [StringLength(250)]
        public string? StatusName { get; set; }
        [Column("Location_Name")]
        [StringLength(200)]
        public string? LocationName { get; set; }
        public string? ShortDescription { get; set; }
        [Column("Job_Type")]
        public int? JobType { get; set; }
        [Column("Is_Online")]
        public bool? IsOnline { get; set; }
        [StringLength(50)]
        public string? Experience { get; set; }
        [Column("Qualification_ID")]
        public int? QualificationId { get; set; }
        [Column("Specification_ID")]
        public int? SpecificationId { get; set; }
        public int? Nationality { get; set; }
        [StringLength(50)]
        public string? Gender { get; set; }
        [Column("Last_Apply_Date")]
        [StringLength(10)]
        public string? LastApplyDate { get; set; }
        [Column("Age_from")]
        public int? AgeFrom { get; set; }
        [Column("Age_To")]
        public int? AgeTo { get; set; }
        [Column("Salary_From", TypeName = "decimal(18, 2)")]
        public decimal? SalaryFrom { get; set; }
        [Column("Salary_To", TypeName = "decimal(18, 2)")]
        public decimal? SalaryTo { get; set; }
        [Column("Qualification_Name")]
        [StringLength(250)]
        public string? QualificationName { get; set; }
        [Column("Qualification_Name2")]
        [StringLength(250)]
        public string? QualificationName2 { get; set; }
        [Column("Specialization_Name")]
        [StringLength(250)]
        public string? SpecializationName { get; set; }
        [Column("Specialization_Name2")]
        [StringLength(250)]
        public string? SpecializationName2 { get; set; }
    }

    public partial class HrRecruitmentVacancyVwDto
    {
        public long Id { get; set; }
        [StringLength(250)]
        public string? VacancyName { get; set; }
        public long? JobId { get; set; }
        public int? BranchId { get; set; }
        public int? DeptId { get; set; }
        public int? LocationId { get; set; }
        public int? FacilityId { get; set; }
        public int? NumberPosition { get; set; }
        public string? BraName { get; set; }
        [StringLength(250)]
        public string? JobCatagoriesName { get; set; }
        public string? DepName { get; set; }
        public string? JobDescription { get; set; }
        public int? StatusId { get; set; }
     
        public string? StatusName { get; set; }
        [StringLength(200)]
        public string? LocationName { get; set; }
        public string? ShortDescription { get; set; }
        public int? JobType { get; set; }
        public bool? IsOnline { get; set; }
        [StringLength(50)]
        public string? Experience { get; set; }
        public int? QualificationId { get; set; }
        public int? SpecificationId { get; set; }
        public int? Nationality { get; set; }
        [StringLength(50)]
        public string? Gender { get; set; }
        [Column("Last_Apply_Date")]
        [StringLength(10)]
        public string? LastApplyDate { get; set; }
        public int? AgeFrom { get; set; }
        public int? AgeTo { get; set; }
        public decimal? SalaryFrom { get; set; }
        public decimal? SalaryTo { get; set; }
        [StringLength(250)]
        public string? QualificationName { get; set; }
        [StringLength(250)]
        public string? QualificationName2 { get; set; }
        [StringLength(250)]
        public string? SpecializationName { get; set; }
        [StringLength(250)]
        public string? SpecializationName2 { get; set; }
        public string? CreatedOnString { get; set; }

        /// <summary>
        /// تمت اضافة الحقل من أجل استخدامه في العرض 
        /// </summary>
        public string? CntApplicants { get; set; }
    }
}
