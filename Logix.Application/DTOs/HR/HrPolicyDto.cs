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
    public class HrPolicyDto 
    {
        public long Id { get; set; }
        
        public long? FacilityId { get; set; }
      
        public long? PolicieId { get; set; }
      
        public int? RateType { get; set; }
        public bool? Salary { get; set; }
        public string? Allawance { get; set; }
        public string? Deductions { get; set; }
        [Column("Salary_Rate", TypeName = "decimal(18, 2)")]
        public decimal? SalaryRate { get; set; }

        [Column("Total_Rate", TypeName = "decimal(18, 2)")]
        public decimal? TotalRate { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool? IsDeleted { get; set; }

    } 
    public class HrPolicyEditDto
    {
        public long Id { get; set; }

        public long? FacilityId { get; set; }

        public long? PolicieId { get; set; }

        public int? RateType { get; set; }
        public bool? Salary { get; set; }
        public string? Allawance { get; set; }
        public string? Deductions { get; set; }
        [Column("Salary_Rate", TypeName = "decimal(18, 2)")]
        public decimal? SalaryRate { get; set; }

        [Column("Total_Rate", TypeName = "decimal(18, 2)")]
        public decimal? TotalRate { get; set; }
        [StringLength(250)]
        public string? TypeName { get; set; }
        [StringLength(250)]
        public string? TypeName2 { get; set; }

        public long? ModifiedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }
       
    }
}
