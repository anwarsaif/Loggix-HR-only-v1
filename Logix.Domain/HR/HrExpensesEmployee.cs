using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Table("HR_Expenses_Employees", Schema = "dbo")]
    public partial class HrExpensesEmployee
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Column("Expense_ID")]
        public long? ExpenseId { get; set; }
        [Column("Emp_ID")]
        public long? EmpId { get; set; }
        [Column("Expense_Type_ID")]
        public int? ExpenseTypeId { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? SubTotal { get; set; }
        [Column("VAT_Rate", TypeName = "decimal(18, 2)")]
        public decimal? VatRate { get; set; }
        [Column("VAT_Amount", TypeName = "decimal(18, 2)")]
        public decimal? VatAmount { get; set; }
        [Column("total", TypeName = "decimal(18, 2)")]
        public decimal? Total { get; set; }
        public int? PaidBy { get; set; }
        [Column("Inv_Code")]
        [StringLength(50)]
        public string? InvCode { get; set; }
        [Column("Payment_Date")]
        [StringLength(10)]
        public string? PaymentDate { get; set; }
        public long CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        [StringLength(2500)]
        public string? Note { get; set; }
    }
}
