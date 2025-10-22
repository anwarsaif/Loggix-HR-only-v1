using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Table("HR_Disciplinary_Action_Type")]
    public partial class HrDisciplinaryActionType
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Column("Action_Name")]
        [StringLength(250)]
        public string? ActionName { get; set; }
        [Column("Action_Name2")]
        [StringLength(250)]
        public string? ActionName2 { get; set; }
    }
}
