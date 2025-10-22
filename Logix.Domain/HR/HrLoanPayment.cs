using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Table("HR_Loan_Payment")]
    public partial class HrLoanPayment
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Column("Emp_ID")]
        public long? EmpId { get; set; }
        [Column("Pay_Date")]
        [StringLength(10)]
        public string? PayDate { get; set; }
        [Column("Voucher_No")]
        [StringLength(50)]
        public string? VoucherNo { get; set; }
        [Column("Voucher_Date")]
        [StringLength(10)]
        public string? VoucherDate { get; set; }
        [Column("Pay_Amount", TypeName = "decimal(18, 2)")]
        public decimal? PayAmount { get; set; }
        public string? Note { get; set; }
        public long? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}
