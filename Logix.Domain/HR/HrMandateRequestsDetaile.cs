using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Table("HR_Mandate_Requests_Detailes", Schema = "dbo")]
    public partial class HrMandateRequestsDetaile
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Column("MR_ID")]
        public long? MrId { get; set; }
        [Column("Job_Level_ID")]
        public long? JobLevelId { get; set; }
        [Column("Allowance_value", TypeName = "decimal(18, 2)")]
        public decimal? AllowanceValue { get; set; }
        [Column("Houseing_IsSecured")]
        public bool? HouseingIsSecured { get; set; }
        [Column("Rate_per_night", TypeName = "decimal(18, 2)")]
        public decimal? RatePerNight { get; set; }
        [Column("Transport_IsInsured")]
        public bool? TransportIsInsured { get; set; }
        [Column("Transport_Amount", TypeName = "decimal(18, 2)")]
        public decimal? TransportAmount { get; set; }
        [Column("Ticket_Value", TypeName = "decimal(18, 2)")]
        public decimal? TicketValue { get; set; }
        public string? Note { get; set; }
        public long? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        [Column("Emp_ID")]
        public long? EmpId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
