using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrLoanPaymentVw
    {
        [Column("ID")]
        public long Id { get; set; }
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
        public bool IsDeleted { get; set; }
        [Column("Emp_Code")]
        [StringLength(50)]
        public string EmpCode { get; set; } = null!;
        [Column("Emp_name")]
        [StringLength(250)]
        public string? EmpName { get; set; }
        [Column("BRANCH_ID")]
        public int? BranchId { get; set; }
        [Column("BRA_NAME")]
        public string? BraName { get; set; }
        [Column("Emp_ID")]
        public long? EmpId { get; set; }
        [Column("Dept_ID")]
        public int? DeptId { get; set; }
        public int? Location { get; set; }
        [Column("Dep_Name")]
        [StringLength(200)]
        public string? DepName { get; set; }
        [Column("Location_Name")]
        [StringLength(200)]
        public string? LocationName { get; set; }
        [Column("Location_Name2")]
        [StringLength(200)]
        public string? LocationName2 { get; set; }
        [Column("Dep_Name2")]
        [StringLength(200)]
        public string? DepName2 { get; set; }
        [Column("BRA_NAME2")]
        public string? BraName2 { get; set; }
        [Column("Emp_name2")]
        [StringLength(250)]
        public string? EmpName2 { get; set; }
    }
}
