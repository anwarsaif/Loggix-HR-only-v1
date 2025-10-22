using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Table("HR_Ohad")]
    public partial class HrOhad
    {
        [Key]
        public long OhdaId { get; set; }
        [Column("Emp_Id")]
        public long? EmpId { get; set; }
        [StringLength(50)]
        public string? OhdaDate { get; set; }
        public long? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
        public bool? IsDeleted { get; set; }
        public long? Code { get; set; }
        [Column("Trans_Type_ID")]
        public int? TransTypeId { get; set; }
        [Column("From_Emp_ID")]
        public long? FromEmpId { get; set; }
        public string? Note { get; set; }
        [Column("Refrance_ID")]
        public long? RefranceId { get; set; }
        [Column("Refrance_Code")]
        [StringLength(50)]
        public string? RefranceCode { get; set; }
        [Column("Emp_ID_To")]
        public long? EmpIdTo { get; set; }
        [Column("Emp_ID_Recipient")]
        public long? EmpIdRecipient { get; set; }
    }
}
