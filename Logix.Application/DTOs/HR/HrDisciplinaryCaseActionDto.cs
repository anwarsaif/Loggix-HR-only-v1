using Castle.MicroKernel.SubSystems.Conversion;
using Logix.Application.DTOs.Main;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Logix.Application.DTOs.HR
{
    public class HrDisciplinaryCaseActionDto
    {
        public long Id { get; set; }
        public long? DisciplinaryCaseId { get; set; }
        public int? ActionType { get; set; }
        [Required]
        public string? EmpCode { get; set; }
        public long? EmpId { get; set; }
        [StringLength(10)]
        public string? DueDate { get; set; }
        public int? StatusId { get; set; }
        public string? Description { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsDeleted { get; set; }
        public int? CountRept { get; set; }
        public decimal? DeductedRate { get; set; }
        public decimal? DeductedAmount { get; set; }
        public long? VisitScheduleDId { get; set; }
        public List<SaveFileDto>? fileDtos { get; set; }

    }
    public class HrDisciplinaryCaseActionEditDto
    {
        public long Id { get; set; }
        public long? DisciplinaryCaseId { get; set; }
        public int? ActionType { get; set; }
        public string? EmpCode { get; set; }
        public long? EmpId { get; set; }

        public string? EmpName { get; set; }

        [StringLength(10)]
        public string? DueDate { get; set; }
        public int? StatusId { get; set; }
        public string? Description { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? CountRept { get; set; }
        public decimal? DeductedRate { get; set; }
        public decimal? DeductedAmount { get; set; }
        public long? VisitScheduleDId { get; set; }
        public List<SaveFileDto>? fileDtos { get; set; }

    }

    public class HrDisciplinaryCaseActionFilterDto
    {
        //رقم الموظف
        public long? EmpId { get; set; }
        public string? EmpCode { get; set; }
        //نوع المخالفة
        public int? DisciplinaryCaseID { get; set; }
        //  الاجراء المتخذ
        public int? ActionTypeId { get; set; }
        public int? DeptId { get; set; }
        public int? BranchId { get; set; }
        public int? LocationProjectId { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }

    }
}
