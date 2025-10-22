using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Logix.Domain.Base;

namespace Logix.Domain.HR
{
    [Table("HR_Absence")]
    [Index("EmpId", "AbsenceDate", Name = "Ind_Emp_ID")]
    [Index("IsDeleted", Name = "Ind_Isdel")]
    public partial class HrAbsence
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }

        [Column("Emp_ID")]
        public long EmpId { get; set; }

        [Column("Absence_Type_Id")]
        public int AbsenceTypeId { get; set; }

        [Column("Absence_Date")]
        [StringLength(10)]
        [Unicode(false)]
        public string AbsenceDate { get; set; } = null!;

        public long CreatedBy { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }

        public long? ModifiedBy { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        [StringLength(10)]
        public string? Type { get; set; }

        public string? Note { get; set; }

        [Column("Location_ID")]
        public long? LocationId { get; set; }

        [Column("TimeTable_ID")]
        public int? TimeTableId { get; set; }
    }
}
