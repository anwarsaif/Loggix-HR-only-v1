using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR;

[Keyless]
public partial class HrClearanceAllowanceVw
{
    [Column("ID")]
    public long Id { get; set; }

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

    public long CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedOn { get; set; }

    public long? ModifiedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedOn { get; set; }

    public bool IsDeleted { get; set; }

    [StringLength(250)]
    public string? Name { get; set; }

    [Column("Catagories_ID")]
    public int? CatagoriesId { get; set; }

    [Column("Fixed_Or_Temporary")]
    public int? FixedOrTemporary { get; set; }

    [Column("Clearance_ID")]
    public long? ClearanceId { get; set; }

    [Column("New_Amount", TypeName = "decimal(18, 2)")]
    public decimal? NewAmount { get; set; }
}
