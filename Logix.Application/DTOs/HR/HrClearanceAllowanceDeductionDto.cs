namespace Logix.Application.DTOs.HR
{
    public class HrClearanceAllowanceDeductionDto
    {
        public long Id { get; set; }
        public long? ClearanceId { get; set; }
        public long? EmpId { get; set; }
        public int? TypeId { get; set; }
        public int? AdId { get; set; }
        public decimal? Rate { get; set; }
        public decimal? Amount { get; set; }
        public decimal? NewAmount { get; set; }
        public int? FixedOrTemporary { get; set; }
        public bool IsDeleted { get; set; }
    }
}
