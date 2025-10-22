using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Logix.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrArchivesFilesVw : TraceEntity
    {
        [Column("Archive_File_ID")]
        public long ArchiveFileId { get; set; }
        [Column("Emp_ID")]
        public long? EmpId { get; set; }
        public int? Qty { get; set; }
        [Column("URL")]
        public string? Url { get; set; }
        [Column("No_Folder")]
        [StringLength(50)]
        public string? NoFolder { get; set; }
        [Column("No_Shelf")]
        [StringLength(50)]
        public string? NoShelf { get; set; }
        [Column("No_Safe")]
        [StringLength(50)]
        public string? NoSafe { get; set; }
        [StringLength(50)]
        public string? Note { get; set; }
        [Column("No_cartoon")]
        [StringLength(50)]
        public string? NoCartoon { get; set; }
        [Column("Emp_type_ID")]
        public int? EmpTypeId { get; set; }
        [StringLength(50)]
        public string? ArchiveDate { get; set; }
        [Column("Emp_name")]
        [StringLength(250)]
        public string? EmpName { get; set; }
        [Column("BRANCH_ID")]
        public int? BranchId { get; set; }
        [Column("BRA_NAME")]
        public string? BraName { get; set; }
        [Column("Emp_Code")]
        [StringLength(50)]
        public string EmpCode { get; set; } = null!;
        [Column("File_Type_ID")]
        public string? FileTypeId { get; set; }
        [StringLength(4000)]
        public string? Name { get; set; }
        [Column("Show_Emp")]
        public bool? ShowEmp { get; set; }
        public bool? IsDeletedD { get; set; }
    }
}
