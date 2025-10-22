using Logix.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Application.DTOs.HR
{
    public class HrArchiveFilesDetailDto 
    {
        public long Id { get; set; }

        public long? ArchiveId { get; set; }

        public string? EmpCode { get; set; }

        public string? FileTypeId { get; set; }

        public string? Note { get; set; }
       

        public bool ShowEmp { get; set; }
        public bool? IsDeletedM { get; set; }

        public string? Url { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool? IsDeleted { get; set; }

    }
    public class HrArchiveFilesDetailEditDto
    {
        public long Id { get; set; }

        public long? ArchiveId { get; set; }

        public string? EmpCode { get; set; }

        public string? FileTypeId { get; set; }

        public string? Note { get; set; }

        public bool? ShowEmp { get; set; }


        public long? ModifiedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }
    

        public bool? IsDeletedM { get; set; }

        public string? Url { get; set; }
    }

}
