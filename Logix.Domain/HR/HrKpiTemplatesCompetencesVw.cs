using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrKpiTemplatesCompetencesVw
    {
        [Column("ID")]
        public long Id { get; set; }
        public string? Subject { get; set; }
        [Column("Competences_ID")]
        public long? CompetencesId { get; set; }
        public string? Description { get; set; }
        public int? Score { get; set; }
        [Column("Template_ID")]
        public int? TemplateId { get; set; }
        [Column("Name_T")]
        public string? NameT { get; set; }
        public bool? IsDeleted { get; set; }
        [Column("weight", TypeName = "decimal(18, 2)")]
        public decimal? Weight { get; set; }
        [Column("Competences_Name")]
        public string? CompetencesName { get; set; }
        [Column("Type_ID")]
        public int? TypeId { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Target { get; set; }
        [Column("Method_ID")]
        public int? MethodId { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? UnitRate { get; set; }
        [Column("Kpi_Type_ID")]
        public int? KpiTypeId { get; set; }
    }
}
