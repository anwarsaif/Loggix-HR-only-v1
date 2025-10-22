namespace Logix.Application.DTOs.HR
{
    public class HrOpeningBalanceDto
    {
        public long Id { get; set; }
        public string? StartDate { get; set; }
        public long? TypeId { get; set; }
        public decimal? ObValue { get; set; }
        public string EmpCode { get; set; } = null!;
        public string? EmpName { get; set; }
    }

    public class HrOpeningBalanceEditDto
    {
        public long Id { get; set; }
        public string? StartDate { get; set; }
        public long? TypeId { get; set; }
        public decimal? ObValue { get; set; }
        public string EmpCode { get; set; } = null!;
        //public string? EmpName { get; set; } = null!;
    }

    public class HrOpeningBalanceFilterDto
    {
        public string? TypeName { get; set; }
        public string? TypeName2 { get; set; }
        public string? EmpCode { get; set; }
        public string? EmpName { get; set; }
        public int? BranchId { get; set; }
        public long Id { get; set; }
        public long? EmpId { get; set; }
        public string? StartDate { get; set; }
        public long? TypeId { get; set; }
        public string? EmpName2 { get; set; }
        public decimal? ObValue { get; set; }
    }

    public class OtherBalanceDto
    {
        public decimal? BalanceInYear { get; set; }
        public decimal? OpBalance { get; set; }
        public decimal? CurBalance { get; set; }
        public string? StartDate { get; set; }
        public decimal? BalanceDays { get; set; }
        public decimal? BalanceUsed { get; set; }
        public long? EmpId { get; set; }
        public string? EmpCode { get; set; }
        public string? EmpName { get; set; }
        public string? CurrDate { get; set; }
        public string? EmpName2 { get; set; }
    }

    public class CurrentBalanceFilterDto
    {
        public string? EmpCode { get; set; }
        public int TypeId { get; set; }
        public string? CurrDate { get; set; }
    }
}

