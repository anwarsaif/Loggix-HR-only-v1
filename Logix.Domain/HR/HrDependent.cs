using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Logix.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Table("HR_Dependents")]
    public partial class HrDependent : TraceEntity
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }

        [Column("Emp_ID")]
        public long? EmpId { get; set; }

        [Column("ID_No")]
        [StringLength(50)]
        public string? IdNo { get; set; }

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
    }
}
