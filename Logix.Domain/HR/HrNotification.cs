using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Logix.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Table("HR_Notifications")]
    [Index(nameof(TypeId), nameof(FacilityId), nameof(EmpId), nameof(IsDeleted), nameof(IsRead), Name = "NonClusteredIndex-20210731-111702")]
    public partial class HrNotification : TraceEntity
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Column("Notification_Date")]
        [StringLength(10)]
        public string? NotificationDate { get; set; }
        [Column("Type_ID")]
        public int? TypeId { get; set; }
        [Column("Facility_ID")]
        public long? FacilityId { get; set; }
        [Column("Emp_ID")]
        public long? EmpId { get; set; }
        public string? Subject { get; set; }
        public string? Detailes { get; set; }      

        [Column("Is_Read")]
        public bool? IsRead { get; set; }
        [Column("Read_Date", TypeName = "datetime")]
        public DateTime? ReadDate { get; set; }
    }
}
