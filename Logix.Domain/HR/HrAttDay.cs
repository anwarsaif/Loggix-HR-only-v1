using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    [Table("HR_Att_Days")]
    public partial class HrAttDay
    {
        [Column("Day_No")]
        public int? DayNo { get; set; }
        [Column("Day_Name")]
        [StringLength(50)]
        public string? DayName { get; set; }
        [Column("Day_Name2")]
        [StringLength(50)]
        public string? DayName2 { get; set; }
    }
}
