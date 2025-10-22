using Logix.Application.DTOs.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Application.DTOs.HR
{
    public class HrInsuranceDto
    {
      
        public long? Id { get; set; }
        public long? Code { get; set; }
      
        public int? TransTypeId { get; set; }
       
        public int? InsuranceType { get; set; }
       
        public long? PolicyId { get; set; }
       
        public string? InsuranceDate { get; set; }
      
        public decimal? Total { get; set; }
        public string? Note { get; set; }
      
        public int? AppId { get; set; }
       
        public int? WfStatusId { get; set; }

        public List<HrInsuranceEmpVM> InsuranceEmp { get; set; }
        public List<SaveFileDto> fileDtos { get; set; }


        public bool IsDeleted { get; set; }
    } 
    public class HrInsuranceEditDto
    {
      
        public long Id { get; set; }
        public long? Code { get; set; }
      
        public int? TransTypeId { get; set; }
       
        public int? InsuranceType { get; set; }
       
        public long? PolicyId { get; set; }
       
        public string? InsuranceDate { get; set; }
      
        public decimal? Total { get; set; }
        public string? Note { get; set; }
      
        public long? AppId { get; set; }
       
        public int? WfStatusId { get; set; }
        public List<HrInsuranceEmpVM> InsuranceEmp { get; set; }
        public List<SaveFileDto> fileDtos { get; set; }


    }
    public class HrInsuranceFilterDto
    {

        public long? Id { get; set; }
        public long? Code { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public string? PolicyName { get; set; }
        public string? InsuranceDate { get; set; }
        public decimal? Total { get; set; }
        public string? Note { get; set; }      
    }

    public class HrInsuranceEmpVM
    {
        public long? Id { get; set; }
        public long? InsuranceId { get; set; }
        public long? EmpId { get; set; }
        public string? EmpCode { get; set; }
        public string? EmpName { get; set; }
        public bool? ToDependents { get; set; } // المومن منه
        public string? ToDependentsName { get; set; } // المومن منه
        public long? DependentId { get; set; } //التابع
        public string? DependentName { get; set; }  //التابع
        public int? ClassId { get; set; }
        public string? ClassName { get; set; }
        public string? InsuranceCardNo { get; set; }
        public decimal? Amount { get; set; }
        public string? Note { get; set; }
        public long? RefranceInsEmpId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
