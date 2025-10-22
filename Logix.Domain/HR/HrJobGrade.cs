using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Table("HR_Job_Grade")]
    public partial class HrJobGrade
    {
        [Key]
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
        public long? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}
