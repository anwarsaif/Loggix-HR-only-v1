using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Table("HR_GOSI_Employee", Schema = "dbo")]
    public partial class HrGosiEmployee
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Column("Gosi_ID")]
        public long? GosiId { get; set; }
        [Column("Emp_ID")]
        public long? EmpId { get; set; }
        [Column("Basic_Salary", TypeName = "decimal(18, 2)")]
        public decimal? BasicSalary { get; set; }
        [Column("Housing_Allowance", TypeName = "decimal(18, 2)")]
        public decimal? HousingAllowance { get; set; }
        [Column("Other_Allowance", TypeName = "decimal(18, 2)")]
        public decimal? OtherAllowance { get; set; }
        [Column("Total_Salary", TypeName = "decimal(18, 2)")]
        public decimal? TotalSalary { get; set; }
        [Column("Gosi_Emp", TypeName = "decimal(18, 2)")]
        public decimal? GosiEmp { get; set; }
        [Column("Gosi_Company", TypeName = "decimal(18, 2)")]
        public decimal? GosiCompany { get; set; }
        [Column("Gosi_Rate", TypeName = "decimal(18, 2)")]
        public decimal? GosiRate { get; set; }
        [Column("Gosi_Emp_Rate", TypeName = "decimal(18, 2)")]
        public decimal? GosiEmpRate { get; set; }
        [Column("Gosi_Company_Rate", TypeName = "decimal(18, 2)")]
        public decimal? GosiCompanyRate { get; set; }
        public long? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        [Column("CC_ID")]
        public long? CcId { get; set; }
    }
}
