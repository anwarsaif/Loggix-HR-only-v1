using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrJobOfferVw
    {
        [Column("Vacancy_Name")]
        [StringLength(250)]
        public string? VacancyName { get; set; }
        [Column("ID")]
        public long Id { get; set; }
        [Column("JobCat_ID")]
        public int? JobCatId { get; set; }
        [Column("Shift_ID")]
        public int? ShiftId { get; set; }
        [Column("Type_Duration_Experiment_ID")]
        public int? TypeDurationExperimentId { get; set; }
        [Column("Duration_Experiment")]
        public int? DurationExperiment { get; set; }
        [Column("Basic_Salary", TypeName = "decimal(18, 2)")]
        public decimal? BasicSalary { get; set; }
        [Column("Total_Salary", TypeName = "decimal(18, 2)")]
        public decimal? TotalSalary { get; set; }
        [Column("Net_Salary", TypeName = "decimal(18, 2)")]
        public decimal? NetSalary { get; set; }
        [Column("Medical_Insurance", TypeName = "decimal(18, 2)")]
        public decimal? MedicalInsurance { get; set; }
        [Column("IS_Family_Medical_Insurance")]
        public bool? IsFamilyMedicalInsurance { get; set; }
        [Column("Flight_Tickets", TypeName = "decimal(18, 2)")]
        public decimal? FlightTickets { get; set; }
        [Column("IS_Family_Flight_Tickets")]
        public bool? IsFamilyFlightTickets { get; set; }
        public long? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        [Column("Trial_Type")]
        public int? TrialType { get; set; }
        [Column("Trial_Count")]
        public int? TrialCount { get; set; }
        [Column("Contract_Type_ID")]
        public int? ContractTypeId { get; set; }
        [Column("Specialization_Name")]
        [StringLength(250)]
        public string? SpecializationName { get; set; }
        [Column("Qualification_Name")]
        [StringLength(250)]
        public string? QualificationName { get; set; }
        [StringLength(250)]
        public string? Name { get; set; }
        [Column("Name_En")]
        [StringLength(250)]
        public string? NameEn { get; set; }
        [Column("Recru_Applicant_ID")]
        public int? RecruApplicantId { get; set; }
        [Column("Catagories_ID")]
        public int? CatagoriesId { get; set; }
        [Column("File_URL")]
        public string? FileUrl { get; set; }
        [Column("Job_Catagories_Name")]
        [StringLength(250)]
        public string? JobCatagoriesName { get; set; }
        [Column("Job_Catagories_Name2")]
        [StringLength(250)]
        public string? JobCatagoriesName2 { get; set; }
        [Column("Housing_allowance", TypeName = "decimal(18, 2)")]
        public decimal? HousingAllowance { get; set; }
        [Column("Transport_allowance", TypeName = "decimal(18, 2)")]
        public decimal? TransportAllowance { get; set; }
        [Column("Other_Allowance", TypeName = "decimal(18, 2)")]
        public decimal? OtherAllowance { get; set; }
    }
}
