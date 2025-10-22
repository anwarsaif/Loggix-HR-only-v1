using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrLanguagesVw
    {
        [Column("ID")]
        public long Id { get; set; }
        [Column("Emp_ID")]
        public long? EmpId { get; set; }
        public int? Language { get; set; }
        [Column("Skill_Lang")]
        public int? SkillLang { get; set; }
        [Column("Fluency_Level")]
        public int? FluencyLevel { get; set; }
        public string? Comment { get; set; }
        [Column("Lang_Name")]
        [StringLength(250)]
        public string? LangName { get; set; }
        [Column("Skill_Lang_Name")]
        [StringLength(250)]
        public string? SkillLangName { get; set; }
        [Column("Fluency_Level_Name")]
        [StringLength(250)]
        public string? FluencyLevelName { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
