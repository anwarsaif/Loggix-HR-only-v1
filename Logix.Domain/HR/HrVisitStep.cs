using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Logix.Domain.Base;

namespace Logix.Domain.HR;

[Table("HR_Visit_Steps")]
public partial class HrVisitStep : TraceEntity
{
    [Key]
    [Column("ID")]
    public long Id { get; set; }

    [Column("Step_Name")]
    [StringLength(500)]
    public string? StepName { get; set; }

    [Column("Step_Name2")]
    [StringLength(500)]
    public string? StepName2 { get; set; }

    [Column("Groups_ID")]
    public string? GroupsId { get; set; }

    [Column("To_Step_ID")]
    public long? ToStepId { get; set; }

    //public long? CreatedBy { get; set; }
    //[Column(TypeName = "datetime")]
    //public DateTime? CreatedOn { get; set; }
    //public long? ModifiedBy { get; set; }
    //[Column(TypeName = "datetime")]
    //public DateTime? ModifiedOn { get; set; }
    //public bool? IsDeleted { get; set; }
}
