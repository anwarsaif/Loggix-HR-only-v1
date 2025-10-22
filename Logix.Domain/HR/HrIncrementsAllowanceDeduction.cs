using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Logix.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Table("HR_Increments_Allowance_Deduction")]
    public partial class HrIncrementsAllowanceDeduction:TraceEntity
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }

        [Column("Increment_ID")]
        public long? IncrementId { get; set; }

        [Column("Type_ID")]
        public int? TypeId { get; set; }

        [Column("AD_ID")]
        public int? AdId { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Rate { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Amount { get; set; }

        [Column("New_Rate", TypeName = "decimal(18, 2)")]
        public decimal? NewRate { get; set; }

        [Column("New_Amount", TypeName = "decimal(18, 2)")]
        public decimal? NewAmount { get; set; }

        public bool? Status { get; set; }

        [Column("All_Ded_ID")]
        public long? AllDedId { get; set; }

        public bool IsNew { get; set; }

        public bool? IsUpdated { get; set; }
    }

}
