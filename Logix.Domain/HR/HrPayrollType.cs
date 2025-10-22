using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Domain.HR
{
    [Table("HR_Payroll_Type")]
    public partial class HrPayrollType
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("Type_Name")]
        [StringLength(50)]
        public string? TypeName { get; set; }
        public long? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        [Column("Type_Name2")]
        [StringLength(50)]
        public string? TypeName2 { get; set; }
    }
}
