using Logix.Domain.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Domain.HR
{
    [Keyless]
    [Table("HR_Att_Shift_Close")]
    public partial class HrAttShiftClose
    {
        [Column("ID")]
        public long Id { get; set; }
        [Column("Shift_ID")]
        public long? ShiftId { get; set; }
        [Column("Date_Close")]
        [StringLength(10)]
        public string? DateClose { get; set; }
        [Column("Count_All")]
        public int? CountAll { get; set; }
        [Column("Count_Attendances")]
        public int? CountAttendances { get; set; }
        [Column("Count_Absence")]
        public int? CountAbsence { get; set; }
        [Column("Count_Vacations")]
        public int? CountVacations { get; set; }
        [Column("Count_Vacations2")]
        public int? CountVacations2 { get; set; }
        [Column("Count_Withoutmovement")]
        public int? CountWithoutmovement { get; set; }
        public long CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}
