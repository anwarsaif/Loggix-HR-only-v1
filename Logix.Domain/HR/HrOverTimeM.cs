using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Logix.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Table("HR_OverTime_M")]
    public partial class HrOverTimeM : TraceEntity
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Column("Emp_ID")]
        public long? EmpId { get; set; }
        [Column("Date_From")]
        [StringLength(10)]
        public string? DateFrom { get; set; }
        [Column("Date_To")]
        [StringLength(10)]
        public string? DateTo { get; set; }
        [Column("Date_Tran")]
        [StringLength(10)]
        public string? DateTran { get; set; }
        [Column("Refrance_ID")]
        [StringLength(50)]
        public string? RefranceId { get; set; }
        [Column("Payment_Type")]
        public int? PaymentType { get; set; }
        public string? Note { get; set; }
        [Column("Cnt_Hours_Total", TypeName = "decimal(18, 2)")]
        public decimal? CntHoursTotal { get; set; }
        [Column("Cnt_Hours_Day", TypeName = "decimal(18, 2)")]
        public decimal? CntHoursDay { get; set; }
        [Column("Cnt_Hours_Month", TypeName = "decimal(18, 2)")]
        public decimal? CntHoursMonth { get; set; }
        [Column("Project_ID")]
        public long? ProjectId { get; set; }
    }
}
