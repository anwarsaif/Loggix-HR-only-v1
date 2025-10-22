using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Logix.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Table("HR_Rate_Type")]
    public partial class HrRateType : TraceEntity
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Column("Rate_Name")]
        [StringLength(50)]
        public string? RateName { get; set; }
        [Column("Rate_Name2")]
        [StringLength(50)]
        public string? RateName2 { get; set; }
    }
}
