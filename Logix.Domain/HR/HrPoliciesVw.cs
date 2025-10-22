using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrPoliciesVw
    {
        [Column("ID")]
        public long Id { get; set; }
        [Column("Facility_ID")]
        public long? FacilityId { get; set; }
        [Column("Policie_ID")]
        public long? PolicieId { get; set; }
        [Column("Rate_Type")]
        public int? RateType { get; set; }
        public bool? Salary { get; set; }
        public string? Allawance { get; set; }
        public string? Deductions { get; set; }
        [Column("Salary_Rate", TypeName = "decimal(18, 2)")]
        public decimal? SalaryRate { get; set; }
        [Column("Total_Rate", TypeName = "decimal(18, 2)")]
        public decimal? TotalRate { get; set; }
        [Column("Type_Name")]
        [StringLength(250)]
        public string? TypeName { get; set; }
        [Column("Type_Name2")]
        [StringLength(250)]
        public string? TypeName2 { get; set; }
        public bool IsDeleted { get; set; }
    }
}
