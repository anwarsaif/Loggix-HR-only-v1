using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Logix.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Table("HR_Att_TimeTable_Days")]
    public partial class HrAttTimeTableDay:TraceEntity
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Column("Day_No")]
        public int? DayNo { get; set; }
        [Column("TimeTable_ID")]
        public long? TimeTableId { get; set; }
    }
}
