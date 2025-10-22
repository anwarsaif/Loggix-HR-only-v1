using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Logix.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Table("HR_Att_Shift_TimeTable")]
    public partial class HrAttShiftTimeTable:TraceEntity
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Column("Shift_ID")]
        public long? ShiftId { get; set; }
        [Column("TimeTable_ID")]
        public long? TimeTableId { get; set; }
    }
}
