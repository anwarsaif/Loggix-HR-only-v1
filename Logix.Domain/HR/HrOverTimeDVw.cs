using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrOverTimeDVw
    {
        [Column("ID")]
        public long Id { get; set; }
        [Column("ID_M")]
        public long? IdM { get; set; }
        [Column("OverTime_Tybe")]
        public int? OverTimeTybe { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Hours { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Amount { get; set; }
        [Column("Currency_ID")]
        public int? CurrencyId { get; set; }
        public string? Description { get; set; }
        public bool? IsDeleted { get; set; }
        [Column("Currency_Code")]
        [StringLength(50)]
        public string? CurrencyCode { get; set; }
        [Column("Currency_Name")]
        [StringLength(50)]
        public string? CurrencyName { get; set; }
        [Column("OverTime_Tybe_Name")]
        [StringLength(250)]
        public string? OverTimeTybeName { get; set; }
        [Column("Emp_ID")]
        public long? EmpId { get; set; }
        [Column("Date_Tran")]
        [StringLength(10)]
        public string? DateTran { get; set; }
        public bool? IsDeletedM { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Total { get; set; }
        [Column("OverTime_H_Cost", TypeName = "decimal(18, 2)")]
        public decimal? OverTimeHCost { get; set; }
        [Column("OverTime_Date")]
        [StringLength(10)]
        public string? OverTimeDate { get; set; }
        [Column("Payment_Type")]
        public int? PaymentType { get; set; }
    }
}
