using Castle.MicroKernel.SubSystems.Conversion;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Logix.Application.DTOs.HR
{
    public class HrAbsenceDto
    {

        public long Id { get; set; }

        public long EmpId { get; set; }

        public int AbsenceTypeId { get; set; }

        public string AbsenceDate { get; set; } = null!;
        public long CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }
        public bool IsDeleted { get; set; }

        public string? Type { get; set; }
        public string? Note { get; set; }

        public long? LocationId { get; set; }

        public int? TimeTableId { get; set; }
    }
    public class HrAbsenceEditDto
    {

        public long Id { get; set; }

        public long EmpId { get; set; }

        public int AbsenceTypeId { get; set; }

        public string AbsenceDate { get; set; } = null!;

        public long? ModifiedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public string? Type { get; set; }
        public string? Note { get; set; }

        public long? LocationId { get; set; }

        public int? TimeTableId { get; set; }
    }

    public partial class HrAbsenceFilterDto
    {
        public long Id { get; set; }
        public string? EmpName { get; set; }
        public string? EmpCode { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public int? DeptId { get; set; }
        public int? Location { get; set; }
        public int? BranchId { get; set; }
        public int? StatusId { get; set; }

        public string? DepName { get; set; }
        public string? LocationName { get; set; }
        public string? AbsenceDate { get; set; }
        public string? Note { get; set; }


    }
 
    public class HrAbsenceAddDto
    {
        [Required]

        public string EmpCode { get; set; } = null!;
        public string? EmpName { get; set; } 
        // السبب 
        [Required]
        public int Type { get; set; }
        [Required]
        public string AbsenceDate { get; set; } = null!;
        public string? ToDate { get; set; } 

        public string? Note { get; set; }
        public bool ApplyPlenties { get; set; }
        public long? DisciplinaryCaseId { get; set; }
        public int? CountRept { get; set; }
        public decimal? DeductedRate { get; set; }
        public decimal? DeductedAmount { get; set; }
        public int? ActionType { get; set; }
        public int? DaysCount { get; set; }


    }
    public class AddAbsenceFromExcelResultDto
    {
        public string? SavedAbsenceRecord { get; set; }
        public string? EmpNotAviable { get; set; }
        public string? AbsenceDateAviable { get; set; }
        public string? PayrollAviable { get; set; }
        public string? DOAppointmentAviable { get; set; }
        public string? VacationAviable { get; set; }
        public string? DelayAviable { get; set; }
        public string? EmpNotActiveAble { get; set; }
    }

    public class AbsenceNotAttendanceDto
    {
        public long? LocationId { get; set; }
        public long? DeptId { get; set; }
        public int? BranchId { get; set; }
        [Required]
        public string FromDate { get; set; } = null!;
        [Required]
        public string ToDate { get; set; } = null!;
        public int ? CMDTYPE { get; set; } = null!;
        public int? DepManagerID { get; set; }


    }

    public class HrMultiAbsenceAddDto
    {
        [Required]

        public List<string> EmpCode { get; set; } = null!;
        // السبب 
        [Required]
        public int Type { get; set; }
        [Required]
        public string AbsenceDate { get; set; } = null!;

        public string? Note { get; set; }
        public bool ApplyPlenties { get; set; }
        public long? DisciplinaryCaseId { get; set; }
        public int? CountRept { get; set; }
        public decimal? DeductedRate { get; set; }
        public decimal? DeductedAmount { get; set; }
        public int? ActionType { get; set; }
    }

    public partial class HrMultiAbsenceAddFilterDto
    {
        public string? EmpName { get; set; }
        public string? EmpCode { get; set; }
        public int? DeptId { get; set; }
        public int? Location { get; set; }
        public int? BranchId { get; set; }
        public string? IDNumber { get; set; }
        public string? EmpNameEn { get; set; }
        public string? DeptName { get; set; }
        public string? CatName { get; set; }

    }
    public class AddMultiAbsenceAddResultDto
    {
        public string? SavedAbsenceRecord { get; set; }
        public string? EmpNotAviable { get; set; }
        public string? AbsenceDateAviable { get; set; }
        public string? PayrollAviable { get; set; }
        public string? DOAppointmentAviable { get; set; }
        public string? VacationAviable { get; set; }
        public string? DelayAviable { get; set; }
        public string? EmpNotActiveAble { get; set; }
    }

}
