using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Application.DTOs.HR
{
    public class HrClearanceTypeDto
    {

        public int TypeId { get; set; }

        [StringLength(50)]
        public string? TypeName { get; set; }
        public bool? IsDeleted { get; set; }

        [StringLength(50)]
        public string? TypeName2 { get; set; }
    }
}
