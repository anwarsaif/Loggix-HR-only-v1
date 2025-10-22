using Logix.Application.DTOs.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Application.DTOs.HR
{
    public class HrInsurancePolicyDto
    {
        public long? Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
  
        public string? StartDate { get; set; }
      
        public string? EndDate { get; set; }
        public long? CompanyId { get; set; }
        public long? FacilityId { get; set; }
        public string? Note { get; set; } 
        public bool IsDeleted { get; set; }
        public List<SaveFileDto> fileDtos { get; set; }

    }
    public class HrInsurancePolicyEditDto
    {
        public long Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
  
        public string? StartDate { get; set; }
      
        public string? EndDate { get; set; }
        public long? CompanyId { get; set; }
        public long? FacilityId { get; set; }
        public string? Note { get; set; }
        public List<SaveFileDto> fileDtos { get; set; }

    }
    public class HrInsurancefilterPolicyDto
    {
        public long? Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }

        public string? StartDate { get; set; }

        public string? EndDate { get; set; }
        public string? Note { get; set; }
       
    }

}
