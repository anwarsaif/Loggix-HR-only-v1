using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Table("HR_Policies")]
    public partial class HrPolicy
    {
        [Key]
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
        public long CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}
