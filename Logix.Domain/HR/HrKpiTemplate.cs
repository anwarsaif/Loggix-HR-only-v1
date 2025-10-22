using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Logix.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Table("HR_KPI_Templates", Schema = "dbo")]
    public partial class HrKpiTemplate
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Name2 { get; set; }
        public long? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
        public bool? IsDeleted { get; set; }
        [Column("Groups_ID")]
        public string? GroupsId { get; set; }
        [Column("Type_ID")]
        public int? TypeId { get; set; }
        [Column("KPI_Weight", TypeName = "decimal(18, 2)")]
        public decimal? KpiWeight { get; set; }
        [Column("Competences_Weight", TypeName = "decimal(18, 2)")]
        public decimal? CompetencesWeight { get; set; }
        public string? Description { get; set; }
        [Column("Reference_NO")]
        public string? ReferenceNo { get; set; }
        [Column("Revision_NO")]
        public string? RevisionNo { get; set; }
        [Column("ReadKPIs_ID")]
        public int? ReadKpisId { get; set; }
        [Column("MinKPIs")]
        public int? MinKpis { get; set; }
        [Column("MaxKPIs")]
        public int? MaxKpis { get; set; }
    }
}
