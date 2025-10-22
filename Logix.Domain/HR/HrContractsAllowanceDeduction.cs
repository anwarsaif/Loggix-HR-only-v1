using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Domain.HR
{
    [Table("HR_Contracts_Allowance_Deduction")]
    public class HrContractsAllowanceDeduction
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }

        [Column("Contract_ID")]
        public long? ContractId { get; set; }

        [Column("All_Ded_ID")]
        public long? AllDedId { get; set; }

        [Column("Type_ID")]
        public int? TypeId { get; set; }

        [Column("AD_ID")]
        public int? AdId { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Rate { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Amount { get; set; }

        [Column("New_Rate", TypeName = "decimal(18, 2)")]
        public decimal? NewRate { get; set; }

        [Column("New_Amount", TypeName = "decimal(18, 2)")]
        public decimal? NewAmount { get; set; }

        public long CreatedBy { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }

        public long? ModifiedBy { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsNew { get; set; }

        public bool? Status { get; set; }

        public bool? IsUpdated { get; set; }
    }

}
