using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Logix.Domain.Base;

namespace Logix.Domain.HR;

[Table("HR_Clearance_Allowance_Deduction")]
public partial class HrClearanceAllowanceDeduction : TraceEntity
{
    [Key]
    [Column("ID")]
    public long Id { get; set; }

    [Column("Clearance_ID")]
    public long? ClearanceId { get; set; }

    [Column("Emp_ID")]
    public long? EmpId { get; set; }

    [Column("Type_ID")]
    public int? TypeId { get; set; }

    [Column("AD_ID")]
    public int? AdId { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Rate { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Amount { get; set; }

    [Column("New_Amount", TypeName = "decimal(18, 2)")]
    public decimal? NewAmount { get; set; }

    [Column("Fixed_Or_Temporary")]
    public int? FixedOrTemporary { get; set; }
}
