using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Table("HR_Notifications_Type")]
    public partial class HrNotificationsType
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        public bool? IsActive { get; set; }
        public string? Detailes { get; set; }
        [Column("Attach_File")]
        public string? AttachFile { get; set; }
        [Column("Facility_ID")]
        public long? FacilityId { get; set; }
        public long? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
        public bool? IsDeleted { get; set; }
        [Column("Subject_Type")]
        public int? SubjectType { get; set; }
    }
}
