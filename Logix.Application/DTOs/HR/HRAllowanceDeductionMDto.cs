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
    public class HrAllowanceDeductionMDto : TraceEntity
    {
        [Key]
        public long Id { get; set; }
        public long? EmpId { get; set; }
        public int? TypeId { get; set; }
        public int? AdId { get; set; }
        public decimal? Rate { get; set; }
        public decimal? Amount { get; set; }
    
        public string? DecisionNo { get; set; }
       
        public string? DecisionDate { get; set; }
     
        public string? StartDate { get; set; }
       
        public string? EndDate { get; set; }
        [StringLength(1500)]
        public string? Note { get; set; }
        public int? FinancelYear { get; set; }

        public bool? Status { get; set; }
      
        public int? FixedOrTemporary { get; set; }
      
        
        public string? DueDate { get; set; }
     
        public long? PreparationSalariesId { get; set; }


    }
    public class HrAllowanceDeductionMEditDto
    {
        [Key]
        public long Id { get; set; }
        public long? EmpId { get; set; }
        public int? TypeId { get; set; }
        public int? AdId { get; set; }
        public decimal? Rate { get; set; }
        public decimal? Amount { get; set; }

        public string? DecisionNo { get; set; }

        public string? DecisionDate { get; set; }

        public string? StartDate { get; set; }

        public string? EndDate { get; set; }
        [StringLength(1500)]
        public string? Note { get; set; }
        public int? FinancelYear { get; set; }

        public bool? Status { get; set; }

        public int? FixedOrTemporary { get; set; }


        public string? DueDate { get; set; }

        public long? PreparationSalariesId { get; set; }

        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
