using Castle.MicroKernel.SubSystems.Conversion;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Logix.Application.DTOs.HR
{
    public class HrDelayDto
    {
        public long? Id { get; set; }

        public long? EmpId { get; set; }
        [Required]
        public string? EmpCode { get; set; }
        [Required]
        public string? DelayDate { get; set; }
        [Required]
        public string DelayTimeString { get; set; } = null!;
        public TimeSpan? DelayTime { get; set; }
        public long? AttendanceId { get; set; }
        public string? Note { get; set; }
        public int? TypeId { get; set; }
        public bool? IsDeleted { get; set; }

    }

    public class HrDelayEditDto
    {
        public long Id { get; set; }

        public long? EmpId { get; set; }

        public string? DelayDate { get; set; }
        public string? DelayTime { get; set; }

        public long? AttendanceId { get; set; }
        public string? Note { get; set; }

        public int? TypeId { get; set; }
    }

    public class HrDelayFilterDto
    {
        public long? Id { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public int? TypeId { get; set; }
        public long? EmpId { get; set; }
        public string? EmpCode { get; set; }
        public string? EmpName { get; set; }
        public string? DelayDate { get; set; }
        public string? DelayTime { get; set; }
        public string? TypeName { get; set; }
        public string? Note { get; set; }
        public int? FacilityId { get; set; }
        public int? BranchId { get; set; }
        public int? DeptId { get; set; }
        public int? LocationId { get; set; }


        //////////////////////
        public string? DeptName { get; set; }
        public string? LocationName { get; set; }
        public string? theDate { get; set; }
        public string? DelayDuration { get; set; }
    }
    public class HrDelayNonCheckoutDto
    {
        [Required]
        public string? FromDate { get; set; }
        [Required]

        public string? ToDate { get; set; }

    }

    public class HrDelayAddDto
    {
        public long? Id { get; set; }

        [Required]
        public string? EmpCode { get; set; }
        [Required]
        public string? DelayDate { get; set; }
        [Required]
        public string? DelayTimeString { get; set; }
        public string? Note { get; set; }
        public int? TypeId { get; set; }
    }

    public class ApproveDto
    {
        public string? EmpCode { get; set; }
        public string? ApproveDate{ get; set; }
        public string? HoursMins { get; set; }
    }
}
