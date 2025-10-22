using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Domain.HR
{
    [Table("HR_Payroll_Transaction_Type_Values")]
    public partial class HrPayrollTransactionTypeValue
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("Payroll_Trans_ID")]
        public int? PayrollTransId { get; set; }

        public int? Value { get; set; }

        [Column("Facility_ID")]
        public int? FacilityId { get; set; }
    }
}
