using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Logix.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Table("HR_Request_Type")]
    public partial class HrRequestType
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Column("Request_Name")]
        [StringLength(250)]
        public string? RequestName { get; set; }
        [Column("Request_Name2")]
        [StringLength(250)]
        public string? RequestName2 { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
