using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Table("HR_Emp_Goal_Indicators_Competences", Schema = "dbo")]
    public partial class HrEmpGoalIndicatorsCompetence
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Column("Competences_ID")]
        public long CompetencesId { get; set; }
        [Column("GoalIndicators_ID")]
        public long? GoalIndicatorsId { get; set; }
        [Column("weight")]
        [StringLength(50)]
        public string? Weight { get; set; }
        [StringLength(50)]
        public string? Target { get; set; }
        public string? Note { get; set; }
        public long CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}
