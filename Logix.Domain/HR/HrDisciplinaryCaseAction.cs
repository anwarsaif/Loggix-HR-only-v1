using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Table("HR_Disciplinary_Case_Action")]
    public partial class HrDisciplinaryCaseAction
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Column("Disciplinary_Case_ID")]
        public long? DisciplinaryCaseId { get; set; }
        [Column("Action_Type")]
        public int? ActionType { get; set; }
        [Column("Emp_ID")]
        public long? EmpId { get; set; }
        [Column("Due_Date")]
        [StringLength(10)]
        public string? DueDate { get; set; }
        [Column("Status_ID")]
        public int? StatusId { get; set; }
        public string? Description { get; set; }
        public long CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        [Column("Count_Rept")]
        public int? CountRept { get; set; }
        [Column("Deducted_Rate", TypeName = "decimal(18, 2)")]
        public decimal? DeductedRate { get; set; }
        [Column("Deducted_Amount", TypeName = "decimal(18, 2)")]
        public decimal? DeductedAmount { get; set; }
        [Column("Visit_Schedule_D_ID")]
        public long? VisitScheduleDId { get; set; }
    }
}
