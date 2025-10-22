using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrSalaryGroupDeductionVw
    {
        [Column("ID")]
        public long Id { get; set; }
        [Column("AD_ID")]
        public int? AdId { get; set; }
        [Column("Type_ID")]
        public int? TypeId { get; set; }
        [Column("Group_ID")]
        public long? GroupId { get; set; }
        public bool? IsDeleted { get; set; }
        [Column("Allowance_Name")]
        [StringLength(250)]
        public string? AllowanceName { get; set; }
        [Column("Account_Due_ID")]
        public long? AccountDueId { get; set; }
        [Column("Account_Due_Code")]
        [StringLength(50)]
        public string? AccountDueCode { get; set; }
        [Column("Account_Due_Name")]
        [StringLength(255)]
        public string? AccountDueName { get; set; }
        [Column("Account_Exp_ID")]
        public long? AccountExpId { get; set; }
        [Column("Account_Exp_Code")]
        [StringLength(50)]
        public string? AccountExpCode { get; set; }
        [Column("Account_Exp_Name")]
        [StringLength(255)]
        public string? AccountExpName { get; set; }
    }
}
