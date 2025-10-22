using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

using Logix.Application.DTOs.Main;

namespace Logix.Application.DTOs.HR
{
    public class HrDirectJobDto
    {
        public long Id { get; set; }
        public List<long>? VacationIds { get; set; }
        public long? EmpId { get; set; }
        [Required]

        public string empCode { get; set; } = null!;
        [StringLength(10)]

        public string? Date1 { get; set; }
        [Required]
        [StringLength(10)]

        public string? DateDirect { get; set; }
        public string? Note { get; set; }
        [Required]
        public int TypeId { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool? IsDeleted { get; set; }
        public List<SaveFileDto>? fileDtos { get; set; }


    }

    public class HrDirectJobEditDto
    {
        public long Id { get; set; }
        [Required]
        public string empCode { get; set; } = null!;
        public long? EmpId { get; set; }
        [Required]
        public long VacationId { get; set; }
        public string? Date1 { get; set; }
        [Required]
        public string? DateDirect { get; set; }
        public string? Note { get; set; }
        [Required]
        public int? TypeId { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public List<SaveFileDto>? fileDtos { get; set; }

    }

    public class HrDirectJobFilterDto
    {
        public long Id { get; set; }
        public int? BranchId { get; set; }
        public string? EmpId { get; set; }
        public string? EmpName { get; set; }
        public int? LocationId { get; set; }
        public int? DeptId { get; set; }
        public string? From { get; set; }
        public string? To { get; set; }
        /////////////
        public string? BraName { get; set; }
        public string? DepName { get; set; }
        public string? LocName { get; set; }
        public string? TypeName { get; set; }
        public string? DirectDate { get; set; }
        public string? Note { get; set; }
        public string? LeaveType { get; set; }
        public string? LeaveStartDate { get; set; }
        public string? LeaveEndDate { get; set; }



    }

}
