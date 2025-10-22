using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrSkillsVw
    {
        [Column("ID")]
        public long Id { get; set; }
        [Column("Emp_ID")]
        public long? EmpId { get; set; }
        public int? Skill { get; set; }
        [Column("Year_Experience")]
        [StringLength(10)]
        public string? YearExperience { get; set; }
        [Column("comment")]
        public string? Comment { get; set; }
        [Column("Skill_Name")]
        [StringLength(250)]
        public string? SkillName { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
