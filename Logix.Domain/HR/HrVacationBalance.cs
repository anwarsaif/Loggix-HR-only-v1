using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Logix.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Table("HR_VacationBalance")]
    public partial class HrVacationBalance : TraceEntity
    {
        [Key]
        [Column("Vac_balanceId")]
        public long VacBalanceId { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? VacationBalance { get; set; }
        [Column("V_balance_Rate", TypeName = "decimal(18, 2)")]
        public decimal? VBalanceRate { get; set; }
        [Column("Emp_Id")]
        public long? EmpId { get; set; }
        [StringLength(10)]
        public string? StartDate { get; set; }
        public bool? Isacitve { get; set; }
        [Column("Vacation_Type_Id")]
        public int? VacationTypeId { get; set; }
        public string? Note { get; set; }
    }
}
