using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Logix.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Table("HR_Salary_Group_Refrance")]
    public partial class HrSalaryGroupRefrance:TraceEntity
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Column("Group_ID")]
        public long? GroupId { get; set; }
        [Column("Reference_Type_ID")]
        public int? ReferenceTypeId { get; set; }
        [Column("Account_ID")]
        public long? AccountId { get; set; }
    }
}
