using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Application.DTOs.HR
{
    public class HrAttShiftCloseDto
    {
        public long Id { get; set; }

        public long? ShiftId { get; set; }

        [StringLength(10)]
        public string? DateClose { get; set; }

        public int? CountAll { get; set; }

        public int? CountAttendances { get; set; }

        public int? CountAbsence { get; set; }

        public int? CountVacations { get; set; }

        public int? CountVacations2 { get; set; }

        public int? CountWithoutmovement { get; set; }

        
    }
    public class HrAttShiftCloseEditDto
    {
        public long Id { get; set; }

        public long? ShiftId { get; set; }

        [StringLength(10)]
        public string? DateClose { get; set; }

        public int? CountAll { get; set; }

        public int? CountAttendances { get; set; }

        public int? CountAbsence { get; set; }

        public int? CountVacations { get; set; }

        public int? CountVacations2 { get; set; }

        public int? CountWithoutmovement { get; set; }

        public long? ModifiedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
