using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrIncrementsAllowanceVw
    {
        [Column("ID")]
        public long Id { get; set; }

        [Column("Type_ID")]
        public int? TypeId { get; set; }

        [Column("AD_ID")]
        public int? AdId { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Rate { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Amount { get; set; }

        public long CreatedBy { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }

        public long? ModifiedBy { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        [StringLength(250)]
        public string? Name { get; set; }

        [Column("Catagories_ID")]
        public int? CatagoriesId { get; set; }

        [Column("New_Rate", TypeName = "decimal(18, 2)")]
        public decimal? NewRate { get; set; }

        [Column("New_Amount", TypeName = "decimal(18, 2)")]
        public decimal? NewAmount { get; set; }

        [Column("Increment_ID")]
        public long? IncrementId { get; set; }
    }

}
