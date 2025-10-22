using Logix.Application.DTOs.Main;

namespace Logix.Application.DTOs.HR
{
    public class HrFixingEmployeeSalaryDto
    {
        public long Id { get; set; }
        public long? FacilityId { get; set; }
        public long? BranchId { get; set; }
        public int? EmpId { get; set; }
        public string? empCode { get; set; }
        public int? FixingType { get; set; }
        public int? SentTo { get; set; }
        public string? FixingDate { get; set; }
        public int? Status { get; set; }
        public string? Notes { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool? IsDeleted { get; set; }
        public List<SaveFileDto>? fileDtos { get; set; }

    }
    public class HrFixingEmployeeSalaryEditDto
    {
        public long Id { get; set; }
        public string? empCode { get; set; }

        public long? FacilityId { get; set; }
        public long? BranchId { get; set; }
        public int? EmpId { get; set; }
        public int? FixingType { get; set; }
        public int? SentTo { get; set; }
        public string? FixingDate { get; set; }
        public int? Status { get; set; }
        public string? Notes { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public List<SaveFileDto>? fileDtos { get; set; }
    }

    public class HrFixingEmployeeSalaryFilterDto
    {
 
        public int? FixingType { get; set; }
        public string? empCode { get; set; }
        public string? empName { get; set; }
        public int? SentTo { get; set; }
        public string? FixingDate { get; set; }
        public int? Status { get; set; }
    }
}
