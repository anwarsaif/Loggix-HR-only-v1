using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Table("HR_Decisions_Type_Employee")]
    public partial class HrDecisionsTypeEmployee
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Column("Decisions_Type_ID")]
        public long? DecisionsTypeId { get; set; }
        [Column("Emp_ID")]
        public long? EmpId { get; set; }
        public long? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
