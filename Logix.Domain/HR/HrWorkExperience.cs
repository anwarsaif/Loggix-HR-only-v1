using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Table("HR_Work_Experience", Schema = "dbo")]
    public partial class HrWorkExperience
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Column("Emp_ID")]
        public long? EmpId { get; set; }
        [StringLength(2500)]
        public string? Company { get; set; }
        [Column("Job_Title")]
        [StringLength(50)]
        public string? JobTitle { get; set; }
        [Column("From_Work")]
        [StringLength(10)]
        public string? FromWork { get; set; }
        [Column("To_Work")]
        [StringLength(10)]
        public string? ToWork { get; set; }
        public string? Comment { get; set; }
        public long? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
