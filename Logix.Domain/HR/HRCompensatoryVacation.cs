using Logix.Domain.Base;
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
    [Table("HR_Compensatory_Vacations")]

    public class HrCompensatoryVacation :TraceEntity
    {
        [Key]
        [Column("Compensatory_ID")]
        public long CompensatoryId { get; set; }
        [Column("Emp_ID")]
        public long? EmpId { get; set; }
        [Column("Vacation_Type_Id")]
        public int? VacationTypeId { get; set; }
        [Column("Vacation_Account_Day")]
        public int? VacationAccountDay { get; set; }
        [Column("Vacation_SDate")]
        [StringLength(10)]
        [Unicode(false)]
        public string? VacationSdate { get; set; }
        [Column("Vacation_EDate")]
        [StringLength(10)]
        [Unicode(false)]
        public string? VacationEdate { get; set; }
        [Column("App_ID")]
        public long? AppId { get; set; }
        [Column("Status_ID")]
        public long? StatusId { get; set; }
        [StringLength(4000)]
        public string? Note { get; set; }
       
    }
}
