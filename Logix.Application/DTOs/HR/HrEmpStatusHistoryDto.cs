namespace Logix.Application.DTOs.HR
{
    public class HrEmpStatusHistoryDto
    {
        public long Id { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool? IsDeleted { get; set; }
        public int? StatusId { get; set; }
        public int? EmpId { get; set; }
        public string? Note { get; set; }
        public int? StatusIdOld { get; set; }
    }

    public class HrEmpStatusHistoryEditDto
    {
        public long Id { get; set; }

        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? StatusId { get; set; }
        public int? EmpId { get; set; }
        public string? Note { get; set; }
        public int? StatusIdOld { get; set; }
    }
}
