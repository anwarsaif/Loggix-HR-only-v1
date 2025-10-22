using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Application.DTOs.HR
{

    public class HrAllowanceDeductionTempOrFixDto
    {
        [Key]
        public int Id { get; set; }
       
        [StringLength(50)]
        public string? TypeName { get; set; }

    }
}
