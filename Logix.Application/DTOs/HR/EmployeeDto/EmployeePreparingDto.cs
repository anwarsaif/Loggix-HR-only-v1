using System.ComponentModel.DataAnnotations.Schema;

namespace Logix.Application.DTOs.HR.EmployeeDto
{
    public class EmployeePreparingDto
    {
        public long Id { get; set; }
        //نوع التحضير
        [Column("Attendance_Type")]
        public int? AttendanceType { get; set; }

        [Column("TimeZone_ID")]
        public int? TimeZoneId { get; set; }
        //  استبعاد من التحضير
        [Column("Exclude_Attend")]
        public bool? ExcludeAttend { get; set; }
        // الزامية تفعيل البصمة من خلال الجوال في التطبيق
        [Column("Check_Device")]
        public bool? CheckDevice { get; set; }
        //تم التفعيل للبصمة من خلال الجوال في التطبيق

        [Column("Check_Device_Active")]
        public bool? CheckDeviceActive { get; set; }
        //  إلزامية الموقع 
        public bool? IsRequiredGps { get; set; }

    }

}
