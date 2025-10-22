using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Application.DTOs.HR
{
    public class HrIncomeTaxDto
    {
        public int? Id { get; set; }
        [StringLength(50)]
        public string? TaxCode { get; set; }
        [StringLength(200)]
        public string? TaxName { get; set; }
        [StringLength(200)]
        public string? TaxName2 { get; set; }
        [Column("AccountID")]
        public string? AccountCode { get; set; }
        public long? AccountId { get; set; }
    }
    public class HrIncomeTaxEditDto
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string? TaxCode { get; set; }
        [StringLength(200)]
        public string? TaxName { get; set; }
        [StringLength(200)]
        public string? TaxName2 { get; set; }
        [Column("AccountID")]
        public long? AccountId { get; set; }
        public string? AccountCode { get; set; }
        public long? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
        public bool? IsDeleted { get; set; }
    }
    public class HrIncomeTaxFilterDto
    {
        public string? TaxCode { get; set; }
        public string? TaxName { get; set; }
    }
}
