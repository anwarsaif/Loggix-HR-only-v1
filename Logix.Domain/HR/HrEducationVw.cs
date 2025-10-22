using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrEducationVw
    {
        [Column("ID")]
        public long Id { get; set; }
        [Column("Emp_ID")]
        public long? EmpId { get; set; }
        [Column("Level_Edu")]
        public int? LevelEdu { get; set; }
        [StringLength(50)]
        public string? Institute { get; set; }
        [StringLength(50)]
        public string? Specialization { get; set; }
        [Column("Year_Edu")]
        [StringLength(10)]
        public string? YearEdu { get; set; }
        [StringLength(50)]
        public string? Score { get; set; }
        [Column("Start_Date")]
        [StringLength(10)]
        public string? StartDate { get; set; }
        [Column("End_Date")]
        [StringLength(10)]
        public string? EndDate { get; set; }
        [Column("Level_Edu_Name")]
        [StringLength(250)]
        public string? LevelEduName { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
