using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Logix.Domain.Base;

namespace Logix.Domain.HR
{
    [Table("HR_Ticket")]
    public partial class HrTicket : TraceEntity
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Column("Emp_ID")]
        public long? EmpId { get; set; }
        public int? Way { get; set; }
        public int? Cabins { get; set; }
        [Column("Ticket_Date")]
        [StringLength(10)]
        public string? TicketDate { get; set; }
        [Column("Ticket_No")]
        [StringLength(250)]
        public string? TicketNo { get; set; }
        [Column("Ticket_Count")]
        public decimal? TicketCount { get; set; }
        [Column("Ticket_Amount", TypeName = "decimal(18, 2)")]
        public decimal? TicketAmount { get; set; }
        [Column("Is_billable")]
        public int? IsBillable { get; set; }
        public string? Purpose { get; set; }
        public string? Note { get; set; }
        [Column("Total_Amount", TypeName = "decimal(18, 2)")]
        public decimal? TotalAmount { get; set; }
    }
}
