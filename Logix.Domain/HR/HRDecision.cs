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
    [Table("HR_Decisions")]

    public class HrDecision :TraceEntity
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Column("Dec_Date")]
        [StringLength(10)]
        public string? DecDate { get; set; }
        [Column("Dec_Type")]
        public int? DecType { get; set; }
        [Column("Dec_Code")]
        public long? DecCode { get; set; }
        [Column("Emp_ID")]
        public long? EmpId { get; set; }
        public string? Note { get; set; }
        [Column("Refrance_Code")]
        [StringLength(50)]
        public string? RefranceCode { get; set; }
        [Column("Refrance_Date")]
        [StringLength(10)]
        public string? RefranceDate { get; set; }
       
        [Column("App_ID")]
        public long? AppId { get; set; }
        [Column("File_URL")]
        public string? FileUrl { get; set; }
        public bool? DecisionSigning { get; set; }
    }
}
