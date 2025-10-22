using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrDefinitionSalaryEmp
    {
        [Column("ID")]
        public long Id { get; set; }
        [Column("Definition_Date")]
        [StringLength(10)]
        public string? DefinitionDate { get; set; }
        [Column("Definition_URL")]
        public string? DefinitionUrl { get; set; }
        [Column("Facility_ID")]
        public long? FacilityId { get; set; }
        public bool? IsDeleted { get; set; }
        [Column("Definition_Type_Name")]
        [StringLength(250)]
        public string? DefinitionTypeName { get; set; }
        [Column("Definition_Type_Name2")]
        [StringLength(250)]
        public string? DefinitionTypeName2 { get; set; }
        [Column("Definition_Type_ID")]
        public long? DefinitionTypeId { get; set; }
        [Column("Definition_Send_TO")]
        public long? DefinitionSendTo { get; set; }
        [Column("Emp_ID")]
        [StringLength(50)]
        public string EmpId { get; set; } = null!;
        [Column("Emp_name")]
        [StringLength(250)]
        public string? EmpName { get; set; }
        [Column("Emp_name2")]
        [StringLength(250)]
        public string? EmpName2 { get; set; }
        [Column("Location_Name")]
        [StringLength(200)]
        public string? LocationName { get; set; }
        [Column("Dep_Name")]
        [StringLength(200)]
        public string? DepName { get; set; }
        [Column("Location_Name2")]
        [StringLength(200)]
        public string? LocationName2 { get; set; }
        [Column("Dep_Name2")]
        [StringLength(200)]
        public string? DepName2 { get; set; }
        [Column("Definition_Send_TO_Name")]
        [StringLength(250)]
        public string? DefinitionSendToName { get; set; }
        [Column("Definition_Send_TO_Name2")]
        [StringLength(250)]
        public string? DefinitionSendToName2 { get; set; }
    }
}
