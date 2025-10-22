using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Logix.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Table("HR_Expenses_Schedule")]
    public partial class HrExpensesSchedule:TraceEntity
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Column("Expense_ID")]
        public long? ExpenseId { get; set; }
        [Column("Settlement_Schedule_ID")]
        public long? SettlementScheduleId { get; set; }
       
    }
}
