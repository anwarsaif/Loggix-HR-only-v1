using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Logix.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Table("HR_Note")]
    public partial class HrNote:TraceEntity
    {
        [Key]
        [Column("NoteID")]
        public long NoteId { get; set; }
        [Column("Emp_Id")]
        public long? EmpId { get; set; }
        [StringLength(4000)]
        public string? NoteText { get; set; }
        [StringLength(50)]
        public string? NoteDate { get; set; }

    }
}
