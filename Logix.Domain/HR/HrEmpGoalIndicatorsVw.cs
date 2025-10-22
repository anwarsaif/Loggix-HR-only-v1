using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrEmpGoalIndicatorsVw
    {
        [Column("Emp_ID")]
        public long? EmpId { get; set; }
        [Column("weight")]
        [StringLength(50)]
        public string? Weight { get; set; }
        [StringLength(50)]
        public string? Target { get; set; }
        public string? Note { get; set; }
        public string? Subject { get; set; }
        [Column("Template_ID")]
        public long? TemplateId { get; set; }
        public bool IsDeleted { get; set; }
        [Column("Kpi_Type_ID")]
        public int? KpiTypeId { get; set; }
        [Column("Method_ID")]
        public int? MethodId { get; set; }
        [Column("Type_ID")]
        public int? TypeId { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? UnitRate { get; set; }
        [Column("Competences_ID")]
        public long CompetencesId { get; set; }
        public string? Description { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Score { get; set; }
        [Column("ID")]
        public long Id { get; set; }
    }
}
