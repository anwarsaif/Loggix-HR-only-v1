namespace Logix.Application.DTOs.HR
{
    public class HrExpensesPaymentDto
    {
        public long? Id { get; set; }
        public long? JId { get; set; }
        public long? ExpenseId { get; set; }
        public decimal? Amount { get; set; }
        public int? PaymentId { get; set; }

        public bool IsDeleted { get; set; }
    }
    public class HrExpensesPaymentEditDto
    {
        public long Id { get; set; }
        public long? JId { get; set; }
        public long? ExpenseId { get; set; }
        public decimal? Amount { get; set; }
        public int? PaymentId { get; set; }

    }

}
