using Logix.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Domain.HR
{
    [Table("HR_Authorization")]

    public class HrAuthorization :TraceEntity
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Column("Emp_ID")]
        public long? EmpId { get; set; }
        [Column("Auth_Date")]
        [StringLength(10)]
        public string? AuthDate { get; set; }
        [Column("Start_Date")]
        [StringLength(10)]
        public string? StartDate { get; set; }
        [Column("End_Date")]
        [StringLength(10)]
        public string? EndDate { get; set; }
        [Column("Delegate_Emp_ID")]
        public long? DelegateEmpId { get; set; }
        [Column("Apps_Type")]
        public string? AppsType { get; set; }
        public string? Note { get; set; }

        [Column("App_ID")]
        public long? AppId { get; set; }
    }
}
