using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrDecisionsTypeEmployeeVw
    {
        [Column("Decisions_Type_ID")]
        public long? DecisionsTypeId { get; set; }
        [Column("Emp_ID")]
        public long? EmpId { get; set; }
        [Column("Emp_Code")]
        [StringLength(50)]
        public string EmpCode { get; set; } = null!;
        [Column("Emp_name")]
        [StringLength(250)]
        public string? EmpName { get; set; }
        public bool? IsDeleted { get; set; }
        [Column("ID")]
        public long Id { get; set; }
    }
}
