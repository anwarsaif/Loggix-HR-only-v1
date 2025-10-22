using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Table("HR_Income_Tax_Period", Schema = "dbo")]
    public partial class HrIncomeTaxPeriod
    {
        [Key]
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
}
