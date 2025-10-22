using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Table("HR_Income_Tax", Schema = "dbo")]
    public partial class HrIncomeTax
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string? TaxCode { get; set; }
        [StringLength(200)]
        public string? TaxName { get; set; }
        [StringLength(200)]
        public string? TaxName2 { get; set; }
        [Column("AccountID")]
        public long? AccountId { get; set; }
        public long? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
