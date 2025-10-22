
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Logix.Application.DTOs.HR
{
    public class HrKpiTemplateDto
    {
        public long? Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Name2 { get; set; }

        public string? GroupsId { get; set; }
        [Range(1, long.MaxValue)]
        public int? TypeId { get; set; }
        [Column("KPI_Weight", TypeName = "decimal(18, 2)")]
        [Range(1, long.MaxValue)]
        public decimal? KpiWeight { get; set; }
        [Column("Competences_Weight", TypeName = "decimal(18, 2)")]

        [Required]
        public decimal? CompetencesWeight { get; set; }

        public string? Description { get; set; }

        [Required]
        public string? ReferenceNo { get; set; }

        [Required]
        public string? RevisionNo { get; set; }
        public int? ReadKpisId { get; set; }
        public int? MinKpis { get; set; }
        public int? MaxKpis { get; set; }
        public bool? IsDeleted { get; set; }

    }
    public class HrKpiTemplateEditDto
    {
        public long Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Name2 { get; set; }

        public string? GroupsId { get; set; }
        [Range(1, long.MaxValue)]
        public int? TypeId { get; set; }
        [Column("KPI_Weight", TypeName = "decimal(18, 2)")]
        [Range(1, long.MaxValue)]
        public decimal? KpiWeight { get; set; }
        [Column("Competences_Weight", TypeName = "decimal(18, 2)")]

        [Required]
        public decimal? CompetencesWeight { get; set; }

        public string? Description { get; set; }

        [Required]
        public string? ReferenceNo { get; set; }
        public int? ReadKpisId { get; set; }
        public int? MinKpis { get; set; }
        public int? MaxKpis { get; set; }

        [Required]
        public string? RevisionNo { get; set; }
        public long? ModifiedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }

}
