using Logix.Application.DTOs.Main;

using Logix.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Application.DTOs.HR
{
    public class HrDecisionDto
    {
        public long Id { get; set; }
        [StringLength(10)]
        public string? DecDate { get; set; }
        public int? DecType { get; set; }
        public long? DecCode { get; set; }
        public long? EmpId { get; set; }
        public string? Note { get; set; }
        [StringLength(50)]
        public string? RefranceCode { get; set; }
        [StringLength(10)]
        public string? RefranceDate { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool? IsDeleted { get; set; }

        public long? AppId { get; set; }
        public string? FileUrl { get; set; }
        public bool? DecisionSigning { get; set; }
        public List<EmpInfo> EmpInfo { get; set; }
        public int? AppTypeID { get; set; }


    }
    public class HrDecisionEditDto
    {
        public long Id { get; set; }
        [StringLength(10)]
        public string? DecDate { get; set; }
        public int? DecType { get; set; }
        public long? DecCode { get; set; }
        public long? EmpId { get; set; }
        public string? Note { get; set; }
        public string? RefranceCode { get; set; }
        public string? RefranceDate { get; set; }

        public long? AppId { get; set; }
        public string? FileUrl { get; set; }
        public bool? DecisionSigning { get; set; }
        public List<EmpInfo> EmpInfo { get; set; }
        //public List<SaveFileDto>? fileDtos { get; set; }


    }


    public class HrDecisionFilterDto
    {
        public long? Id { get; set; }

        public string? EmpCode { get; set; }
        public string? EmpName { get; set; }
        public int? DecisionType { get; set; }

        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public int? Location { get; set; }
        public int? DeptId { get; set; }
        public int? BranchId { get; set; }
        //////////////////////
        public long? DecCode { get; set; }
        public string? DecisionDate { get; set; }
        public string? DecisionTypeName { get; set; }



    }

    public class EmpInfo
    {

        public long? Id { get; set; } 
        public string EmpCode { get; set; } = null!;
        public string? EmpName { get; set; }
        public bool? IsDeleted { get; set; }

    }
    public class DecisionTypeChangedDto
    {
        public long? TypeId { get; set; }
        public string? ApplicationDate { get; set; }
        public long? ApplicationCode { get; set; }


    }
    public class HrDecisionsDashboardFilterDto
    {
        public int? BranchId { get; set; }
        public int? DeptId { get; set; }
        public int? Location { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public int? DecType { get; set; }
        /////////////////////////

        public int? Cnt { get; set; }
        public string? DecTypeName { get; set; }
        public string? DecTypeName2 { get; set; }
        public string? Icon { get; set; }
        public string? Url { get; set; }
        public string? Color { get; set; }
        public string? DepName { get; set; }
        public string? DepName2 { get; set; }
        public string? BraName { get; set; }
        public string? BraName2 { get; set; }
        public string? LocationName { get; set; }
        public string? LocationName2 { get; set; }

    }
}
