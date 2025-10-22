using System;
using System.Collections.Generic;

namespace Logix.Application.DTOs.HR
{
    public  class HrSalaryGroupRefranceDto
    {
        public long Id { get; set; }
        public long? GroupId { get; set; }
        public int? ReferenceTypeId { get; set; }
        public long? AccountId { get; set; }
        public string? AccountCode { get; set; }

        public bool? IsDeleted { get; set; }
    }
    public class HrSalaryGroupRefranceEditDto
    {
        public long Id { get; set; }
        public long? GroupId { get; set; }
        public int? ReferenceTypeId { get; set; }
        public long? AccountId { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
