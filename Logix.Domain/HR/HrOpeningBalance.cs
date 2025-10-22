using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Logix.Domain.Base;

namespace Logix.Domain.HR
{
    [Table("HR_Opening_Balance")]
    public partial class HrOpeningBalance : TraceEntity
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Column("Emp_ID")]
        public long? EmpId { get; set; }
        [Column("Start_Date")]
        [StringLength(10)]
        public string? StartDate { get; set; }
        [Column("Type_ID")]
        public long? TypeId { get; set; }
        [Column("OB_Value", TypeName = "decimal(18, 2)")]
        public decimal? ObValue { get; set; }
    }
}
