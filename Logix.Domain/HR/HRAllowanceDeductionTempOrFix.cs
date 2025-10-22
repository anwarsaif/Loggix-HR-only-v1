using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Domain.HR
{
    [Table("HR_Allowance_Deduction_TempOrFix")]

    public class HrAllowanceDeductionTempOrFix
    {
        [Key]
        public int Id { get; set; }
        [Column("Type_name")]
        [StringLength(50)]
        public string? TypeName { get; set; }
    }
}
