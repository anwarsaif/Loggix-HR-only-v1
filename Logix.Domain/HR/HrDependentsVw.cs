using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Logix.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrDependentsVw : TraceEntity
    {
        [Column("Relationship_Name")]
        [StringLength(250)]
        public string? RelationshipName { get; set; }

        [Column("ID")]
        public long Id { get; set; }

        [Column("Emp_ID")]
        public long? EmpId { get; set; }

        [StringLength(50)]
        public string? Name { get; set; }

        [StringLength(50)]
        public string? Name1 { get; set; }

        [StringLength(50)]
        public string? Relationship { get; set; }

        [Column("Date_of_Birth")]
        [StringLength(50)]
        public string? DateOfBirth { get; set; }

        public bool? Insurance { get; set; }

        public bool? Ticket { get; set; }

        public int? Gender { get; set; }

        [Column("Insurance_No")]
        [StringLength(50)]
        public string? InsuranceNo { get; set; }

        [Column("Nationality_ID")]
        public int? NationalityId { get; set; }

        [Column("Marital_Status")]
        public int? MaritalStatus { get; set; }

        [Column("Emp_Code")]
        [StringLength(50)]
        public string EmpCode { get; set; } = null!;

        [Column("ISDEL")]
        public bool? Isdel { get; set; }

        [Column("Emp_name")]
        [StringLength(250)]
        public string? EmpName { get; set; }

        [Column("ID_No")]
        [StringLength(50)]
        public string? IdNo { get; set; }

        [Column("Nationality_Name")]
        [StringLength(250)]
        public string? NationalityName { get; set; }

        [Column("Nationality_Name2")]
        [StringLength(250)]
        public string? NationalityName2 { get; set; }

        public long? Code { get; set; }

        [Column("BRANCH_ID")]
        public int? BranchId { get; set; }

        [Column("Facility_ID")]
        public int? FacilityId { get; set; }
    }
}
