
using Logix.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Application.DTOs.HR
{
    public class HrAttLocationEmployeeDto
    {
        public long Id { get; set; }
        public long? LocationId { get; set; }
        public List<long>? EmpIds { get; set; }
        [Required]
        [StringLength(10)]
        public string BeginDate { get; set; } = null!;
        [Required]
        [StringLength(10)]
        public string EndDate { get; set; } = null!;
        public long? EmpId { get; set; }
        public bool? IsDeleted { get; set; }


    }
    public class HrAttLocationEmployeeEditeDto
    {

        public long Id { get; set; }
        public long? LocationId { get; set; }
        public string? EmpCode { get; set; }
        [Required]
        [StringLength(10)]
        public string? BeginDate { get; set; }
        [Required]
        [StringLength(10)]
        public string? EndDate { get; set; }
        public long? ModifiedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }


    }

    public class HrAttLocationEmployeeCancelDto
    {
        [Required]
        public long? LocationId { get; set; }
        [Required]
        public List<long>? EmpId { get; set; }

    }
    public class HrAttLocationEmployeeFilterDto
    {
        public long? LocationId { get; set; }
        public int? BranchId { get; set; }
        public string? EmpName { get; set; }
        public string? EmpCode { get; set; }
        //===============search 2=====================
        public int? JobCatagoriesId { get; set; }
        public int? DeptId { get; set; }

        public int? Location { get; set; }

    }
}
