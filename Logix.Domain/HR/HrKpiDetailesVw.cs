using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrKpiDetailesVw
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
        [Column("Eva_Date")]
        [StringLength(50)]
        public string? EvaDate { get; set; }
        [Column("Emp_ID")]
        public long? EmpId { get; set; }
        public bool? IsDeleted { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? ActualTarget { get; set; }
        public string? Subject { get; set; }
        [Column("Competences_ID")]
        public long? CompetencesId { get; set; }
        public string? Description { get; set; }
        public decimal? Score { get; set; }
        [Column("Template_ID")]
        public long? TemplateId { get; set; }
        [Column("Type_ID")]
        public int? TypeId { get; set; }
        [Column("weight", TypeName = "decimal(18, 2)")]
        public decimal? Weight { get; set; }
        [Column("Method_ID")]
        public int? MethodId { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? UnitRate { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Target { get; set; }
        [Column("KPI_Tem_ID")]
        public int? KpiTemId { get; set; }
        [Column("KPI_IsDeleted")]
        public bool KpiIsDeleted { get; set; }
    }
}
