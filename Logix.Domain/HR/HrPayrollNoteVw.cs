using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.Hr
{
    [Keyless]
    public partial class HrPayrollNoteVw
    {
        [Column("ID")]
        public long Id { get; set; }
        [Column("MS_ID")]
        public long? MsId { get; set; }
        [Column("State_ID")]
        public int? StateId { get; set; }
        public long? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        public string? Note { get; set; }
        [Column("Status_Name")]
        [StringLength(50)]
        public string? StatusName { get; set; }
        [Column("USER_FULLNAME")]
        [StringLength(50)]
        public string? UserFullname { get; set; }
        public string? Signature { get; set; }
    }
}
