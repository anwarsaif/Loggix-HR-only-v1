using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.Hr
{
    [Table("HR_Payroll_Note")]
    public partial class HrPayrollNote
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Column("MS_ID")]
        public long? MsId { get; set; }
        [Column("State_ID")]
        public int? StateId { get; set; }
        public long? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        public string? Note { get; set; }
    }
}
