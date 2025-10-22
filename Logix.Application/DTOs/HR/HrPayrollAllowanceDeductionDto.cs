namespace Logix.Application.DTOs.HR
{
    public class HrPayrollAllowanceDeductionDto
    {
        public long Id { get; set; }
        public long? MsId { get; set; }
        public long? MsdId { get; set; }
        public long? AdId { get; set; }
        public decimal? AdValue { get; set; }
        public decimal? Debit { get; set; }
        public decimal? Credit { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsDeleted { get; set; }
        public decimal? AdValueOrignal { get; set; }
        public long? EmpId { get; set; }
        public int? FixedOrTemporary { get; set; }
    }
    public class HrPayrollAllowanceDeductionEditDto
    {
        public long Id { get; set; }
        public long? MsId { get; set; }
        public long? MsdId { get; set; }
        public long? AdId { get; set; }
        public decimal? AdValue { get; set; }
        public decimal? Debit { get; set; }
        public decimal? Credit { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public decimal? AdValueOrignal { get; set; }
        public long? EmpId { get; set; }
        public int? FixedOrTemporary { get; set; }
    }


}
