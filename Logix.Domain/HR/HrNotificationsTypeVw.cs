using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrNotificationsTypeVw
    {
        [Column("ID")]
        public long Id { get; set; }
        public bool? IsActive { get; set; }
        [Column("Facility_ID")]
        public long? FacilityId { get; set; }
        public bool? IsDeleted { get; set; }
        [Column("Subject_Type")]
        public int? SubjectType { get; set; }
        [Column("Msg_Subject")]
        [StringLength(250)]
        public string? MsgSubject { get; set; }
        public string? Detailes { get; set; }
    }

}
