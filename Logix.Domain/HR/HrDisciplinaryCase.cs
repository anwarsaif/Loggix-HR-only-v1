using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Logix.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Table("HR_Disciplinary_Case")]
    public partial class HrDisciplinaryCase :TraceEntity
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Column("Case_Name")]
        [StringLength(2500)]
        public string? CaseName { get; set; }
        [Column("Case_Name2")]
        public string? CaseName2 { get; set; }
        
        [Column("C_Begin")]
        public int? CBegin { get; set; }
        [Column("C_End")]
        public int? CEnd { get; set; }
        [Column("Apply_OF_Delay")]
        public bool? ApplyOfDelay { get; set; }
        [Column("Apply_OF_Absence")]
        public bool? ApplyOfAbsence { get; set; }
        [Column("Type_ID")]
        public int? TypeId { get; set; }
        [Column("From_Minutes")]
        public int? FromMinutes { get; set; }
        [Column("To_Minutes")]
        public int? ToMinutes { get; set; }
        [Column("From_Day")]
        public int? FromDay { get; set; }
        [Column("To_Day")]
        public int? ToDay { get; set; }
        [Column("Apply_OF_Early")]
        public bool? ApplyOfEarly { get; set; }
    }
}
