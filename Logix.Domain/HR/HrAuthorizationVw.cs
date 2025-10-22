using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Domain.HR
{
    [Keyless]

    public class HrAuthorizationVw
    {
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
        [Column("Apps_Type")]
        public string? AppsType { get; set; }
        public string? Note { get; set; }
        public bool IsDeleted { get; set; }
        [Column("App_ID")]
        public long? AppId { get; set; }
        [Column("Emp_Code")]
        [StringLength(50)]
        public string EmpCode { get; set; } = null!;
        [Column("Emp_name")]
        [StringLength(250)]
        public string? EmpName { get; set; }
        [Column("Delegate_Emp_ID")]
        public long? DelegateEmpId { get; set; }
        [Column("Delegate_Emp_Code")]
        [StringLength(50)]
        public string DelegateEmpCode { get; set; } = null!;
        [Column("Delegate_Emp_Name")]
        [StringLength(250)]
        public string? DelegateEmpName { get; set; }
    }
}
