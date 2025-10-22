using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Application.DTOs.HR
{
    public class HrRequestGoalsEmployeeDetailDto
    {
        public long Id { get; set; }
        public string? Target { get; set; }
        public long? Weight { get; set; }
        public long? RequestGoalsEmpId { get; set; }
        public decimal? TargetValue { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
