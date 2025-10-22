namespace Logix.Application.DTOs.HR
{
    public class HrIncrementsAllowanceDeductionDto
    {
        public long Id { get; set; }
        public long? IncrementId { get; set; }
        public int? TypeId { get; set; }
        public int? AdId { get; set; }
        public decimal? Rate { get; set; }
        public decimal? Amount { get; set; }
        public decimal? NewRate { get; set; }
        public decimal? NewAmount { get; set; }
        public long CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool IsDeleted { get; set; }
        public bool? Status { get; set; }
        public long? AllDedId { get; set; }

        public bool IsNew { get; set; }

        public bool? IsUpdated { get; set; }
    }
    public class HrIncrementsAllowanceDeductionEditDto
    {
        public long Id { get; set; }
        public long? IncrementId { get; set; }
        public int? TypeId { get; set; }
        public int? AdId { get; set; }
        public decimal? Rate { get; set; }
        public decimal? Amount { get; set; }
        public decimal? NewRate { get; set; }
        public decimal? NewAmount { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool? Status { get; set; }
        public long? AllDedId { get; set; }

        public bool IsNew { get; set; }

        public bool? IsUpdated { get; set; }
    }
}
