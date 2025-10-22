using System.ComponentModel.DataAnnotations.Schema;

namespace Logix.Application.DTOs.HR.EmployeeDto
{
    public class EmployeeFollowersDto
    {
        public long Id { get; set; }
        //  هل له تذاكر
        [Column("IS_Ticket")]
        public bool? IsTicket { get; set; }
        // هل له تأمين

        //الاسم الكامل بالعربي 

        //الاسم بالانجليزي 
        //تاريخ الميلاد

        //الحالة الإجتماعية

        [Column("Marital_Status")]
        public int? MaritalStatus { get; set; }
        // صلة القرابة

        //الجنسية
        [Column("Nationality_ID")]
        public int? NationalityId { get; set; }
        // الجنس
        public int? Gender { get; set; }
        //رقم الهوية 
    }

}
