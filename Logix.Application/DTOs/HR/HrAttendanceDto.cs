
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Logix.Application.DTOs.HR
{
    public class HrAttendanceDto
    {
        public long AttendanceId { get; set; }
        public long? EmpId { get; set; }

        public DateTime? TimeIn { get; set; }
        [StringLength(500)]
        public string? NoteIn { get; set; }
        public DateTime? TimeOut { get; set; }

        [StringLength(500)]
        public string? NoteOut { get; set; }
        [Required]
        public int AttType { get; set; }

        public int? DayNo { get; set; }

        public bool? AllowTimeIn { get; set; }

        public bool? AllowTimeOut { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? DefTimeIn { get; set; }
        public DateTime? DefTimeOut { get; set; }
        [StringLength(10)]
        public string? DayDateGregorian { get; set; }
        [StringLength(10)]
        public string? DayDateHijri { get; set; }
        public long? CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }
        public bool? IsDeleted { get; set; }
        public string? WhyException { get; set; }
        [StringLength(50)]
        public string? LogInBy { get; set; }
        [StringLength(50)]
        public string? LogOutBy { get; set; }

        public long? TimeTableId { get; set; }

        public string? Longitude { get; set; }
        public string? Latitude { get; set; }
        public long? LocationId { get; set; }
        public string? LongitudeOut { get; set; }
        public string? LatitudeOut { get; set; }

        public bool? IsnextDay { get; set; }
        //Extra variables for Addition

        //     متغير  مستقبلي غير مستخدم  سنستخدمة مستقبلا اذا وجد استخدام  لنفس البرسيجر 
        public int? CMDTYPE { get; set; }
        [Required]
        public string TxtDate { get; set; } = null!;
        //      رقم الموظف  
        [Required]
        public string EmpCode { get; set; } = null!;

        //ملاحظات
        public string? Note { get; set; }

        //  الوقت
        [Required]
        public string timeText { get; set; } = null!;


    }

    public class HrAttendanceEditDto
    {
        public long AttendanceId { get; set; }
        public string? EmpCode { get; set; }
        public DateTime? TimeIn { get; set; }
        public string? TimeInString { get; set; }
        [StringLength(500)]
        public string? NoteIn { get; set; }
        public DateTime? TimeOut { get; set; }
        public string? TimeOutString { get; set; }

        [StringLength(500)]
        public string? NoteOut { get; set; }

        public int? AttType { get; set; }

        public int? DayNo { get; set; }

        public bool? AllowTimeIn { get; set; }

        public bool? AllowTimeOut { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? DefTimeIn { get; set; }
        public string? DefTimeInString { get; set; }
        public DateTime? DefTimeOut { get; set; }
        public string? DefTimeOutString { get; set; }

        [StringLength(10)]
        public string DayDateGregorian { get; set; } = null!;
        [StringLength(10)]
        public string? DayDateHijri { get; set; }
        public long? ModifiedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }
        public string? WhyException { get; set; }
        [StringLength(50)]
        public string? LogInBy { get; set; }
        [StringLength(50)]
        public string? LogOutBy { get; set; }

        public long? TimeTableId { get; set; }

        public string? Longitude { get; set; }
        public string? Latitude { get; set; }
        public long? LocationId { get; set; }
        public string? LongitudeOut { get; set; }
        public string? LatitudeOut { get; set; }

        public bool? IsnextDay { get; set; }
    }

    public class HrAttendanceAdd1Dto
    {
        public long? EmpId { get; set; }
        public DateTime? TimeIn { get; set; }
        public string? NoteIn { get; set; }
        public DateTime? TimeOut { get; set; }
        public string? NoteOut { get; set; }
        [Required]
        public int AttType { get; set; }

        public int? DayNo { get; set; }

        public DateTime? DefTimeIn { get; set; }
        public DateTime? DefTimeOut { get; set; }
        public string? DayDateGregorian { get; set; }
        public string? DayDateHijri { get; set; }

        public bool? IsDeleted { get; set; }
        public string? LogInBy { get; set; }
        public string? LogOutBy { get; set; }

        public string? Longitude { get; set; }
        public string? Latitude { get; set; }
        public string? LongitudeOut { get; set; }
        public string? LatitudeOut { get; set; }

        //Extra variables for Addition

        //     متغير  مستقبلي غير مستخدم  سنستخدمة مستقبلا اذا وجد استخدام  لنفس البرسيجر 
        public long? CMDTYPE { get; set; }
        [Required]
        public string TxtDate { get; set; } = null!;
        //      رقم الموظف  
        [Required]
        public string EmpCode { get; set; } = null!;

        //ملاحظات
        public string? Note { get; set; }

        //  الوقت
        [Required]
        public string timeText { get; set; } = null!;


    }

    public partial class HrAttendancesFilterDto
    {
        public string? EmpCode { get; set; }
        public long? Id { get; set; }
        public string? EmpName { get; set; }
        public DateTime? TimeIn { get; set; } = DateTime.Now;
        public DateTime? TimeOut { get; set; } = DateTime.Now;
        public string? NoteIn { get; set; }
        public string? NoteOut { get; set; }
        public string? DayName { get; set; }
        public string? theDate { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public int? BranchId { get; set; }

    }



    //--------------------------Begin Of Filter Attendances to Add MultiAttendance Dtos-------------------------------//

    public class HRAddMultiAttendanceFilterDto
    {
        public string? EmpCode { get; set; }
        public string? EmpName { get; set; }
        public int? BranchId { get; set; }
        public int? TimeTableId { get; set; }
        [Required]
        public string DayDateGregorian { get; set; } = null!;
        public int? Location { get; set; }
        public int? DeptId { get; set; }
        //  هذا الحقل يستخدم في الواجهة كمطلوب ولكن لاتعتمد عليه الفلترة 
        [Required]
        public int? AttendanceType { get; set; }
    }
    public partial class HRAddMultiAttendanceDto
    {
        public long? Id { get; set; }

        public string Emp_ID { get; set; } = null!;
        public string? Emp_Name { get; set; }

        public string? Shift_Name { get; set; }
        public string? Emp_ID_Int { get; set; }
        public string? Cat_name { get; set; }
        public string? Cat_name2 { get; set; }

        public string? Dep_Name { get; set; }
        public string? Location_Name { get; set; }

        public int? Attend { get; set; }
        public int? Absence { get; set; }
        public int? Rest { get; set; }
    }
    //--------------------------End Of Filter Attendances to Add MultiAttendance Dtos-------------------------------//


    public class HrMultiAddDto
    {
        [Required]
        public long? TimeTableId { get; set; }
        [Required]
        public int AttType { get; set; }
        //Extra variables for Addition

        //     متغير  مستقبلي غير مستخدم  سنستخدمة مستقبلا اذا وجد استخدام  لنفس البرسيجر 
        [Required]
        public string TxtDate { get; set; } = null!;
        //      رقم الموظف  
        [Required]
        public string EmpCode { get; set; } = null!;
        //ملاحظات
        public string? Note { get; set; }
        //  الوقت
        [Required]
        public string TimeText { get; set; } = null!;
        public bool RestSelected { get; set; } = false;
        public bool AbsenceSelected { get; set; } = false;
        public bool PresentSelected { get; set; } = false;


    }
    public class AddMultiAttendanceResultDto
    {
        public string? SavedAttendanceRecord { get; set; }
        public string? SavedAbsenceRecord { get; set; }
        public string? SavedRestRecord { get; set; }
        public List<string>? EmpWithProblems { get; set; }
        public string? AttendanceNotSelectedMessage { get; set; }
        public string? AbsenceNotSelectedMessage { get; set; }
        public string? RestNotSelectedMessage { get; set; }
    }
    //--------------------------Begin Of add Attendance From Excel Dtos-------------------------------//

    /// <summary>
    /// this Dto to add Attendance From Excel
    /// </summary>
    public class AddAttendanceFromExcelDto
    {
        [Required]
        public string TxtDate { get; set; } = null!;
        //      رقم الموظف  
        [Required]
        public string EmpCode { get; set; } = null!;
        //  الوقت
        [Required]
        public string TimeText { get; set; } = null!;
        [Required]
        public int AttType { get; set; }

        //ملاحظات
        public string? Note { get; set; }

    }
    public class AddAttendanceFromExcelResultDto
    {
        public List<string>? SavedRecord { get; set; }
        public List<string>? EmpNotActive { get; set; }
        public List<string>? EmpNotExist { get; set; }
        public List<string>? PayrollAviable { get; set; }
        public List<string>? EmpWithProblems { get; set; }
    }
    //--------------------------End Of add Attendance From Excel Dtos-------------------------------//


    //--------------------------Begin Of HR_Attendance_TotalReportManagerial_SP StoredProcedures-------------------------------//

    [Keyless]
    public class HRAttendanceTotalReportDto
    {
        public string? Location_Name { get; set; }
        public string? Location_Name2 { get; set; }
        public string? Emp_ID { get; set; }
        public string? Emp_name { get; set; }
        public string? Emp_name2 { get; set; }
        public int? WorkingDays { get; set; }
        public int? Attendances { get; set; }
        public int? Absence { get; set; }
        public int? Vacation_days { get; set; }
        public int? Remote_Work_Days { get; set; }
        public int? Assignments_Days { get; set; }
        public int? AssignmentsDaysWithNoShift { get; set; }
        public int? BusinessTrip_Days { get; set; }
        public string? Shift_Hours { get; set; }
        public string? Work_hours { get; set; }
        public string? Delay_in { get; set; }
        public string? Early_out { get; set; }
        public string? Compensation { get; set; }
        public int? Permissions { get; set; }
        public string? Contract_Type_Name { get; set; }
    }

    public class HRAttendanceTotalReportFilterDto
    {
        public string? From { get; set; }
        public string? To { get; set; }
        public string? empCode { get; set; }
        public string? EmpName { get; set; }

        public int? BranchId { get; set; }
        public int? Location { get; set; }
    }

    //--------------------------End Of HR_Attendance_TotalReportManagerial_SP StoredProcedures-------------------------------//
    
    // رفع بيانات الحضور والانصراف
    public class HRAttendanceCheckingStaffFilterDto
    {
        public string? Date { get; set; }
        public string? empCode { get; set; }
        public string? EmpName { get; set; }
        public int? DeptId { get; set; }
        public int? Permission { get; set; }
        public int? Absence { get; set; }
        public int? NormalVacation { get; set; }
        public int? OtherVacation { get; set; }
    }
    public class HRAttendanceUploadDto
    {
        public string Date { get; set; } = null!;
        public List<HRAttendanceCheckingStaffFilterDto> Data { get; set; } = null!;
    }

    //    إعادة أرسال البصمة
    public class HrAttendanceResetDto
    {
        public long? EmpId { get; set; }
        public string? EmpCode { get; set; }
        public string? From { get; set; }
        public string? To { get; set; }
        public int? Branch { get; set; }

       

    }
}
