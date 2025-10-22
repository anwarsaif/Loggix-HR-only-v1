using Logix.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Application.DTOs.HR
{
    public class HrDecisionsEmployeeDto 
    {
        public long Id { get; set; }
        public long? DecisionsId { get; set; }
        public long? EmpId { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool? IsDeleted { get; set; }

    }
    public class HrDecisionsEmployeeEditDto
    {
        public long Id { get; set; }
        public long? DecisionsId { get; set; }
        public long? EmpId { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

    }
}
