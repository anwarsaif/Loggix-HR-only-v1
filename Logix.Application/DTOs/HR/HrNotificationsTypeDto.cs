using Castle.MicroKernel.SubSystems.Conversion;
using System.ComponentModel.DataAnnotations.Schema;

namespace Logix.Application.DTOs.HR
{
    public class HrNotificationsTypeDto
    {
        public long? Id { get; set; }
        public bool? IsActive { get; set; }
        public string? Detailes { get; set; }
        public string? AttachFile { get; set; }
        public long? FacilityId { get; set; }
        public bool? IsDeleted { get; set; }
        public int? SubjectType { get; set; }
    }
    public class HrNotificationsTypeEditDto
    {
        public long Id { get; set; }
        public string? NameLookup { get; set; }
        public bool? IsActive { get; set; }
        public string? Detailes { get; set; }
        public string? AttachFile { get; set; }
        public long? FacilityId { get; set; }
        public int? SubjectType { get; set; }

    }

    public class HrNotificationsTypeFilterDto
    {
        public int? IsActive { get; set; }
        public string? MsgSubject { get; set; }
        public long? Id { get; set; }
    }
}
