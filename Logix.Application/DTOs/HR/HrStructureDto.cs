using Logix.Domain.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Application.DTOs.HR
{
    public class HrStructureDto
    {
        public long? Id { get; set; }

        [StringLength(50)]
        [Unicode(false)]
        public string? Code { get; set; }

        [StringLength(250)]
        public string? Name { get; set; }

        [StringLength(250)]
        public string? Name2 { get; set; }

        public string? Note { get; set; }

        [Column("Status_ID")]
        public int? StatusId { get; set; }
    }
    public class HrStructureEditDto
    {
        public long Id { get; set; }

        [StringLength(50)]
        [Unicode(false)]
        public string? Code { get; set; }

        [StringLength(250)]
        public string? Name { get; set; }

        [StringLength(250)]
        public string? Name2 { get; set; }

        public string? Note { get; set; }

        [Column("Status_ID")]
        public int? StatusId { get; set; }
    }
    public class HrStructureFilterDto
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public int? StatusId { get; set; }
    }
}
