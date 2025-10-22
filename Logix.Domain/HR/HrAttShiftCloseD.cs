using Logix.Domain.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Domain.HR
{
    [Keyless]
    [Table("HR_Att_Shift_Close_D")]
    public class HrAttShiftCloseD :TraceEntity
    {

        [Column("ID")]
        public long? Id { get; set; }
        [Column("C_ID")]
        public long? CId { get; set; }
        [Column("Type_ID")]
        public int? TypeId { get; set; }
        public int? Cnt { get; set; }
       



    }
}
