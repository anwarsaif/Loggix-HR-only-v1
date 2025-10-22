using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Logix.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrKpiVw:TraceEntity
    {
        [Column("Emp_name")]
        [StringLength(250)]
        public string? EmpName { get; set; }
        [Column("ID")]
        public long Id { get; set; }
        [Column("Emp_ID")]
        public long? EmpId { get; set; }
        [Column("Eva_Date")]
        [StringLength(50)]
        public string? EvaDate { get; set; }
        [Column("Status_ID")]
        public int? StatusId { get; set; }

        [Column("BRA_NAME")]
        public string? BraName { get; set; }
        [Column("BRANCH_ID")]
        public int? BranchId { get; set; }
        [Column("Emp_Code")]
        [StringLength(50)]
        public string EmpCode { get; set; } = null!;
        [Column("App_ID")]
        public long? AppId { get; set; }
        [Column("KPI_Tem_ID")]
        public int? KpiTemId { get; set; }
        [Column("Tem_Name")]
        public string? TemName { get; set; }
        [Column("Dept_ID")]
        public int? DeptId { get; set; }
        public int? Location { get; set; }
        [Column("Facility_ID")]
        public int? FacilityId { get; set; }
        [Column("Emp_name2")]
        [StringLength(250)]
        public string? EmpName2 { get; set; }
        [Column("Manager_ID")]
        public long? ManagerId { get; set; }
        [Column("Performance_ID")]
        public long? PerformanceId { get; set; }
        [Column("Evaluator_Name")]
        [StringLength(250)]
        public string? EvaluatorName { get; set; }
        [Column("Evaluator_Code")]
        [StringLength(50)]
        public string? EvaluatorCode { get; set; }
        [Column("Tem_Name2")]
        public string? TemName2 { get; set; }
        [Column("BRA_NAME2")]
        public string? BraName2 { get; set; }
        public string? Achievements { get; set; }
        [Column("Strengths_Points")]
        public string? StrengthsPoints { get; set; }
        [Column("Weaknesses_Points")]
        public string? WeaknessesPoints { get; set; }
        [Column("Suggested_Training")]
        public string? SuggestedTraining { get; set; }
        public string? Recommendations { get; set; }
        [Column("Type_ID")]
        public int? TypeId { get; set; }
        [Column("Final_Rating", TypeName = "decimal(18, 2)")]
        public decimal? FinalRating { get; set; }
        [Column("Start_date")]
        [StringLength(10)]
        public string? StartDate { get; set; }
        [Column("End_Date")]
        [StringLength(10)]
        public string? EndDate { get; set; }
        [Column("Probation_Result")]
        public long? ProbationResult { get; set; }
        public string? Note { get; set; }
        [Column("FinalRating_KPI", TypeName = "decimal(18, 2)")]
        public decimal? FinalRatingKpi { get; set; }
        [Column("FinalRating_Competences", TypeName = "decimal(18, 2)")]
        public decimal? FinalRatingCompetences { get; set; }
        [Column("KPI_Weight", TypeName = "decimal(18, 2)")]
        public decimal? KpiWeight { get; set; }
        [Column("Competences_Weight", TypeName = "decimal(18, 2)")]
        public decimal? CompetencesWeight { get; set; }
        [Column("Probation_Result_Name")]
        [StringLength(250)]
        public string? ProbationResultName { get; set; }
        public string? Description { get; set; }
        [Column("Reference_NO")]
        public string? ReferenceNo { get; set; }
        [Column("Revision_NO")]
        public string? RevisionNo { get; set; }
    }
}
