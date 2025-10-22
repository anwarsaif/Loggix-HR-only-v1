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
    [Table("HR_Assignmen")]
    public partial class HrAssignman
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [StringLength(10)]
        public string? AssignmentDate { get; set; }
        [Column("Type_ID")]
        public long? TypeId { get; set; }
        [Column("EMP_ID")]
        public long? EmpId { get; set; }
        [StringLength(10)]
        public string? FromDate { get; set; }
        [StringLength(10)]
        public string? ToDate { get; set; }
        public string? Note { get; set; }
        [Column("Facility_ID")]
        public int? FacilityId { get; set; }
        public long? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
