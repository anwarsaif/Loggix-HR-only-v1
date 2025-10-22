namespace Logix.Application.DTOs.HR
{
    public class HrExpensesScheduleDto
    {
        public long? Id { get; set; }
        public long? ExpenseId { get; set; }
        public long? SettlementScheduleId { get; set; }

        public bool IsDeleted { get; set; }
    }
    public class HrExpensesScheduleEditDto
    {
        public long Id { get; set; }
        public long? ExpenseId { get; set; }
        public long? SettlementScheduleId { get; set; }

    }

}
