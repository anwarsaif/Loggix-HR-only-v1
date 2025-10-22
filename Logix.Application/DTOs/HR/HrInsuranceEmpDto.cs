using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Application.DTOs.HR
{
    public class HrInsuranceEmpDto
    {
     
        public long Id { get; set; }
    
        public long? InsuranceId { get; set; }
      
        public long? EmpId { get; set; }
        
        public bool? ToDependents { get; set; }
      
        public long? DependentId { get; set; }
      
        public decimal? Amount { get; set; }
        public string? Note { get; set; }
      
        public int? ClassId { get; set; }
        public long CreatedBy { get; set; }
      
        public DateTime CreatedOn { get; set; }
     
        public bool IsDeleted { get; set; }
      
        public long? RefranceInsEmpId { get; set; }
     
        public string? InsuranceCardNo { get; set; }
    }
    public class HrInsuranceEmpEditDto
    {
     
        public long Id { get; set; }
    
        public long? InsuranceId { get; set; }
      
        public long? EmpId { get; set; }
        
        public bool? ToDependents { get; set; }
      
        public long? DependentId { get; set; }
      
        public decimal? Amount { get; set; }
        public string? Note { get; set; }
      
        public int? ClassId { get; set; }
        
      
    
        public long? ModifiedBy { get; set; }
      
        public DateTime? ModifiedOn { get; set; }
     
      
        public long? RefranceInsEmpId { get; set; }
     
        public string? InsuranceCardNo { get; set; }
    }
    public class HrInsuranceEmpfilterDto {
        public long Id { get; set; }
        public int? InsuranceType { get; set; }
        public long? PolicyId { get; set; }
        public long? EmpId { get; set; }
        public int? DeptId { get; set; }
        public int? Location { get; set; }
        public int? BranchId { get; set; }
        public string? EmpName { get; set; }
        public long? RefranceInsEmpId { get; set; }
    } 
    public class HrInsuranceEmpfilterRPDto {
        //public long Id { get; set; }

        //public string? InsuranceDate { get; set; }
        //public string? StatusName { get; set; }
        public int? StatusId { get; set; }
        public long? PolicyId { get; set; }
        public int? InsuranceType { get; set; }
        public int? ClassId { get; set; }
        //public string? ClassName { get; set; }
        public long? EmpId { get; set; }
        public string? EmpCode { get; set; }
        public string? EmpName { get; set; }
        public int? BranchId { get; set; }
        public int? Location { get; set; }
        //public string? LocationName { get; set; }

        //public string? DependentName { get; set; }
        public int? DeptId { get; set; }
        //public string? DepName { get; set; }
        //public string? PolicyCode { get; set; }
        //public decimal? Amount { get; set; }
        public string? StartDate { get; set; }

        public string? EndDate { get; set; }
        //public string? CreatedOn { get; set; }


    }
    public class HrInsuranceEmpResulteDto {
        public long Id { get; set; }

        public string? InsuranceDate { get; set; }
        public string? StatusName { get; set; }
        public int? StatusId { get; set; }
        public long? PolicyId { get; set; }
        public int? InsuranceType { get; set; }
        public int? ClassId { get; set; }
        public string? ClassName { get; set; }
        public long? EmpId { get; set; }
        public string EmpCode { get; set; } = null!;
        public string? EmpName { get; set; }
        public int? BranchId { get; set; }
        public int? Location { get; set; }
        public string? LocationName { get; set; }

        public string CreatedOn { get; set; }
        public string? DependentName { get; set; }
        public long? DependentId { get; set; }
        public int? DeptId { get; set; }
        public string? DepName { get; set; }
        public string? PolicyCode { get; set; }
        public string? Note { get; set; }
        public string? InsuranceCardNo { get; set; }
        public decimal? Amount { get; set; }
      

    }


}
