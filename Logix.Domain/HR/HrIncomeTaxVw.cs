using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrIncomeTaxVw
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string? TaxCode { get; set; }
        [StringLength(200)]
        public string? TaxName { get; set; }
        [StringLength(200)]
        public string? TaxName2 { get; set; }
        [Column("AccountID")]
        public long? AccountId { get; set; }
        [StringLength(50)]
        public string? AccountCode { get; set; }
        [StringLength(255)]
        public string? AccountName { get; set; }
        [StringLength(255)]
        public string? AccountName2 { get; set; }
        public bool? IsDeleted { get; set; }
        public long? CreatedBy { get; set; }
    }
}
