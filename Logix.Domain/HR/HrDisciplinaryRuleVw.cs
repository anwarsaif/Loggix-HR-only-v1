using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrDisciplinaryRuleVw
    {
        [Column("ID")]
        public long Id { get; set; }
        [StringLength(500)]
        public string? Name { get; set; }
        [Column("Disciplinary_Case_ID")]
        public long? DisciplinaryCaseId { get; set; }
        [Column("Rept_From")]
        public int? ReptFrom { get; set; }
        [Column("Rept_To")]
        public int? ReptTo { get; set; }
        [Column("Action_Type")]
        public int? ActionType { get; set; }
        [Column("Deducted_Rate", TypeName = "decimal(18, 2)")]
        public decimal? DeductedRate { get; set; }
        [Column("Deducted_Amount", TypeName = "decimal(18, 2)")]
        public decimal? DeductedAmount { get; set; }
        [Column("Deducted_late")]
        public bool? DeductedLate { get; set; }
        [Column("Action_Name")]
        [StringLength(250)]
        public string? ActionName { get; set; }
        [StringLength(500)]
        public string? Name2 { get; set; }
    }
}
