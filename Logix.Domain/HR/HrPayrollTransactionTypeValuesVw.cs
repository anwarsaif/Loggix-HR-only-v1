using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrPayrollTransactionTypeValuesVw
    {
        [Column("ID")]
        public int Id { get; set; }

        [StringLength(50)]
        public string? Name { get; set; }

        [StringLength(50)]
        public string? Name2 { get; set; }

        public int? Value { get; set; }

        [Column("Facility_ID")]
        public int? FacilityId { get; set; }
    }

}
