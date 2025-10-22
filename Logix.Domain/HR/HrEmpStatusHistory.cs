using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Table("HR_Emp_Status_History", Schema = "dbo")]
    public partial class HrEmpStatusHistory
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        public long? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
        public bool? IsDeleted { get; set; }
        [Column("Status_ID")]
        public int? StatusId { get; set; }
        [Column("Emp_ID")]
        public int? EmpId { get; set; }
        public string? Note { get; set; }
        [Column("Status_ID_Old")]
        public int? StatusIdOld { get; set; }
    }
}
