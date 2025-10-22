using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Table("HR_KPI_Templates_Jobs", Schema = "dbo")]
    public partial class HrKpiTemplatesJob
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Column("Cat_Job_ID")]
        public long? CatJobId { get; set; }
        [Column("Template_ID")]
        public long? TemplateId { get; set; }
        public long CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        [Column("Facility_ID")]
        public long? FacilityId { get; set; }
    }
}
