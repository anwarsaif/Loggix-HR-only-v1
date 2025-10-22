using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Logix.Application.DTOs.HR
{
    public class HrEvaluationAnnualIncreaseConfigDto
    {
        public long? Id { get; set; }
        [StringLength(500)]

        public string Name { get; set; } = null!;



        public decimal EvaluationFrom { get; set; }

        [Column(TypeName = "decimal(18, 2)")]

        public decimal EvaluationTo { get; set; }

        [Column(TypeName = "decimal(18, 2)")]

        public decimal IncreasePercentage { get; set; }


        public bool? Eligible { get; set; }


        public string? Note { get; set; }

        public long? FacilityId { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool? IsDeleted { get; set; }


    }
    public class HrEvaluationAnnualIncreaseConfigEditDto
    {
        public long? Id { get; set; }
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

        public long? FacilityId { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

    }


}
