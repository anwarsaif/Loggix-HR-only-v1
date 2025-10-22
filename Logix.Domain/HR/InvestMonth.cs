using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Domain.HR
{
    [Table("INVEST_MONTH")]
    public partial class InvestMonth
    {
        [Column("Month_Code")]
        [StringLength(50)]
        public string? MonthCode { get; set; }
        [Column("Month_Name")]
        [StringLength(50)]
        public string? MonthName { get; set; }
        [Key]
        [Column("Month_ID")]
        public int MonthId { get; set; }
        [Column("Days_Of_Month")]
        public int? DaysOfMonth { get; set; }
    }
}
