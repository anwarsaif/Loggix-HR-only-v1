using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrCompetencesVw
    {
        [Column("ID")]
        public long Id { get; set; }
        [Column("Cat_ID")]
        public long? CatId { get; set; }
        public string? Name { get; set; }
        [Column("Cat_Name")]
        [StringLength(50)]
        public string? CatName { get; set; }
        public string? Description { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Score { get; set; }
        public bool? IsDeleted { get; set; }
        [Column("Type_ID")]
        public int? TypeId { get; set; }
        [Column("Kpi_Type_ID")]
        public int? KpiTypeId { get; set; }
        [Column("Method_ID")]
        public int? MethodId { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? UnitRate { get; set; }
        [Column("Kpi_Type_Name")]
        [StringLength(250)]
        public string? KpiTypeName { get; set; }
    }
}
