using System.ComponentModel.DataAnnotations;

namespace Logix.Application.DTOs.HR
{
    public class HrDisciplinaryCaseDto
    {
        public long Id { get; set; }

        [StringLength(2500)]
        [Required]
        public string? CaseName { get; set; }
        public string? CaseName2 { get; set; }


        public int? CBegin { get; set; }

        public int? CEnd { get; set; }

        public bool? ApplyOfDelay { get; set; }

        public bool? ApplyOfAbsence { get; set; }

        [Range(1, long.MaxValue)]
        public int? TypeId { get; set; }

        public int? FromMinutes { get; set; }

        public int? ToMinutes { get; set; }

        public int? FromDay { get; set; }

        public int? ToDay { get; set; }

        public bool? ApplyOfEarly { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool? IsDeleted { get; set; }
    }


    public class HrDisciplinaryCaseEditDto
    {
        public long Id { get; set; }

        [StringLength(2500)]
        [Required]
        public string? CaseName { get; set; }
        public string? CaseName2 { get; set; }


        public int? CBegin { get; set; }

        public int? CEnd { get; set; }

        public bool? ApplyOfDelay { get; set; }

        public bool? ApplyOfAbsence { get; set; }

        [Range(1, long.MaxValue)]
        public int? TypeId { get; set; }

        public int? FromMinutes { get; set; }

        public int? ToMinutes { get; set; }

        public int? FromDay { get; set; }

        public int? ToDay { get; set; }

        public bool? ApplyOfEarly { get; set; }

        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }

}
