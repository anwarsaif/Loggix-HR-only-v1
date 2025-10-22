using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Table("HR_Expenses_Type", Schema = "dbo")]
    public partial class HrExpensesType
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [StringLength(50)]
        public string? Name { get; set; }
        [StringLength(50)]
        public string? Name2 { get; set; }
        [Column("Account_Exp_ID")]
        public long? AccountExpId { get; set; }
        [Column("Account_Due_ID")]
        public long? AccountDueId { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Amount { get; set; }
        [Column("VAT_Rate", TypeName = "decimal(18, 2)")]
        public decimal? VatRate { get; set; }
        public long CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public bool? NeedSchedul { get; set; }
        [Column("App_Type_IDs")]
        [StringLength(250)]
        public string? AppTypeIds { get; set; }
        [Column("Account_Paid_Advance_ID")]
        public long? AccountPaidAdvanceId { get; set; }
        [Column("Facility_ID")]
        public long FacilityId { get; set; }
    }
}
