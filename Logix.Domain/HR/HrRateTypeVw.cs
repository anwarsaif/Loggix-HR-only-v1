using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrRateTypeVw
    {
        [Column("ID")]
        public long Id { get; set; }
        [Column("Rate_Name")]
        [StringLength(50)]
        public string? RateName { get; set; }
        [Column("Rate_Name2")]
        [StringLength(50)]
        public string? RateName2 { get; set; }
        public long? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}
