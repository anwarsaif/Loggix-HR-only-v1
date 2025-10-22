using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Logix.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Table("HR_Expenses_Payment")]
    public partial class HrExpensesPayment: TraceEntity
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Column("J_ID")]
        public long? JId { get; set; }
        [Column("Expense_ID")]
        public long? ExpenseId { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Amount { get; set; }
        [Column("Payment_ID")]
        public int? PaymentId { get; set; }

    }
}
