using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Logix.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Table("HR_Competences")]
    public partial class HrCompetence :TraceEntity
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        public string? Name { get; set; }
        [Column("Cat_ID")]
        public long? CatId { get; set; }
        public string? Description { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Score { get; set; }
       
        [Column("Type_ID")]
        public int? TypeId { get; set; }
        [Column("Kpi_Type_ID")]
        public int? KpiTypeId { get; set; }
        [Column("Method_ID")]
        public int? MethodId { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? UnitRate { get; set; }
    }
}
