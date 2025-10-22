using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Application.DTOs.HR
{
    public class InvestMonthDto
    {
      
        public string? MonthCode { get; set; }
        
        public string? MonthName { get; set; }
    
        public int MonthId { get; set; }
      
        public int? DaysOfMonth { get; set; }
    }
}
