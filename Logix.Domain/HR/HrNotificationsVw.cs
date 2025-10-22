using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrNotificationsVw
    {
        [Column("ID")]
        public long Id { get; set; }
        [Column("Notification_Date")]
        [StringLength(10)]
        public string? NotificationDate { get; set; }
        [Column("Type_ID")]
        public int? TypeId { get; set; }
        [Column("Facility_ID")]
        public long? FacilityId { get; set; }
        public string? Subject { get; set; }
        public string? Detailes { get; set; }
        public bool? IsDeleted { get; set; }
        [Column("Emp_ID")]
        public long? EmpId { get; set; }
        [Column("Emp_name")]
        [StringLength(250)]
        public string? EmpName { get; set; }
        [Column("Emp_Code")]
        [StringLength(50)]
        public string EmpCode { get; set; } = null!;
        [Column("Type_Name")]
        [StringLength(250)]
        public string? TypeName { get; set; }
        [Column("Is_Read")]
        public bool? IsRead { get; set; }
        [Column("Read_Date", TypeName = "datetime")]
        public DateTime? ReadDate { get; set; }
    }
}
