using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Application.DTOs.HR
{
    public class HrAttendanceBioTimeDto
    {
        [Key]
        public long Id { get; set; }
        [StringLength(10)]
        public string? EmpId { get; set; }
        public DateTime? Checktime { get; set; }
        public int? Verified { get; set; }
        public int? CheckState { get; set; }
        public int? WorkCode { get; set; }
        public int? BranchId { get; set; }
        public int? SendLogix { get; set; }
        public int? Repeater { get; set; }
        [StringLength(10)]
        public string? Date { get; set; }
    }
}
