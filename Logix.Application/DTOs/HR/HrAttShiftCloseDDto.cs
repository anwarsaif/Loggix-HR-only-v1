using Logix.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Application.DTOs.HR
{
    public class HrAttShiftCloseDDto: TraceEntity
    {
        public long? Id { get; set; }

        public long? CId { get; set; }

        public int? TypeId { get; set; }
        public int? Cnt { get; set; }


      
    }  public class HrAttShiftCloseDEditDto
    {
        public long? Id { get; set; }

        public long? CId { get; set; }

        public int? TypeId { get; set; }
        public int? Cnt { get; set; }


        public long? ModifiedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
