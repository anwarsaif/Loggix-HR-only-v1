using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Logix.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Table("HR_Loan")]
    [Index("LoanDate", "InstallmentValue", "StartDatePayment", "EndDatePayment", "EmpId", Name = "Ind_Emp_ID")]
    [Index("IsDeleted", Name = "Ind_Isdel")]
    public partial class HrLoan
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }

        [Column("Loan_Date")]
        [StringLength(10)]
        [Unicode(false)]
        public string? LoanDate { get; set; }

        [Column("Loan_Value", TypeName = "decimal(18, 2)")]
        public decimal? LoanValue { get; set; }

        [Column("Installment_Value", TypeName = "decimal(18, 2)")]
        public decimal? InstallmentValue { get; set; }

        [Column("Installment_Count")]
        public int? InstallmentCount { get; set; }

        [Column("Installment_Count_paid")]
        public int? InstallmentCountPaid { get; set; }

        [Column("Installment_Count_Remaining")]
        public int? InstallmentCountRemaining { get; set; }

        [Column("Installment_Last_Value", TypeName = "decimal(18, 2)")]
        public decimal? InstallmentLastValue { get; set; }

        [Column("Start_Date_Payment")]
        [StringLength(10)]
        [Unicode(false)]
        public string? StartDatePayment { get; set; }

        [Column("End_Date_Payment")]
        [StringLength(50)]
        [Unicode(false)]
        public string? EndDatePayment { get; set; }

        public bool? IsActive { get; set; }

        [Column("Emp_ID")]
        [StringLength(50)]
        public string? EmpId { get; set; }

        public long? CreatedBy { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }

        public long? ModifiedBy { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }

        public bool? IsDeleted { get; set; }

        public string? Note { get; set; }

        public int? Type { get; set; }

        [Column("Create_Installment")]
        public bool? CreateInstallment { get; set; }

        [Column("Guarantor1_Emp_ID")]
        public long? Guarantor1EmpId { get; set; }
    }
}
