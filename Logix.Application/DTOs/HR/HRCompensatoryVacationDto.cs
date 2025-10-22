using System.ComponentModel.DataAnnotations;
using Logix.Application.DTOs.Main;

using Microsoft.EntityFrameworkCore;

namespace Logix.Application.DTOs.HR
{
    public class HrCompensatoryVacationDto
    {
        public long? CompensatoryId { get; set; }

        public long? EmpId { get; set; }

        public int? VacationTypeId { get; set; }

        public int? VacationAccountDay { get; set; }

        [StringLength(10)]
        [Unicode(false)]
        public string? VacationSdate { get; set; }

        [StringLength(10)]
        [Unicode(false)]
        public string? VacationEdate { get; set; }

        public long? AppId { get; set; }

        public long? StatusId { get; set; }

        public string? Note { get; set; }

        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool? IsDeleted { get; set; }

    }
    public class HrCompensatoryVacationEditDto
    {
        public long CompensatoryId { get; set; }

        public long? EmpId { get; set; }
        public string? EmpCode { get; set; }

        public int? VacationTypeId { get; set; }

        public int? VacationAccountDay { get; set; }

        [StringLength(10)]
        [Unicode(false)]
        public string? VacationSdate { get; set; }

        [StringLength(10)]
        [Unicode(false)]
        public string? VacationEdate { get; set; }

        public long? AppId { get; set; }

        public long? StatusId { get; set; }

        public string? Note { get; set; }
        public List<SaveFileDto>? fileDtos { get; set; }



    }

    public class HrCompensatoryVacationAddDto
    {
        public long? CompensatoryId { get; set; }
        public long? EmpId { get; set; }
        public string? EmpCode { get; set; }
        [Required]
        public int? VacationTypeId { get; set; }
        [Required]
        public int? VacationAccountDay { get; set; }
        [Required]
        public string? VacationSdate { get; set; }
        [Required]
        public string? VacationEdate { get; set; }

        public long? AppId { get; set; }
        public int? AppTypeId { get; set; } = 0;
        public long ScreenID { get; set; }
        public int? TableID { get; set; }

        public long? StatusId { get; set; }

        public string? Note { get; set; }
        public List<SaveFileDto>? fileDtos { get; set; }

    }

}
