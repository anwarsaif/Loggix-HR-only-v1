using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Application.DTOs.HR
{
    public class HrPayrollTransactionTypeValueDto
    {
        public int? Id { get; set; }

        [Column("Payroll_Trans_ID")]
        public int? PayrollTransId { get; set; }

        public int? Value { get; set; }

        [Column("Facility_ID")]
        public int? FacilityId { get; set; }
    }
    public class HrPayrollTransactionTypeValueEditDto
    {
        public int Id { get; set; }

        [Column("Payroll_Trans_ID")]
        public int? PayrollTransId { get; set; }

        public int? Value { get; set; }

        [Column("Facility_ID")]
        public int? FacilityId { get; set; }
    }
}
