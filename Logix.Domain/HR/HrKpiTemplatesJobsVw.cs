using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrKpiTemplatesJobsVw
    {
        [Column("ID")]
        public long Id { get; set; }
        [Column("Template_ID")]
        public long? TemplateId { get; set; }
        [Column("Template_Name")]
        public string? TemplateName { get; set; }
        [Column("Template_Name2")]
        public string? TemplateName2 { get; set; }
        [Column("Cat_Job_ID")]
        public long? CatJobId { get; set; }
        [Column("Cat_Job_Name")]
        [StringLength(250)]
        public string? CatJobName { get; set; }
        [Column("Cat_Job_Name2")]
        [StringLength(250)]
        public string? CatJobName2 { get; set; }
        [Column("Facility_ID")]
        public long? FacilityId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
