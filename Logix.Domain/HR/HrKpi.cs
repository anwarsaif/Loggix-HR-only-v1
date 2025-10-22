using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Logix.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Table("HR_KPI")]
    public partial class HrKpi:TraceEntity
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Column("Emp_ID")]
        public long? EmpId { get; set; }
        [Column("KPI_Tem_ID")]
        public int? KpiTemId { get; set; }
        [Column("Eva_Date")]
        [StringLength(50)]
        public string? EvaDate { get; set; }
        [Column("Status_ID")]
        public int? StatusId { get; set; }
        [Column("App_ID")]
        public long? AppId { get; set; }
        [Column("Performance_ID")]
        public long? PerformanceId { get; set; }
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
        public string? Note { get; set; }
        [Column("Probation_Result")]
        public long? ProbationResult { get; set; }
        [Column("FinalRating_KPI", TypeName = "decimal(18, 2)")]
        public decimal? FinalRatingKpi { get; set; }
        [Column("FinalRating_Competences", TypeName = "decimal(18, 2)")]
        public decimal? FinalRatingCompetences { get; set; }
    }
}
