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
    [Table("HR_Archive_FilesDetails")]
    public partial class HrArchiveFilesDetail
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Column("Archive_ID")]
        public long? ArchiveId { get; set; }
        [Column("Emp_ID")]
        public long? EmpId { get; set; }
        [Column("File_Type_ID")]
        public string? FileTypeId { get; set; }
        [Column("URL")]
        [StringLength(4000)]
        public string? Url { get; set; }
        [StringLength(4000)]
        public string? Note { get; set; }
        public long? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
        public bool? IsDeleted { get; set; }
        [Column("Show_Emp")]
        public bool? ShowEmp { get; set; }
    }
}