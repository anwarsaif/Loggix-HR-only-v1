using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using Logix.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Table("HR_KPI_Detailes", Schema = "dbo")]
    public partial class HrKpiDetaile
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Column("KPI_ID")]
        public long? KpiId { get; set; }
        [Column("KPI_Tem_Com_ID")]
        [StringLength(10)]
        public string? KpiTemComId { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Degree { get; set; }
        public long? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
        public bool? IsDeleted { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? ActualTarget { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Target { get; set; }
        [Column("Template_ID")]
        public long? TemplateId { get; set; }
        [Column("Competences_ID")]
        public long? CompetencesId { get; set; }
        [Column("weight", TypeName = "decimal(18, 2)")]
        public decimal? Weight { get; set; }
    }
}
