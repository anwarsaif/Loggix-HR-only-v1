namespace Logix.Application.DTOs.HR
{
    public partial class HrExpensesTypeDto
    {
        public long? Id { get; set; }
        public string? Name { get; set; }
        public string? Name2 { get; set; }
        public long? AccountExpId { get; set; }
        public string? AccountExpCode{ get; set; }
        public long? AccountDueId { get; set; }
        public string? AccountDueCode { get; set; }
        public decimal? Amount { get; set; }
        public decimal? VatRate { get; set; }

        public bool IsDeleted { get; set; }
        public bool? NeedSchedul { get; set; }
        public string? AppTypeIds { get; set; }
        public long? AccountPaidAdvanceId { get; set; }
        public string? AccountPaidAdvanceCode { get; set; }
        public long? FacilityId { get; set; }
    }

    public partial class HrExpensesTypeEditDto
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Name2 { get; set; }
        public long? AccountExpId { get; set; }
        public long? AccountDueId { get; set; }
        public decimal? Amount { get; set; }
        public decimal? VatRate { get; set; }

        public bool IsDeleted { get; set; }
        public bool? NeedSchedul { get; set; }
        public string? AppTypeIds { get; set; }
        public long? AccountPaidAdvanceId { get; set; }
        public long? FacilityId { get; set; }
        public string? AccountExpCode { get; set; }

        public string? AccountDueCode { get; set; }
        public string? AccountPaidAdvanceCode { get; set; }

    }
    public partial class HrExpensesTypeFilterDto
    {
        public string? Name { get; set; }

    }

}
