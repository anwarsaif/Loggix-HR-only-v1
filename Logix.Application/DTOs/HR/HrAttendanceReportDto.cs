using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Application.DTOs.HR
{
    [Keyless]
    public class HrAttendanceReportDto
    {
        //this dto use for HR_Attendance_Report4_SP

        public string? Color_Value { get; set; }
        public string? MapLocation { get; set; }
        public string? Attendance_Type_Name { get; set; }
        public DateTime? Def_Time_Out { get; set; }
        public DateTime? Def_Time_In { get; set; }
        public string? Day_Name { get; set; }
        public string? Day_Date_Gregorian { get; set; }
        public string? Emp_Code { get; set; }
        public string? Emp_name { get; set; }
        public string? TimeTable_Name { get; set; }
        public DateTime? Time_In { get; set; }
        public DateTime? Time_Out { get; set; }
        public TimeSpan? AllowTime_In { get; set; }
        public TimeSpan? AllowTime_Out { get; set; }
        public string? Location_Name { get; set; }
        public string? BRA_NAME { get; set; }
        public string? Day_Name2 { get; set; }
        public string? Location_Name2 { get; set; }
        public string? Emp_name2 { get; set; }
        public string? Dep_Name { get; set; }
        public string? Dep_name2 { get; set; }
        public long? TimeTable_ID { get; set; }
        public string? BRA_NAME2 { get; set; }
        public int? Delay { get; set; }
        public int? LeaveEarly { get; set; }
        public int? Late_Time_M { get; set; }
        public int? Leave_Early_Time_M { get; set; }
        public int? Permission { get; set; }
        public int? Task { get; set; }
        public int? Trng { get; set; }
        public int? addTime { get; set; }

    }

    public class HrAttendanceReportVM
    {
        public HrAttendanceReportDto Dto { get; set; }

        //these variables for display forexample: delay =130  => delay = 2:10
        public string? TrngH_M { get; set; }
        public string? AddTimeH_M { get; set; }
        public string? DelayH_M { get; set; }
        public string? TaskH_M { get; set; }
        public string? LeaveEarlyH_M { get; set; }
        public string? PermissionH_M { get; set; }
        
        public HrAttendanceReportVM()
        {
            Dto = new HrAttendanceReportDto();
        }
    }
}
