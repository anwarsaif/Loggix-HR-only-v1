using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrJobGradeVw
    {
        [Column("ID")]
        public long Id { get; set; }
        [Column("Grade_Name")]
        [StringLength(50)]
        public string? GradeName { get; set; }
        [Column("Grade_No")]
        [StringLength(50)]
        public string? GradeNo { get; set; }
        [Column("Level_ID")]
        public long? LevelId { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Salary { get; set; }
        public bool IsDeleted { get; set; }
        [Column("Level_Name")]
        [StringLength(250)]
        public string? LevelName { get; set; }
    }
}
