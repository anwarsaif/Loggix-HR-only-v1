using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Logix.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Table("HR_Vacations_Catagories")]
    public partial class HrVacationsCatagory : TraceEntity
    {
        [Key]
        [Column("Cat_ID")]
        public int CatId { get; set; }
        [Column("Cat_Name")]
        [StringLength(250)]
        public string? CatName { get; set; }
        [Column("Cat_Name2")]
        [StringLength(250)]
        public string? CatName2 { get; set; }
    }
}
