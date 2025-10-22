using Logix.Application.DTOs.Main;

namespace Logix.Application.DTOs.HR
{
    public class HrTicketDto
    {
        public long? Id { get; set; }
        public long? EmpId { get; set; }
        public string? EmpCode { get; set; }
        public int? Way { get; set; }
        public int? Cabins { get; set; }
        public string? TicketDate { get; set; }
        public string? TicketNo { get; set; }
        public decimal? TicketCount { get; set; }
        public decimal? TicketAmount { get; set; }
        public int? IsBillable { get; set; }
        public string? Purpose { get; set; }
        public string? Note { get; set; }

        public bool IsDeleted { get; set; }
        public decimal? TotalAmount { get; set; }

        public List<SaveFileDto>? fileDtos { get; set; }
    }

    public class HrTicketEditDto
    {
        public long Id { get; set; }
        public string? EmpCode { get; set; }

        public long? EmpId { get; set; }
        public int? Way { get; set; }
        public int? Cabins { get; set; }
        public string? TicketDate { get; set; }
        public string? TicketNo { get; set; }
        public decimal? TicketCount { get; set; }
        public decimal? TicketAmount { get; set; }
        public int? IsBillable { get; set; }
        public string? Purpose { get; set; }
        public string? Note { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public decimal? TotalAmount { get; set; }
        public List<SaveFileDto>? fileDtos { get; set; }
    }

    public class HrTicketFilterDto
    {
        public long? Id { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public int? DeptId { get; set; }
        public int? LocationId { get; set; }
        public int? BranchId { get; set; }
        public string? EmpCode { get; set; }
        public string? EmpName { get; set; }
        public string? TicketDate { get; set; }
        public string? TicketNo { get; set; }
        public decimal? TicketCount { get; set; }
        public decimal? TicketAmount { get; set; }
        public int? IsBillable { get; set; }
        public string? billableName { get; set; }

    }

}
