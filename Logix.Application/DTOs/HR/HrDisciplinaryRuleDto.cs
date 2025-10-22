using Castle.MicroKernel.SubSystems.Conversion;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Logix.Application.DTOs.HR
{
    public class HrDisciplinaryRuleDto
    {
        public long Id { get; set; }
        [StringLength(500)]
        public string? Name { get; set; }
        public long? DisciplinaryCaseId { get; set; }
        public int? ReptFrom { get; set; }
        public int? ReptTo { get; set; }
        public int? ActionType { get; set; }
        public decimal? DeductedRate { get; set; }
        public decimal? DeductedAmount { get; set; }
        public bool? DeductedLate { get; set; }
        [StringLength(500)]
        public string? Name2 { get; set; }
    }
    public class HrDisciplinaryRuleEditDto
    {
        public long Id { get; set; }
        [StringLength(500)]
        public string? Name { get; set; }
        public long? DisciplinaryCaseId { get; set; }
        public int? ReptFrom { get; set; }
        public int? ReptTo { get; set; }
        public int? ActionType { get; set; }
        public decimal? DeductedRate { get; set; }
        public decimal? DeductedAmount { get; set; }
        public bool? DeductedLate { get; set; }
        [StringLength(500)]
        public string? Name2 { get; set; }
    }

}
