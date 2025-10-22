using Logix.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Application.DTOs.HR
{
    public class HrAuthorizationDto : TraceEntity
    {
        [Key]
        public long Id { get; set; }

        public long? EmpId { get; set; }
        [StringLength(10)]
        public string? AuthDate { get; set; }
        [StringLength(10)]
        public string? StartDate { get; set; }
        [StringLength(10)]
        public string? EndDate { get; set; }
        public long? DelegateEmpId { get; set; }
        public string? AppsType { get; set; }
        public string? Note { get; set; }

       

        public long? AppId { get; set; }
    } 
    public class HrAuthorizationEditDto
    {
        [Key]
        public long Id { get; set; }

        public long? EmpId { get; set; }
        [StringLength(10)]
        public string? AuthDate { get; set; }
        [StringLength(10)]
        public string? StartDate { get; set; }
        [StringLength(10)]
        public string? EndDate { get; set; }
        public long? DelegateEmpId { get; set; }
        public string? AppsType { get; set; }
        public string? Note { get; set; }

        public long? ModifiedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }


        public long? AppId { get; set; }
    }
}
