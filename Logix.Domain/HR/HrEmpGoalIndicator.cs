using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Table("HR_Emp_Goal_Indicators", Schema = "dbo")]
    public partial class HrEmpGoalIndicator
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Column("Period_ID")]
        public long? PeriodId { get; set; }
        [Column("KPI_Templates_ID")]
        public long? KpiTemplatesId { get; set; }
        public long CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}
