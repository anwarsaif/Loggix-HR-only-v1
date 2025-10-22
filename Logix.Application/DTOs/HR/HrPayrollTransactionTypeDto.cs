using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Application.DTOs.HR
{
    public class HrPayrollTransactionTypeDto
    {
        public int? Id { get; set; }

        [StringLength(50)]
        public string? Name { get; set; }

        [StringLength(50)]
        public string? Name2 { get; set; }
    }
    public class HrPayrollTransactionTypeEditDto
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string? Name { get; set; }

        [StringLength(50)]
        public string? Name2 { get; set; }
    }
}
