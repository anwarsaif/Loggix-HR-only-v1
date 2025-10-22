using Castle.MicroKernel.SubSystems.Conversion;
using Logix.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Application.DTOs.HR
{
    public class HrCostTypeDto
    {
        public long? Id { get; set; }
        [StringLength(500)]
        public string? TypeName { get; set; }
        [StringLength(500)]
        public string? TypeNameEn { get; set; }
        public int? TypeId { get; set; }
        public int? TypeNationality { get; set; }
        public int? TypeCalculation { get; set; }
        public decimal? CalculationValue { get; set; }
        public decimal? CalculationRate { get; set; }
        public int? RateType { get; set; }
        public bool? SalaryBasic { get; set; }
        public string? Allowance { get; set; }
        public string? Deduction { get; set; }

        public bool? IsDeleted { get; set; }
        public bool? ChkActive { get; set; }
        public string? Description{ get; set; }

    }
    public class HrCostTypeEditDto
    {
        public long Id { get; set; }
        [StringLength(500)]
        public string? TypeName { get; set; }
        [StringLength(500)]
        public string? TypeNameEn { get; set; }
        public int? TypeId { get; set; }
        public int? TypeNationality { get; set; }
        public int? TypeCalculation { get; set; }
        public decimal? CalculationValue { get; set; }
        public decimal? CalculationRate { get; set; }


        public int? RateType { get; set; }
        public bool? SalaryBasic { get; set; }
        public string? Allowance { get; set; }
        public string? Deduction { get; set; }

    }
    public class HrCostTypeFilterDto
    {
        public string? TypeName { get; set; }
        public string? TypeNameEn { get; set; }
        //نوع المصروف
        public int? TypeId { get; set; }
        //طريقة الاحتساب
        public int? TypeCalculation { get; set; }
        //////////////////////////////////////////////////////
        public string? ExpenseName { get; set; }
        public long Id { get; set; }
        public decimal? CalculationValue { get; set; }
        public decimal? CalculationRate { get; set; }
        public string? TypeCalculationName { get; set; }
        public string? ExpenseName2 { get; set; }
        public string? TypeCalculationName2 { get; set; }

    }
}
