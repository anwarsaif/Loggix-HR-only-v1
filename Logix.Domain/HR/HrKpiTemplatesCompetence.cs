using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Logix.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Table("HR_KPI_Templates_Competences")]
    public partial class HrKpiTemplatesCompetence: TraceEntity
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        public string? Subject { get; set; }
        [Column("Competences_ID")]
        public long? CompetencesId { get; set; }
        public string? Description { get; set; }
        public int? Score { get; set; }
        [Column("Template_ID")]
        public int? TemplateId { get; set; }
        [Column("weight", TypeName = "decimal(18, 2)")]
        public decimal? Weight { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Target { get; set; }
    }
}
