using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrRecruitmentCandidateKpiDVw
    {
        [Column("ID")]
        public long Id { get; set; }
        [Column("KPI_ID")]
        public long? KpiId { get; set; }
        [Column("KPI_Tem_Com_ID")]
        [StringLength(10)]
        public string? KpiTemComId { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Degree { get; set; }
        public bool? IsDeleted { get; set; }
        public string? Subject { get; set; }
        [Column("Competences_ID")]
        public long? CompetencesId { get; set; }
        public string? Description { get; set; }
        public int? Score { get; set; }
        [Column("Template_ID")]
        public int? TemplateId { get; set; }
        [Column("Candidate_ID")]
        public long? CandidateId { get; set; }
        [Column("Eva_Date")]
        [StringLength(50)]
        public string? EvaDate { get; set; }
        [Column("weight", TypeName = "decimal(18, 2)")]
        public decimal? Weight { get; set; }
        [Column("Candidate_Name")]
        [StringLength(250)]
        public string? CandidateName { get; set; }
        [Column("Vacancy_Name")]
        [StringLength(250)]
        public string? VacancyName { get; set; }
    }
}
