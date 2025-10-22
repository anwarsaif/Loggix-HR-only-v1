using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Logix.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{

    [Table("HR_Permissions")]
    public class HrPermission : TraceEntity
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }

        [Column("Emp_ID")]
        public long? EmpId { get; set; }

        public int? Type { get; set; }

        [Column("Reason_Leave")]
        public int? ReasonLeave { get; set; }

        [Column("Permission_Date")]
        [StringLength(10)]
        public string? PermissionDate { get; set; }

        [Column("Details_Reason")]
        public string? DetailsReason { get; set; }

        [Column("Leaveing_Time")]
        [StringLength(50)]
        public string? LeaveingTime { get; set; }

        [Column("Estimated_Time_Return")]
        [StringLength(50)]
        public string? EstimatedTimeReturn { get; set; }

        [Column("Contact_Number")]
        [StringLength(50)]
        public string? ContactNumber { get; set; }

        public string? Note { get; set; }
    }
}
