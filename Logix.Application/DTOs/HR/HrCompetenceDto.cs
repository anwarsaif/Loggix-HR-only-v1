using System.ComponentModel.DataAnnotations.Schema;

namespace Logix.Application.DTOs.HR
{
    public class HrCompetenceDto
    {
        public long Id { get; set; }

        public string? Name { get; set; }
        public long? CatId { get; set; }

        public string? Description { get; set; }
        [Column(TypeName = "decimal(18, 2)")]

        public decimal? Score { get; set; }
        public int? TypeId { get; set; }

        public int? KpiTypeId { get; set; }

        public int? MethodId { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? UnitRate { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool? IsDeleted { get; set; }
    }
    public class HrCompetenceEditDto
    {
        public long Id { get; set; }

        public string? Name { get; set; }

        public long? CatId { get; set; }


        public string? Description { get; set; }
        [Column(TypeName = "decimal(18, 2)")]

        public decimal? Score { get; set; }
        public long? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }

        public int? TypeId { get; set; }

        public int? KpiTypeId { get; set; }

        public int? MethodId { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? UnitRate { get; set; }
    }
}
