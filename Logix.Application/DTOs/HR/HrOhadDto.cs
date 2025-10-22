using Logix.Application.DTOs.Main;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Logix.Application.DTOs.HR
{
    public class HrOhadDto
    {
        public long OhdaId { get; set; }
        public string? EmpCode { get; set; }
        public string? EmpName { get; set; }
        public string? OhdaDate { get; set; }
        public bool? IsDeleted { get; set; }
        public long? Code { get; set; }
        public int? TransTypeId { get; set; }
        public long? FromEmpId { get; set; }
        public string? Note { get; set; }
        public long? RefranceId { get; set; }

        public string? RefranceCode { get; set; }
        public long? EmpIdTo { get; set; }
        public string? EmpCodeTo { get; set; }
        public long? EmpIdRecipient { get; set; }
        public List<HrOhadDetailDto> OhadDetails { get; set; }
        public List<SaveFileDto> fileDtos { get; set; }

    }

    public class HrOhadFilterDto
    {
        public string? EmpCode { get; set; }
        public long? OhadId { get; set; }
        public long? DeptId { get; set; }
        public int? BranchId { get; set; }
        public long? Location { get; set; }
        public string? EmpName { get; set; }
        public string? EmpName2 { get; set; }
        public string? CustodyReturnDate { get; set; }
        public string? OhadDate { get; set; }

        public string? Note { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public string? EmpIdTo { get; set; }
        public string? EmpNameTo { get; set; }
        public int? TransTypeId { get; set; }
        public string? TransTypeName { get; set; }

    }


    public class HrOhadEditDto
    {
        public long OhdaId { get; set; }
        public string? EmpCode { get; set; }
        public string? EmpName { get; set; }
        public string? OhdaDate { get; set; }
        public bool? IsDeleted { get; set; }
        public long? Code { get; set; }
        [Column("Trans_Type_ID")]
        public int? TransTypeId { get; set; }
        public long? FromEmpId { get; set; }
        public string? Note { get; set; }
        public long? RefranceId { get; set; }
        public string? RefranceCode { get; set; }
        public long? EmpIdTo { get; set; }
        public long? EmpIdRecipient { get; set; }
        public string? EmpCodeTo { get; set; }
        public string? EmpNameTo { get; set; }
        public List<HrOhadDetailDto> OhadDetails { get; set; }
        public List<SaveFileDto>? fileDtos { get; set; }

    }


}