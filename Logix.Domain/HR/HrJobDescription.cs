using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Table("HR_Job_Description")]
    public partial class HrJobDescription
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Column("Job_Cat_ID")]
        public long? JobCatId { get; set; }
        [Column("Job_Title")]
        [StringLength(2500)]
        public string? JobTitle { get; set; }
        [Column("Job_Description")]
        public string? JobDescription { get; set; }
        [Column("File_Url")]
        public string? FileUrl { get; set; }
        public long CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}
