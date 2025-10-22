namespace Logix.Application.DTOs.HR
{
    public class HrPsAllowanceDeductionDto
    {
        public long Id { get; set; }
        public long? PsId { get; set; }
        public long? EmpId { get; set; }
        public int? TypeId { get; set; }
        public decimal? Amount { get; set; }
        public decimal? Debit { get; set; }
        public decimal? Credit { get; set; }
        public decimal? AmountOrginal { get; set; }
        public int? AdId { get; set; }
        public int? FixedOrTemporary { get; set; }
        public long CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }

    }
    public class HrPsAllowanceDeductionEditDto
    {
        public long Id { get; set; }
        public long? PsId { get; set; }
        public long? EmpId { get; set; }
        public int? TypeId { get; set; }
        public decimal? Amount { get; set; }
        public decimal? Debit { get; set; }
        public decimal? Credit { get; set; }
        public decimal? AmountOrginal { get; set; }
        public int? AdId { get; set; }
        public int? FixedOrTemporary { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }

}
