using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

using Logix.Domain.HR;

namespace Logix.Application.DTOs.HR
{
    public class HrDependentDto
    {
        public long Id { get; set; }

        public long? EmpId { get; set; }
        public string? EmpCode { get; set; }
        public string? IdNo { get; set; }


        public string? Name { get; set; }

        public string? Name1 { get; set; }


        public int? RelationshipId { get; set; }
        public string? DateOfBirth { get; set; }
        public bool? Insurance { get; set; }
        public bool? Ticket { get; set; }
        public int? Gender { get; set; }
        public string? InsuranceNo { get; set; }
        public int? NationalityId { get; set; }
        public int? MaritalStatus { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool? IsDeleted { get; set; }

    }

    public class HrDependentEditDto
    {
        public long Id { get; set; }

        public string? IdNo { get; set; }

        public string? Name { get; set; }

        public string? EmpCode { get; set; }

        public string? Name1 { get; set; }

        public int? RelationshipId { get; set; }
        public string? DateOfBirth { get; set; }
        public bool? Insurance { get; set; }
        public bool? Ticket { get; set; }
        public int? Gender { get; set; }
        public string? InsuranceNo { get; set; }
        public int? NationalityId { get; set; }
        public int? MaritalStatus { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }


    public class HrDependentVM
    {
        public long? DependentId { get; set; }
        public string? EmpId { get; set; }
        public string? EmpName { get; set; }
        public string? Name { get; set; }
        public string? Name2 { get; set; }
        public int? Relationship { get; set; }
        public string? RelationshipName { get; set; }
        public string? DateOfBirth { get; set; }
        public bool? Insurance { get; set; }
        public bool? Ticket { get; set; }
        public int? BranchId { get; set; }
        public int? Age { get; set; }
    }
    public class HrDependentVw : HrDependentsVw
    {
        public int? Relationship { get; set; }
    }
}
