using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrLoanInstallmentVw
    {
        [Column("ID")]
        public long Id { get; set; }
        [Column("Loan_ID")]
        public long? LoanId { get; set; }
        [Column("Intallment_No")]
        public int? IntallmentNo { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Amount { get; set; }
        [Column("Due_Date")]
        [StringLength(10)]
        public string? DueDate { get; set; }
        public bool? IsDeleted { get; set; }
        [Column("Emp_name")]
        [StringLength(250)]
        public string? EmpName { get; set; }
        [Column("Emp_ID")]
        [StringLength(50)]
        public string? EmpId { get; set; }
        [Column("Emp_Code")]
        [StringLength(50)]
        public string EmpCode { get; set; } = null!;
        public bool? IsPaid { get; set; }
        [Column("Recepit_No")]
        [StringLength(50)]
        public string? RecepitNo { get; set; }
        [Column("Recepit_Date")]
        [StringLength(10)]
        public string? RecepitDate { get; set; }
        public bool? IsDeletedM { get; set; }
        [Column("End_Date_Payment")]
        [StringLength(50)]
        [Unicode(false)]
        public string? EndDatePayment { get; set; }
        [Column("Loan_Date")]
        [StringLength(10)]
        [Unicode(false)]
        public string? LoanDate { get; set; }
        [Column("Loan_Value", TypeName = "decimal(18, 2)")]
        public decimal? LoanValue { get; set; }
        [Column("BRANCH_ID")]
        public int? BranchId { get; set; }
        [Column("BRA_NAME")]
        public string? BraName { get; set; }
    }
}
