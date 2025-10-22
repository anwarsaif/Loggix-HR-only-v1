using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Domain.HR
{
    [Keyless]

    public class HrCostTypeVw
    {
        [Column("Expense_Name")]
        [StringLength(250)]
        public string? ExpenseName { get; set; }
        [Column("Catagories_ID")]
        public int? CatagoriesId { get; set; }
        [Column("ID")]
        public long Id { get; set; }
        [Column("Type_Name")]
        [StringLength(500)]
        public string? TypeName { get; set; }
        [Column("Type_NameEn")]
        [StringLength(500)]
        public string? TypeNameEn { get; set; }
        [Column("Type_ID")]
        public int? TypeId { get; set; }
        [Column("Type_Nationality")]
        public int? TypeNationality { get; set; }
        [Column("Type_Calculation")]
        public int? TypeCalculation { get; set; }
        [Column("Calculation_Value", TypeName = "decimal(18, 2)")]
        public decimal? CalculationValue { get; set; }
        [Column("Calculation_Rate", TypeName = "decimal(18, 2)")]
        public decimal? CalculationRate { get; set; }
        public bool? IsDeleted { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public long? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
        [Column("Type_Calculation_Name")]
        [StringLength(250)]
        public string? TypeCalculationName { get; set; }
        public int? Expr2 { get; set; }
        [Column("Expense_Name2")]
        [StringLength(250)]
        public string? ExpenseName2 { get; set; }
        [Column("Type_Calculation_Name2")]
        [StringLength(250)]
        public string? TypeCalculationName2 { get; set; }
        [Column("Rate_Type")]
        public int? RateType { get; set; }
        [Column("Salary_Basic")]
        public bool? SalaryBasic { get; set; }
        public string? Allowance { get; set; }
        public string? Deduction { get; set; }

    }
}
