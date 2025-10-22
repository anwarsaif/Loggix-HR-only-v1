namespace Logix.Application.DTOs.HR
{
    public  class HrExpensesEmployeeDto
    {
        public long ?Id { get; set; }
        public long? ExpenseId { get; set; }
        public long? EmpId { get; set; }
        public string? EmpCode { get; set; }
        public int? ExpenseTypeId { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? VatRate { get; set; }
        public decimal? VatAmount { get; set; }
        public decimal? Total { get; set; }
        public int? PaidBy { get; set; }
        public string? InvCode { get; set; }
        public string? PaymentDate { get; set; }
        public bool IsDeleted { get; set; }
        public string? Note { get; set; }
    }

    public class HrExpensesEmployeeEditDto
    {
        public long Id { get; set; }
        public long? ExpenseId { get; set; }
        public long? EmpId { get; set; }
        public string? EmpCode { get; set; }
        public int? ExpenseTypeId { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? VatRate { get; set; }
        public decimal? VatAmount { get; set; }
        public decimal? Total { get; set; }
        public int? PaidBy { get; set; }
        public string? InvCode { get; set; }
        public string? PaymentDate { get; set; }
        public string? Note { get; set; }
    }


    public class HrExpensesEmployeeAddDto
    {
        public long? Id { get; set; }
        public long? ExpenseId { get; set; }
        public long? EmpId { get; set; }
        public string? EmpCode { get; set; }
        public int? ExpenseTypeId { get; set; }
        public decimal? Amount { get; set; }
        public decimal? VatRate { get; set; }
        public decimal? VatAmount { get; set; }
        public decimal? Total { get; set; }
        public int? PaidBy { get; set; }
        public string? InvCode { get; set; }
        public string? PaymentDate { get; set; }
        public bool IsDeleted { get; set; }
        public string? Note { get; set; }
    }

}
