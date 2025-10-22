using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Logix.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Table("HR_Competences_Catagories")]
    public partial class HrCompetencesCatagory :TraceEntity
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [StringLength(50)]
        public string? Name { get; set; }
       
    }


}
