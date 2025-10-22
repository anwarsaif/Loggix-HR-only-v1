using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrTicketVw
    {
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

        public bool IsDeleted { get; set; }

        [Column("Emp_Code")]
        [StringLength(50)]
        public string EmpCode { get; set; } = null!;

        [Column("Emp_name")]
        [StringLength(250)]
        public string? EmpName { get; set; }

        [Column("Is_billable_Name")]
        [StringLength(250)]
        public string? IsBillableName { get; set; }

        [Column("Total_Amount", TypeName = "decimal(18, 2)")]
        public decimal? TotalAmount { get; set; }

        [Column("BRANCH_ID")]
        public int? BranchId { get; set; }

        [Column("Dept_ID")]
        public int? DeptId { get; set; }

        public int? Location { get; set; }

        [Column("Emp_name2")]
        [StringLength(250)]
        public string? EmpName2 { get; set; }

        [Column("Is_billable_Name2")]
        [StringLength(250)]
        public string? IsBillableName2 { get; set; }
    }
}
