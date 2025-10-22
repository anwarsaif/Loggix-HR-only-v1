using Logix.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Domain.HR
{
    [Table("HR_Leave_Allowance_Deduction")]
    public class HrLeaveAllowanceDeduction : TraceEntity
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }

        [Column("Leave_ID")]
        public long? LeaveId { get; set; }

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

}
