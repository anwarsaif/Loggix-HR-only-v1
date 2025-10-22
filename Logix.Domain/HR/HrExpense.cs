using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Table("HR_Expenses", Schema = "dbo")]
    public partial class HrExpense
    {
        [Key]
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
        public long CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        [Column("App_ID")]
        public long? AppId { get; set; }
        [Column("Status_ID")]
        public int? StatusId { get; set; }
        public string? Note { get; set; }
        [Column("Emp_ID")]
        public long? EmpId { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? SubTotal { get; set; }
        [Column("VAT_Amount", TypeName = "decimal(18, 2)")]
        public decimal? VatAmount { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Total { get; set; }
        [Column("Facility_ID")]
        public int? FacilityId { get; set; }
    }
}
