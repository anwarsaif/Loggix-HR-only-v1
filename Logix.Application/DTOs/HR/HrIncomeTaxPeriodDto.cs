using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Application.DTOs.HR
{
    public class HrIncomeTaxPeriodDto
    {
        public int Id { get; set; }
        [StringLength(10)]
        public string? FromDate { get; set; }
        [StringLength(10)]
        public string? ToDate { get; set; }
        public int? IncomeTaxId { get; set; }
        public long? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
        public bool? IsDeleted { get; set; }
        [StringLength(50)]
        public string? PersonalExemption { get; set; }
    }
    public class HrIncomeTaxPeriodEditDto
    {
        public int? Id { get; set; }
        [StringLength(10)]
        public string? FromDate { get; set; }
        [StringLength(10)]
        public string? ToDate { get; set; }
        public int? IncomeTaxId { get; set; }
        public long? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
        public bool? IsDeleted { get; set; }
        [StringLength(50)]
        public string? PersonalExemption { get; set; }
    }
}
