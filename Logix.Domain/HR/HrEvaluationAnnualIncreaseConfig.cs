using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Logix.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Table("HR_EvaluationAnnualIncrease_Config")]
    public partial class HrEvaluationAnnualIncreaseConfig : TraceEntity
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [StringLength(500)]
        public string Name { get; set; } = null!;
        [Column(TypeName = "decimal(18, 2)")]
        public decimal EvaluationFrom { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal EvaluationTo { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal IncreasePercentage { get; set; }
        public bool? Eligible { get; set; }
        public string? Note { get; set; }
        [Column("Facility_ID")]
        public long? FacilityId { get; set; }
       
    }
}
