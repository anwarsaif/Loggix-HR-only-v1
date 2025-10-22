
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Application.DTOs.HR
{
    public class HrAssignmenDto
    {
        public long? Id { get; set; }
        public string? AssignmentDate { get; set; }
        [Required]
        public long? TypeId { get; set; }
        public long? EmpId { get; set; }
        [Required]
        public string? empCode { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public string? Note { get; set; }
        public bool? IsDeleted { get; set; }

    }
    public class HrAssignmenEditDto
    {
        public long Id { get; set; }
        [Required]
        public string? AssignmentDate { get; set; }
        public long? TypeId { get; set; }
        public long? EmpId { get; set; }
        [Required]
        public string? empCode { get; set; }
        [Required]

        public string? FromDate { get; set; }
        [Required]

        public string? ToDate { get; set; }
        public string? Note { get; set; }

    }
    public class HrAssignmenFilterDto
    {
        public long? Id { get; set; }

        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public long? TypeId { get; set; }
        public string? EmpId { get; set; }
        public string? EmpName { get; set; }
        public string? TypeName { get; set; }
        public string? Note { get; set; }
        public int? FacilityId { get; set; }
        public int? BranchId { get; set; }
        public int? LocationId { get; set; }
    }
    public class HrAssignmen2AddDto
    {
        public long Id { get; set; }
        public string? AssignmentDate { get; set; }
        [Required]
        public long? TypeId { get; set; }
        public List<HrAssignmentDescriptionsDto> descriptionsDtos { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }

    }
    public class HrAssignmentDescriptionsDto
    {
        public string? empCode { get; set; }
        public string? Note { get; set; }

    }

}
