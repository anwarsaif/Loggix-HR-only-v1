using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Application.DTOs.HR
{
    public class HrCheckInOutDto
    {
        public long Id { get; set; }

        public long? EmpId { get; set; }
        public DateTime? Checktime { get; set; }
        public int? Checktype { get; set; }
        public int? DayNo { get; set; }
        public bool? IsSend { get; set; }
        public bool? SendActualAttendance { get; set; }
    }



    public class HrAttendanceUnknownFilterDto
    {
        public string? EmpCode { get; set; }
        public string? EmpName { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public string? TimeFrom { get; set; }
        public string? TimeTo { get; set; }
        public int? Location { get; set; }
        public int? BranchId { get; set; }


        /// <summary>
        ///  Search Result Variables
        /// </summary>
        public long? Id { get; set; }
        public DateTime? Checktime { get; set; }
        public string? TimeText { get; set; }
        public string? DayName { get; set; }
        public string? CheckTypeName { get; set; }
    }

    public class HrRecordAttendanceFilterDto
    {
        public string? EmpCode { get; set; }
        public string? EmpName { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public string? TimeFrom { get; set; }
        public string? TimeTo { get; set; }
        public int? Location { get; set; }
        public int? BranchId { get; set; }
        public int? AttType { get; set; }
    }

    public class HrCheckInOutFilterDto
    {
        public string? EmpCode { get; set; }
        public string? EmpName { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public int? TotalTimeIn { get; set; }
        public int? TotalTimeOut { get; set; }
        public int? ReportType { get; set; }
    }

    public class HrUpdateCheckINout
    {
        public string? EmpCode { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public List<HrActualAttendanceReportDto>? AttendanceData { get; set; }
    }
}
