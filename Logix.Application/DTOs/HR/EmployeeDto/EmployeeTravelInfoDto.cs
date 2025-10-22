using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Logix.Application.DTOs.HR.EmployeeDto
{
    public class EmployeeTravelInfoDto
    {
        public long Id { get; set; }

        //  هل له تذاكر
        [Column("IS_Ticket")]
        public bool? IsTicket { get; set; }
        //وجهة السفر
        [StringLength(250)]
        public string? TicketTo { get; set; }
        //عدد التذاكر 
        public string? TicketNoDependent { get; set; }

        //نوع التذاكر
        public int? TicketType { get; set; }

        //استحقاق التذاكر

        [Column("Ticket_Entitlement")]
        public int? TicketEntitlement { get; set; }
        //قيمة التذكرة 
        [Column("Value_Ticket", TypeName = "decimal(18, 2)")]
        public decimal? ValueTicket { get; set; }
    }

}
