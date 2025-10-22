using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrNotificationsSettingVw
    {
        [Column("ID")]
        public long Id { get; set; }
        [Column("Notification_Date")]
        [StringLength(10)]
        public string? NotificationDate { get; set; }
        [Column("Nationality_ID")]
        public int? NationalityId { get; set; }
        public string? Subject { get; set; }
        public string? Detailes { get; set; }
        [Column("Attach_File")]
        public string? AttachFile { get; set; }
        public bool? IsActive { get; set; }
        [Column("Nationality_Name")]
        [StringLength(250)]
        public string? NationalityName { get; set; }
        public bool? IsDeleted { get; set; }
        [Column("Facility_ID")]
        public long? FacilityId { get; set; }
    }
}
