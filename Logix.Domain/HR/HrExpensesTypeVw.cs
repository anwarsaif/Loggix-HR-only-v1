using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrExpensesTypeVw
    {
        [Column("ID")]
        public long Id { get; set; }
        [StringLength(50)]
        public string? Name { get; set; }
        [StringLength(50)]
        public string? Name2 { get; set; }
        [Column("Facility_ID")]
        public long FacilityId { get; set; }
        [Column("Exp_Account_ID")]
        public long? ExpAccountId { get; set; }
        [Column("Exp_Account_Code")]
        [StringLength(50)]
        public string? ExpAccountCode { get; set; }
        [Column("Exp_Account_Name")]
        [StringLength(258)]
        public string? ExpAccountName { get; set; }
        [Column("Exp_Account_Name2")]
        [StringLength(255)]
        public string? ExpAccountName2 { get; set; }
        [Column("Due_Account_ID")]
        public long? DueAccountId { get; set; }
        [Column("Due_Account_Code")]
        [StringLength(50)]
        public string? DueAccountCode { get; set; }
        [Column("Due_Account_Name")]
        [StringLength(258)]
        public string? DueAccountName { get; set; }
        [Column("Due_Account_Name2")]
        [StringLength(255)]
        public string? DueAccountName2 { get; set; }
        [Column("Exp_Acc_Account_Code")]
        [StringLength(50)]
        public string? ExpAccAccountCode { get; set; }
        [Column("Exp_CC_ID")]
        public long? ExpCcId { get; set; }
        [Column("Exp_Acc_group_ID")]
        public long? ExpAccGroupId { get; set; }
        [Column("Due_Acc_Account_Code")]
        [StringLength(50)]
        public string? DueAccAccountCode { get; set; }
        [Column("Due_CC_ID")]
        public long? DueCcId { get; set; }
        [Column("Due_Acc_group_ID")]
        public long? DueAccGroupId { get; set; }
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
        [Column("Paid_Advance_Name")]
        [StringLength(255)]
        public string? PaidAdvanceName { get; set; }
        [Column("Paid_Advance_Name2")]
        [StringLength(255)]
        public string? PaidAdvanceName2 { get; set; }
        [Column("Paid_Advance_Code")]
        [StringLength(50)]
        public string? PaidAdvanceCode { get; set; }
    }
}
