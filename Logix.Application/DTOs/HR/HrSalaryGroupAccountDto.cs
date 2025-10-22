using System;
using System.Collections.Generic;

namespace Logix.Application.DTOs.HR
{
    public  class HrSalaryGroupAccountDto
    {
        public long? Id { get; set; }
        public int? TypeId { get; set; }
        public int? AdId { get; set; }
        public long? AccountDueId { get; set; }
        public string? AccountDueCode { get; set; }
        public string? AccountExpCode { get; set; }
        public long? AccountExpId { get; set; }
        public long? GroupId { get; set; }

        public bool? IsDeleted { get; set; }
        public long? FacilityId { get; set; }

    }

    public class HrSalaryGroupAccountEditDto
    {
        public long Id { get; set; }
        public int? TypeId { get; set; }
        public int? AdId { get; set; }
        public long? AccountDueId { get; set; }
        public long? AccountExpId { get; set; }
        public long? GroupId { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }

    public class CheckSalaryGroupDto
    {
        public bool check { get; set; }
        public string? Message { get; set; }

    }
}
