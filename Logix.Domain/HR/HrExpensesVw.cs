using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrExpensesVw
    {
        [Column("ID")]
        public long Id { get; set; }
        [StringLength(2500)]
        public string? Title { get; set; }
        public long? No { get; set; }
        [StringLength(50)]
        public string? Code { get; set; }
        [Column("Ex_Date")]
        [StringLength(10)]
        public string? ExDate { get; set; }
        [Column("App_ID")]
        public long? AppId { get; set; }
        [Column("Status_ID")]
        public int? StatusId { get; set; }
        public string? Note { get; set; }
        [Column("Emp_ID")]
        public long? EmpId { get; set; }
        [Column("Emp_Code")]
        [StringLength(50)]
        public string? EmpCode { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? SubTotal { get; set; }
        [Column("VAT_Amount", TypeName = "decimal(18, 2)")]
        public decimal? VatAmount { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Total { get; set; }
        [Column("Facility_ID")]
        public int? FacilityId { get; set; }
        [Column("Emp_name")]
        [StringLength(250)]
        public string? EmpName { get; set; }
        [Column("Emp_name2")]
        [StringLength(250)]
        public string? EmpName2 { get; set; }
        [Column("App_Code")]
        public long? AppCode { get; set; }
        public long CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}
