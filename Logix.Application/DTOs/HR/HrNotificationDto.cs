//using Castle.MicroKernel.SubSystems.Conversion;
using Logix.Application.DTOs.Main;

using Logix.Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Logix.Application.DTOs.HR
{
    public class HrNotificationDto : TraceEntity
    {
        public long? Id { get; set; }

        [StringLength(10)]
        public string? NotificationDate { get; set; }

        public int? TypeId { get; set; }

        public long? FacilityId { get; set; }

        public long? EmpId { get; set; }
        public string? EmpCode { get; set; }

        [Required]
        public string? Subject { get; set; }

        [Required]
        public string? Detailes { get; set; }


        public bool? IsRead { get; set; }

        public DateTime? ReadDate { get; set; }
        public List<SaveFileDto>? fileDtos { get; set; }


    }
    public class HrNotificationEditDto
    {
        public long Id { get; set; }

        [StringLength(10)]
        public string? NotificationDate { get; set; }

        public int? TypeId { get; set; }

        public long? FacilityId { get; set; }

        public long? EmpId { get; set; }
        public string? EmpCode { get; set; }

        public string? Subject { get; set; }
        public string? Detailes { get; set; }

        public long? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }

        public bool? IsRead { get; set; }

        public DateTime? ReadDate { get; set; }


    }
}
