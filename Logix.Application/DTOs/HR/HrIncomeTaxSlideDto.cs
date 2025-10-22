using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Application.DTOs.HR
{
    public class HrIncomeTaxSlideDto
    {
        public int? Id { get; set; }

        public int? TaxSlideOrderNo { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? TaxSlideValue { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? TaxSlideRate { get; set; }

        [StringLength(50)]
        public string? TaxSlideStartingFromTheSlideNo { get; set; }

        [StringLength(250)]
        public string? TaxSlideNote { get; set; }

        public int? IncomeTaxPeriodId { get; set; }

        public long? CreatedBy { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }

        public long? ModifiedBy { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }

        public bool? IsDeleted { get; set; }
    }
    public class HrIncomeTaxSlideEditDto
    {
        public int Id { get; set; }

        public int? TaxSlideOrderNo { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? TaxSlideValue { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? TaxSlideRate { get; set; }

        [StringLength(50)]
        public string? TaxSlideStartingFromTheSlideNo { get; set; }

        [StringLength(250)]
        public string? TaxSlideNote { get; set; }

        public int? IncomeTaxPeriodId { get; set; }

        public long? CreatedBy { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }

        public long? ModifiedBy { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }

        public bool? IsDeleted { get; set; }
    }
}
