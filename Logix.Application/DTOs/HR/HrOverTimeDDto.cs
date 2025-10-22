using Castle.MicroKernel.SubSystems.Conversion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Application.DTOs.HR
{
    public class HrOverTimeDDto
    {
        
        public long? Id { get; set; }
       
        public long? IdM { get; set; }
        
        public int? OverTimeTybe { get; set; }
       
        public decimal? OverTimeHCost { get; set; }
       
        public decimal? Hours { get; set; }
       
        public decimal? Amount { get; set; }
        public decimal? Total { get; set; }
        public int? CurrencyId { get; set; }
        public string? Description { get; set; }
        public bool? IsDeleted { get; set; }
        public string? OverTimeDate { get; set; }
        public string? EmpCode { get; set; }
        public string? RefranceId { get; set; }

    }

    public class HrOverTimeDEditDto
    {

        public long Id { get; set; }

        public long? IdM { get; set; }

        public int? OverTimeTybe { get; set; }

        public decimal? OverTimeHCost { get; set; }

        public decimal? Hours { get; set; }

        public decimal? Amount { get; set; }
        public decimal? Total { get; set; }
        public int? CurrencyId { get; set; }
        public string? Description { get; set; }
        public string? OverTimeDate { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
